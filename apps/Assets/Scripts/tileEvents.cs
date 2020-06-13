using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileEvents : MonoBehaviour
{
    public bool isActive = true;

    public float expandSize = 0.1f;
    public float expandDuration = 0.1f;
    
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
