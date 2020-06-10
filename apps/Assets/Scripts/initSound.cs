using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initSound : MonoBehaviour
{
    public float waitingTime = 0;
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
}
