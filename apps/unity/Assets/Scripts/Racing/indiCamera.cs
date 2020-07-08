using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class indiCamera : MonoBehaviour
{
    private GameObject target = null;
    [SerializeField]
    private float distance = 0f;
    [SerializeField]
    private float height = 0f;
    [SerializeField]
    private float dampening = 0f;

    private void Awake() {
        target = gameObject.transform.parent.gameObject;
    }

    private void Update() {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, target.transform.position + target.transform.TransformDirection(new Vector3(0f, height, -distance)), Time.deltaTime);
        gameObject.transform.LookAt(target.transform);
    }
}
