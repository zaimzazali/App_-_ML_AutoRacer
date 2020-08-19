using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using MoonSharp.Interpreter;


// Sensor to detect distance to wall
[System.Serializable]
public struct Sensor {
    public Transform Transform;
    public float HitThreshold;
}

// Mode
public enum AgentMode {
    Training,
    Inferencing
}

// The Agent
public class agentBrain : Agent
{
    // Steering and Throttle/Braking
    const int LocalActionSize = 2;

#region Training Modes
    [Tooltip("Are we training the agent or is the agent production ready?")]
    public AgentMode Mode = AgentMode.Training;
    [Tooltip("What is the initial checkpoint the agent will go to? This value is only for inferencing.")]
    public ushort InitCheckpointIndex;
#endregion

#region Senses
    [Header("Observation Params")]
    [Tooltip("How far should the agent shoot raycasts to detect the world?")]
    public float RaycastDistance;
    [Tooltip("What objects should the raycasts hit and detect?")]
    public LayerMask Mask;
    [Tooltip("Sensors contain ray information to sense out the world, you can have as many sensors as you need.")]
    public Sensor[] Sensors;

    [Header("Checkpoints")]
    [Tooltip("What are the series of checkpoints for the agent to seek and pass through?")]
    public Collider[] Colliders;
    [Tooltip("What layer are the checkpoints on? This should be an exclusive layer for the agent to use.")]
    public LayerMask CheckpointMask;

    [Space]
    [Tooltip("Would the agent need a custom transform to be able to raycast and hit the track? " +
        "If not assigned, then the root transform will be used.")]
    public Transform AgentSensorTransform;
#endregion

#region Rewards
    [Header("Rewards")]
    [Tooltip("What penatly is given when the agent crashes?")]
    public float HitPenalty = -1f;
    [Tooltip("How much reward is given when the agent successfully passes the checkpoints?")]
    public float PassCheckpointReward;
    [Tooltip("Should typically be a small value, but we reward the agent for moving in the right direction.")]
    public float TowardsCheckpointReward;
    [Tooltip("Typically if the agent moves faster, we want to reward it for finishing the track quickly.")]
    public float SpeedReward;
#endregion

#region ResetParams
    [Header("Inference Reset Params")]
    [Tooltip("What is the unique mask that the agent should detect when it falls out of the track?")]
    public LayerMask OutOfBoundsMask;
    [Tooltip("What are the layers we want to detect for the track and the ground?")]
    public LayerMask TrackMask;
    [Tooltip("How far should the ray be when casted? For larger karts - this value should be larger too.")]
    public float GroundCastDistance;
#endregion

#region Debugging
    [Header("Debug Option")]
    [Tooltip("Should we visualize the rays that the agent draws?")]
    public bool ShowRaycasts;
#endregion

    private agentKart kart;
    private float acceleration;
    private float steering;
    private float[] localActions;
    private int checkpointIndex;

#region Variables
    [Header("Variables")]
    [SerializeField]
    private bool isOutBound = false;
    [SerializeField]
    private bool isCarReversed = false;
    [SerializeField]
    private bool passedCheckpoint = false;
    [SerializeField]
    private int checkpointPassed = 0;
    [SerializeField]
    private int cumCheckpointPassed = 0;
    [SerializeField]
    private bool finishedLap = false;
    [SerializeField]
    private int totalCheckpoints = 0;
    [SerializeField]
    private List<float> distanceToWall;
#endregion

    // private string filePath = @"D:\Git_Clone\App_-_ML_AutoRacer\apps\python-lua\rewardFunction.lua"; // testing
    private string filePath = @".\build\python-lua\rewardFunction.lua"; // production
    private string rewardScript = "";
    private Script script;
    private string functionHeader = "function rewardFunction (passedCheckpoint, distanceToCheckpoint, distanceToWall, " + 
                                    "speed, isOutBound, checkpointPassed, cumCheckpointPassed, isCarReversed, " + 
                                    "finishedLap, steering, acceleration)";
    private string functionFooter = "\n" + "end";
    [SerializeField]
    private GameObject statusBackground = null;
    [SerializeField]
    private Text statusText = null;
    private bool toProceed = false;

