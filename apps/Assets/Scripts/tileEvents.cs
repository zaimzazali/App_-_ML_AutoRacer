using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileEvents : MonoBehaviour
{
    [SerializeField]
    private bool isActive = true;

    [SerializeField]
    private float expandSize = 0f, expandDuration = 0f;
    
    public void tileExpand() {
        if (isActive) {
            LeanTween.scale(gameObject, new Vector3(expandSize,expandSize,expandSize), expandDuration);
            gameObject.GetComponent<AudioSource>().Play();
        }
    }

    public void tileNormal() {
        if (isActive) {
            LeanTween.scale(gameObject, new Vector3(1f,1f,1f), expandDuration);
        }
    }
}
