using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoint : MonoBehaviour
{
    [SerializeField]
    private agentData agentData = null;

    private void OnTriggerEnter () {
        if (gameObject.transform.parent.gameObject == agentData.getTargetCheckpoint()) {
            if (agentData.isWithinCheckpointIndexLength()) {
                agentData.increaseCheckpointIndex();
            } else {
                agentData.resetCheckpointIndex();
            }
        }
    }
}