    private void Awake() {
        kart = GetComponent<agentKart>();
        if (AgentSensorTransform == null) {
            AgentSensorTransform = transform;
        }

        try {
            if (File.Exists(filePath)) {
                rewardScript = File.ReadAllText(filePath);
                rewardScript = functionHeader +
                                "\n" + rewardScript + "\n" +
                                functionFooter;

                Debug.Log(rewardScript);

                script = new Script();
                script.DoString(rewardScript);
                toProceed = true;
            } else {
                statusBackground.GetComponent<Image>().color = Color.red;
                statusText.text = "Cannot find reward function file";
            }
        } catch (System.Exception) {
            // Do Nothing
        }
        
    }

    private void Start() {
        localActions = new float[LocalActionSize];

        // If the agent is training, then at the start of the simulation, pick a random checkpoint to train the agent.
        OnEpisodeBegin();

        if (Mode == AgentMode.Inferencing) {
            checkpointIndex = InitCheckpointIndex;
        }

        totalCheckpoints = Colliders.Length;
    }

    private void LateUpdate() {
        switch (Mode) {
            case AgentMode.Inferencing:
                if (ShowRaycasts) {
                    Debug.DrawRay(transform.position, Vector3.down * GroundCastDistance, Color.cyan);
                }
                // We want to place the agent back on the track if the agent happens to launch itself outside of the track.
                if (Physics.Raycast(transform.position, Vector3.down, out var hit, GroundCastDistance, TrackMask)
                    && ((1 << hit.collider.gameObject.layer) & OutOfBoundsMask) > 0) {
                    // Reset the agent back to its last known agent checkpoint
                    Transform checkpoint      = Colliders[checkpointIndex].transform;
                    transform.localRotation   = checkpoint.rotation;
                    transform.position        = checkpoint.position;
                    kart.Rigidbody.velocity = default;
                    acceleration = steering = 0f;
                }
                break;
        }
    }

    private void OnTriggerEnter(Collider other) {
        int maskedValue = 1 << other.gameObject.layer;
        int triggered   = maskedValue & CheckpointMask;

        FindCheckpointIndex(other, out int index);

        passedCheckpoint = false;

        // Ensure that the agent touched the checkpoint and the new index is greater than the m_CheckpointIndex.
        if (triggered > 0 && index > checkpointIndex || index == 0 && checkpointIndex == Colliders.Length - 1) {
            passedCheckpoint = true;
            checkpointIndex = index;
            checkpointPassed += 1;
            cumCheckpointPassed += 1;
            finishedLap = false;

            if (checkpointPassed == Colliders.Length) {
                checkpointPassed = 0;
                finishedLap = true;
            }
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Track")) {
            isOutBound = true;
        } else {
            isOutBound = false;
        }
    }

    private void FindCheckpointIndex(Collider checkPoint, out int index) {
        for (int i = 0; i < Colliders.Length; i++) {
            if (Colliders[i].GetInstanceID() == checkPoint.GetInstanceID()) {
                index = i;
                return;
            }
        }
        index = -1;
    }

    private float Sign(float value) {
        if (value > 0) {
            return 1;
        } else if (value < 0) {
            return -1;
        }
        return 0;
    }

    private void InterpretDiscreteActions(float[] actions) {
        steering     = actions[0] - 1f;
        acceleration = actions[1] - 1f;

        kart.GatherInputs(steering, acceleration);
    }

