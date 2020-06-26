using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehiclePhysics;

public class resetCar : MonoBehaviour
{    
    [SerializeField]
    private VPVehicleController VPVehicleController = null;
    [SerializeField]
    private VPCameraController VPCameraController = null;

    [SerializeField]
    private agentData agentData = null;

    private bool toTrigger = false;
    
    
    private void Awake() {
        toTrigger = true;
    }

    private void OnTriggerEnter (Collider collider) {
        if (collider.gameObject.layer == 10 && toTrigger) {
            toTrigger = false;
            StartCoroutine("resetTheCar");
        }
    }

    private IEnumerator resetTheCar() {
        agentData.resetProgression();

        VPVehicleController.Reposition(new Vector3(10f, 0.5f, 0f), Quaternion.Euler(0f, 0f, 0f));
        VPVehicleController.enabled = false;
        VPCameraController.ResetCamera();

        yield return new WaitForEndOfFrame();
        
        VPVehicleController.enabled = true;
        toTrigger = true;
    }
}
