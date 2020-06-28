﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VehiclePhysics;
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
	public float currentThrottleInput = 0f;
	[SerializeField]
	public float currentBrakeInput = 0f;
	[SerializeField]
	public float currentSteerInput = 0f;
	[SerializeField]
	public float currentSteerInputDegree = 0f;

    [Header("Dashboard")]
	[SerializeField]
	public float currentKMH = 0f;
	[SerializeField]
	public float currentRPM = 0f;
	[SerializeField]
	public int currentGear = 0;

    [Header("Checkpoints")]
    [SerializeField]
    public GameObject[] checkpoints = null;
    [SerializeField]
    public int checkpointIndex = 0;
    [SerializeField]
    public GameObject nextCheckpoint = null;
    [SerializeField]
    public GameObject previousCheckpoint = null;
    [SerializeField]
    public float distanceToNextCheckpoint = 0f;
    [SerializeField]
    public float lastDistance = 0f;
    [SerializeField]
    public float maxLastDistance = 0f;
    [SerializeField]
    public bool isIdle = false;
    [SerializeField]
    public bool isRightDirection = false;

    [Header("Raycasts")]
    [SerializeField]
    public float rayWall_00 = 0f;
    [SerializeField]
    public float rayWall_01 = 0f;
    [SerializeField]
    public float rayWall_02 = 0f;
    [SerializeField]
    public float rayWall_03 = 0f;
    [SerializeField]
    public float rayWall_04 = 0f;


    private void Awake() {
        resetProgression();
    }

    public void resetProgression() {
        checkpointIndex = 0;
        nextCheckpoint = checkpoints[checkpointIndex];
        previousCheckpoint = checkpoints[checkpoints.Length-1];

        distanceToNextCheckpoint = calculateDistanceToNextCheckpoint();

        lastDistance = distanceToNextCheckpoint;
        maxLastDistance = distanceToNextCheckpoint;

        isIdle = false;
        isRightDirection = false;
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

        shootRays_Wall();
    }

    private void FixedUpdate() {
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
            // Debug.Log("Right Direction - Pure");
            flag = true;
        } else if (distanceToNextCheckpoint > lastDistance) {
            // Debug.Log("Wrong Direction");
            flag = false;
        } else {
            // Debug.Log("Right Direction - Recovery");
            flag = true;
        }
        lastDistance = distanceToNextCheckpoint;
        return flag;
    }

    private void shootRays_Wall() {
        RaycastHit hit;
        int layerMask = ~( (1 << 11) | (1 << 12) );

        Physics.Raycast(gameObject.transform.position, -gameObject.transform.right, out hit, Mathf.Infinity, layerMask);
        Debug.DrawLine (gameObject.transform.position, hit.point,Color.red);
        rayWall_00 = hit.distance;

        Physics.Raycast(gameObject.transform.position, (transform.forward - transform.right).normalized, out hit, Mathf.Infinity, layerMask);
        Debug.DrawLine (gameObject.transform.position, hit.point,Color.red);
        rayWall_01 = hit.distance;

        Physics.Raycast(gameObject.transform.position, transform.forward, out hit, Mathf.Infinity, layerMask);
        Debug.DrawLine (gameObject.transform.position, hit.point,Color.red);
        rayWall_02 = hit.distance;

        Physics.Raycast(gameObject.transform.position, (transform.forward + transform.right).normalized, out hit, Mathf.Infinity, layerMask);
        Debug.DrawLine (gameObject.transform.position, hit.point,Color.red);
        rayWall_03 = hit.distance;

        Physics.Raycast(gameObject.transform.position, gameObject.transform.right, out hit, Mathf.Infinity, layerMask);
        Debug.DrawLine (gameObject.transform.position, hit.point,Color.red);
        rayWall_04 = hit.distance;
    }

    public GameObject getTargetCheckpoint() {
        return nextCheckpoint;
    }

    public GameObject getPreviousCheckpoint() {
        return previousCheckpoint;
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
        previousCheckpoint = checkpoints[checkpointIndex-1];
        maxLastDistance = calculateDistanceToNextCheckpoint();
    }

    public void resetCheckpointIndex() {
        checkpointIndex = 0;
        nextCheckpoint = checkpoints[checkpointIndex];
        previousCheckpoint = checkpoints[checkpoints.Length-1];
        maxLastDistance = calculateDistanceToNextCheckpoint();
    }

    private float calculateDistanceToNextCheckpoint() {
        return Vector3.Distance(gameObject.transform.position, nextCheckpoint.transform.position);
    }
    
}
