using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initSound : MonoBehaviour
{
    [SerializeField]
    private float waitingTime = 0f;
    private AudioSource thisSound;

    private void Start()
    {
        thisSound = GetComponent<AudioSource>();
        playSound();
    }

    private void playSound()
    {
        thisSound.PlayDelayed(waitingTime);
    }

    public float getWaitingTime() {
        return waitingTime;
    }
}
