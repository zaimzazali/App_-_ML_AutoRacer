using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour
{
    [SerializeField]
    private GameObject focus = null;
    [SerializeField]
    private float distance = 0f;
    [SerializeField]
    private float height = 0f;
    [SerializeField]
    private float dampening = 0f;

    private void Update() {
        transform.position = Vector3.Lerp(transform.position, focus.transform.position + focus.transform.TransformDirection(new Vector3(0f, height, -distance)), dampening * Time.deltaTime);
        transform.LookAt(focus.transform);
    }
}
