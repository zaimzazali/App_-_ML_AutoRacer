using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    [SerializeField]
    private Camera[] cameras = null;

    private int cameraIndex = 0;

    private void Awake() {
        cameras[cameraIndex].gameObject.SetActive(true);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.C)) {
            changeCamera(); 
        }
    }

    private void changeCamera() {
        cameras[cameraIndex].gameObject.SetActive(false);
        if (cameraIndex+1 >= cameras.Length) {
            cameraIndex = 0;
        } else {
            cameraIndex += 1;
        }
        cameras[cameraIndex].gameObject.SetActive(true);
    }
}