    public override void CollectObservations(VectorSensor sensor) {

        sensor.AddObservation(kart.LocalSpeed());
        if (kart.LocalSpeed() < 0f) {
            isCarReversed = true;
        } else {
            isCarReversed = false;
        }
        
        // Add an observation for direction of the agent to the next checkpoint.
        var next          = (checkpointIndex + 1) % Colliders.Length;
        var nextCollider  = Colliders[next];
        var direction     = (nextCollider.transform.position - kart.transform.position).normalized;
        float distanceToCheckpoint      = Vector3.Dot(kart.Rigidbody.velocity.normalized, direction);
        sensor.AddObservation(distanceToCheckpoint);

        if (ShowRaycasts) {
            Debug.DrawLine(AgentSensorTransform.position, nextCollider.transform.position, Color.magenta);
        }

        distanceToWall = new List<float>();
        for (int i = 0; i < Sensors.Length; i++) {
            var current = Sensors[i];
            var xform   = current.Transform;
            var hit     = Physics.Raycast(AgentSensorTransform.position, xform.forward, out var hitInfo,
                RaycastDistance, Mask, QueryTriggerInteraction.Ignore);

            if (ShowRaycasts) {
                Debug.DrawRay(AgentSensorTransform.position, xform.forward * RaycastDistance, Color.green);
                Debug.DrawRay(AgentSensorTransform.position, xform.forward * RaycastDistance * current.HitThreshold, Color.red);
            }

            var hitDistance = (hit ? hitInfo.distance : RaycastDistance) / RaycastDistance;
            sensor.AddObservation(hitDistance);

            distanceToWall.Add(hitDistance);
        }

        sensor.AddObservation(isOutBound);
        sensor.AddObservation(gameObject.transform.localPosition);
        sensor.AddObservation(gameObject.transform.localRotation);
        sensor.AddObservation(checkpointPassed);
        sensor.AddObservation(cumCheckpointPassed);
        sensor.AddObservation(isCarReversed);
        sensor.AddObservation(finishedLap);
        sensor.AddObservation(steering);
        sensor.AddObservation(acceleration);

        // ------------------------------------------------------------------------------------------------

        float thisReward = 0f;

        if (toProceed) {
            thisReward = getRewardFromFunction(passedCheckpoint, distanceToCheckpoint, distanceToWall, 
                                                    kart.LocalSpeed(), isOutBound, checkpointPassed, isCarReversed,
                                                    cumCheckpointPassed, finishedLap, steering, acceleration);
        }

        // Debug.Log(thisReward);
        AddReward(thisReward);

        if (isOutBound) {
            OnEpisodeBegin();
        }
    }

    private float getRewardFromFunction(bool passedCheckpoint, float distanceToCheckpoint, List<float> distanceToWall, 
                                        float speed, bool isOutBound, int checkpointPassed, bool isCarReversed,
                                        int cumCheckpointPassed, bool finishedLap, float steering, float acceleration) {

        try {
            script.Globals["passedCheckpoint"]      = passedCheckpoint;
            script.Globals["distanceToCheckpoint"]  = distanceToCheckpoint;
            script.Globals["distanceToWall"]        = distanceToWall;
            script.Globals["speed"]                 = speed;
            script.Globals["isOutBound"]            = isOutBound;
            script.Globals["checkpointPassed"]      = checkpointPassed;
            script.Globals["cumCheckpointPassed"]   = cumCheckpointPassed;
            script.Globals["cumCheckpointPassed"]   = cumCheckpointPassed;
            script.Globals["isCarReversed"]         = isCarReversed;
            script.Globals["steering"]              = steering;
            script.Globals["acceleration"]          = acceleration;

            DynValue res = script.Call(script.Globals["rewardFunction"], passedCheckpoint, distanceToCheckpoint, distanceToWall,
                                                                            speed, isOutBound, checkpointPassed, cumCheckpointPassed,
                                                                            finishedLap, steering, acceleration);

            statusText.text = "Training";

            // Debug.Log((float)res.Number);
            return (float)res.Number;

        } catch (System.Exception) {
            statusBackground.GetComponent<Image>().color = Color.red;
            statusText.text = "Error in Reward Function";
            return 0f;
        }
    }

    public override void OnActionReceived(float[] vectorAction) {
        InterpretDiscreteActions(vectorAction);

        if (ShowRaycasts) {
            Debug.DrawRay(AgentSensorTransform.position, kart.Rigidbody.velocity, Color.blue);
        }
    }

    public override void OnEpisodeBegin() {
        switch (Mode) {
            case AgentMode.Training:
                checkpointIndex         = Random.Range(0, Colliders.Length - 1);
                Collider collider       = Colliders[checkpointIndex];
                transform.localRotation = collider.transform.rotation;
                transform.position      = collider.transform.position;
                acceleration            = 0f;
                steering                = 0f;

                kart.Rigidbody.velocity         = Vector3.zero;
                kart.Rigidbody.angularVelocity  = Vector3.zero; 
                
                isOutBound              = false;
                isCarReversed           = false;
                passedCheckpoint        = false;
                checkpointPassed        = 0;
                cumCheckpointPassed     = 0;
                finishedLap             = false;
                distanceToWall          = new List<float>();
                break;
            default:
                break;
        }
    }

    public override void Heuristic(float[] actionsOut) {
        actionsOut[0] = Input.GetAxis("Horizontal") + 1f;
        actionsOut[1] = Input.GetAxis("Vertical") + 1f;
    }


}
