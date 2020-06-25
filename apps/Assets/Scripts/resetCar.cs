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

    private void OnTriggerEnter (Collider collider) {
        if (collider.gameObject.layer == 10) {
            resetTheCar();
        }
    }

    private void resetTheCar() {
        VPVehicleController.enabled = false;

        GameObject theAgent = gameObject.transform.parent.gameObject;
        theAgent.transform.position = new Vector3(10f, 0.3f, 0f);
        theAgent.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        VPVehicleController.enabled = true;
        VPCameraController.ResetCamera();
    }
}
