using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputManager : MonoBehaviour
{
    [SerializeField]
    private float throttle = 0f;
    [SerializeField]
    private float steer = 0f;

    // Update is called once per frame
    private void Update() {
        throttle = Input.GetAxis("Vertical");
        steer = Input.GetAxis("Horizontal");
    }

    public float getThrottle() {
        return throttle;
    }

    public float getSteer() {
        return steer;
    }
}
