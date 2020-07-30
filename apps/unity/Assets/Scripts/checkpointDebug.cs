using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpointDebug : MonoBehaviour
{
    [Tooltip("What is the color of the forward direction of every collider we want to draw?")]
    public Color RayColor = Color.yellow;
    [Tooltip("What is the color of the collider we want to draw?")]
    public Color ColliderColor = Color.red;
    [Tooltip("What is the distance of the forward direction we want to multiply by?")]
    public float RayLength = 3f;
    [Tooltip("What are the agent checkpoints we want to draw?")]
    public Collider[] Colliders;
    [Tooltip("What is the general name of each collider? For example, \"Training Checkpoint\" can be a name.")]
    public string ColliderNameTemplate;

    void OnDrawGizmos()
    {
        foreach (var collider in Colliders)
        {
            Gizmos.color = RayColor;
            Transform xform = collider.transform;
            Gizmos.DrawLine(xform.position, xform.position + xform.forward * RayLength);
            Gizmos.color = ColliderColor;
            Gizmos.DrawWireCube(xform.position, Vector3.one);
        }
    }
}
