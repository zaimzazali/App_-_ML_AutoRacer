﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tileEvents : MonoBehaviour
{
    [SerializeField]
    private float expandSize = 0f, expandDuration = 0f;

    private bool isActive;

    private void Awake() {
        if (gameObject.GetComponent<Button>() != null) { 
            isActive = gameObject.GetComponent<Button>().interactable;
        } else {
            isActive = gameObject.GetComponentInParent<Button>().interactable;
        }
    }
    
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