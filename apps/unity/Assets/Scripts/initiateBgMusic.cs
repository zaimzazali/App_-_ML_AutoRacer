using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initiateBgMusic : MonoBehaviour
{
    private AudioSource thisSound;

    private void Awake() {
        thisSound = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    public void playSound(float waitingTime ) {
        thisSound.PlayDelayed(waitingTime);
    }
}
