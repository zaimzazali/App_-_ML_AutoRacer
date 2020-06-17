using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initSound : MonoBehaviour
{
    private float waitingTime = 0.5f;
    private AudioSource thisSound;

    private void Awake() {
        thisSound = GetComponent<AudioSource>();
        playSound();
    }

    private void playSound() {
        thisSound.PlayDelayed(waitingTime);
    }

    public float getWaitingTime() {
        return waitingTime;
    }
}
