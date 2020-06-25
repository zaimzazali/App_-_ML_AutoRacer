using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VehiclePhysics.UI;

public class agentData : MonoBehaviour
{
    [SerializeField]
    private InputMonitor InputMonitor = null;

    [SerializeField]
    private Dashboard Dashboard = null;


    // For display only
    [Header("Input Monitor")]
	[SerializeField]
	private float currentThrottleInput = 0f;
	[SerializeField]
	private float currentBrakeInput = 0f;
	[SerializeField]
	private float currentSteerInput = 0f;
	[SerializeField]
	private float currentSteerInputDegree = 0f;

    [Header("Dashboard")]
	[SerializeField]
	private float currentKMH = 0f;
	[SerializeField]
	private float currentRPM = 0f;
	[SerializeField]
	private int currentGear = 0;

    [Header("Checkpoints")]
    [SerializeField]
    private GameObject[] checkpoints = null;
    private int checkpointIndex = 0;
    private GameObject nextCheckpoint = null;
    private float distanceToNextCheckpoint = 0f;
    private float lastDistance = 0f, maxLastDistance = 0f;
    private bool isIdle = false, isRightDirection = false;





    private void Awake() {
        nextCheckpoint = checkpoints[checkpointIndex];

        distanceToNextCheckpoint = calculateDistanceToNextCheckpoint();

        lastDistance = distanceToNextCheckpoint;
        maxLastDistance = distanceToNextCheckpoint;
    }
    
    private void Update() {
        currentThrottleInput = InputMonitor.getCurrentThrottleInput();
        currentBrakeInput = InputMonitor.getCurrentBrakeInput();
        currentSteerInput = InputMonitor.getCurrentSteerInput();
        currentSteerInputDegree = InputMonitor.getCurrentSteerInputDegree();

        currentKMH = Dashboard.getCurrentKMH();
        currentRPM = Dashboard.getCurrentRPM();
        currentGear = Dashboard.getCurrentGear();

        distanceToNextCheckpoint = calculateDistanceToNextCheckpoint();

        isIdle = checkIsIdle();

        if (!isIdle) {
            isRightDirection = checkIsRightDirection();
        }
    }

    private bool checkIsIdle() {
        if (gameObject.GetComponent<Rigidbody>().velocity.magnitude < 0.5f && 
            currentKMH <= 0f && 
            currentThrottleInput <= 0f) {
            return true;
        } 
        return false;
    }

    private bool checkIsRightDirection() {
        bool flag = false;
        if (distanceToNextCheckpoint < maxLastDistance || Mathf.Approximately(distanceToNextCheckpoint, maxLastDistance)) {
            maxLastDistance = distanceToNextCheckpoint;
            //Debug.Log("Right Direction - Pure");
            flag = true;
        } else if (distanceToNextCheckpoint > lastDistance) {
            //Debug.Log("Wrong Direction");
            flag = false;
        } else {
            //Debug.Log("Right Direction - Recovery");
            flag = true;
        }
        lastDistance = distanceToNextCheckpoint;
        return flag;
    }

    public GameObject getTargetCheckpoint() {
        return nextCheckpoint;
    }

    public bool isWithinCheckpointIndexLength() {
        if (checkpointIndex+1 >= checkpoints.Length) {
            return false;
        }
        return true;
    } 

    public void increaseCheckpointIndex() {
        checkpointIndex += 1;
        nextCheckpoint = checkpoints[checkpointIndex];
        maxLastDistance = calculateDistanceToNextCheckpoint();
    }

    public void resetCheckpointIndex() {
        checkpointIndex = 0;
        nextCheckpoint = checkpoints[checkpointIndex];
        maxLastDistance = calculateDistanceToNextCheckpoint();
    }

    private float calculateDistanceToNextCheckpoint() {
        return Vector3.Distance(gameObject.transform.position, nextCheckpoint.transform.position);
    }
    
}
