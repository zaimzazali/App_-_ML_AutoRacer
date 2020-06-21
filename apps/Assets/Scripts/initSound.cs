﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initSound : MonoBehaviour
{
    [SerializeField]
    private GameObject canvasFader = null;  

    private float waitingTime = 0f;
    private AudioSource thisSound;

    private void Awake() {
        thisSound = GetComponent<AudioSource>();

        sceneFader sceneFader = canvasFader.transform.GetChild(0).gameObject.GetComponent<sceneFader>();
        waitingTime = sceneFader.getTotalWaitingTime();
        
        playSound();
    }

    private void playSound() {
        thisSound.PlayDelayed(waitingTime);
    }

    public float getWaitingTime() {
        return waitingTime;
    }
}
