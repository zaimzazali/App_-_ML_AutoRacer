using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileEvents : MonoBehaviour
{
    public float expandSize = 0.1f;
    public float expandDuration = 0.1f;
    
    public void tileExpand() {
        LeanTween.scale(gameObject, new Vector3(expandSize,expandSize,expandSize), expandDuration);
        gameObject.GetComponent<AudioSource>().Play();
    }

    public void tileNormal() {
        LeanTween.scale(gameObject, new Vector3(1f,1f,1f), expandDuration);
        // gameObject.GetComponent<AudioSource>().Stop();
    }
}
