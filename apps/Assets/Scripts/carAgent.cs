using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using VehiclePhysics;

public class carAgent : Agent
{
    private agentData agentData = null;
    private VPStandardInput VPStandardInput = null;
    private VPVehicleController VPVehicleController = null;
    [SerializeField]
    private VPCameraController VPCameraController = null;

    private void Start() {
        agentData = gameObject.GetComponent<agentData>();
        VPStandardInput = gameObject.GetComponent<VPStandardInput>();
        VPVehicleController = gameObject.GetComponent<VPVehicleController>();
    }

    public override void OnEpisodeBegin() {
        VPVehicleController.enabled = false;
        agentData.resetProgression();
        StartCoroutine("init");
    }

    private IEnumerator init() {
        yield return new WaitForEndOfFrame();
        VPVehicleController.enabled = true;
        VPVehicleController.Reposition(new Vector3(10f, 0.5f, 0f), Quaternion.Euler(0f, 0f, 0f));
        VPCameraController.ResetCamera();
    }

    public override void CollectObservations(VectorSensor sensor) {
        sensor.AddObservation(gameObject.transform.localPosition);              // 3
        sensor.AddObservation(gameObject.transform.localRotation);              // 4
        sensor.AddObservation(gameObject.GetComponent<Rigidbody>().velocity.x); // 1
        sensor.AddObservation(gameObject.GetComponent<Rigidbody>().velocity.z); // 1

        sensor.AddObservation(agentData.currentThrottleInput);                  // 1
        sensor.AddObservation(agentData.currentBrakeInput);                     // 1
        sensor.AddObservation(agentData.currentSteerInput);                     // 1

        sensor.AddObservation(agentData.currentKMH);                            // 1
        sensor.AddObservation(agentData.currentRPM);                            // 1
        sensor.AddObservation(agentData.currentGear);                           // 1

        sensor.AddObservation(agentData.distanceToNextCheckpoint);              // 1
        sensor.AddObservation(agentData.isIdle);                                // 1
        sensor.AddObservation(agentData.isRightDirection);                      // 1

        sensor.AddObservation(agentData.rayWall_00);                            // 1
        sensor.AddObservation(agentData.rayWall_01);                            // 1
        sensor.AddObservation(agentData.rayWall_02);                            // 1
        sensor.AddObservation(agentData.rayWall_03);                            // 1
        sensor.AddObservation(agentData.rayWall_04);                            // 1
    }

    public override void OnActionReceived(float[] vectorAction) {
        // Actions, size = 2
        if (vectorAction[0] >= 0f) {
            VPStandardInput.externalThrottle = vectorAction[0];
            VPStandardInput.externalBrake = 0f;
        } else {
            VPStandardInput.externalThrottle = 0f;
            VPStandardInput.externalBrake = -1*(vectorAction[0]);
        }
        
        VPStandardInput.externalSteer = vectorAction[1];

        bool toReset = false;

        if (agentData.currentKMH > 10f) {
            AddReward(1f);
        } else {
            AddReward(-1f);
        }

        if (agentData.rayWall_00 > 3.5f && agentData.rayWall_04 > 3.5f ) {
            AddReward(1f);
        } else {
            AddReward(-2f);
        }

        if (agentData.isIdle) {
            AddReward(-10f);
        }

        if (agentData.isRightDirection) {
            AddReward(0.1f);
        }

        if (gameObject.transform.position.y < 0f) {
            AddReward(-100f);
            toReset = true;
        } else if (GetCumulativeReward() < -5000f) {
            toReset = true;
        }

        Debug.Log("Cum Reward = " + GetCumulativeReward());

        if (toReset) {
            EndEpisode();
        }
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetAxis("Vertical");
        actionsOut[1] = Input.GetAxis("Horizontal");

        // Debug.Log(actionsOut[0]);
        // Debug.Log(actionsOut[1]);
    }
}
