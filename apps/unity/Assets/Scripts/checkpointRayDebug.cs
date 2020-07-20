using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpointRayDebug : MonoBehaviour
{
    public Color RayColor = Color.yellow;
    public Color ColliderColor = Color.red;
    public float RayLength = 3f;
    public Collider[] Colliders;
    public string ColliderNameTemplate;

    private void OnDrawGizmos()
    {
        foreach (var collider in Colliders) {
            Gizmos.color = RayColor;
            Transform xform = collider.transform;
            Gizmos.DrawLine(xform.position, xform.position + xform.forward * RayLength);
            Gizmos.color = ColliderColor;
            Gizmos.DrawWireCube(xform.position, Vector3.one);
        }
    }
}
