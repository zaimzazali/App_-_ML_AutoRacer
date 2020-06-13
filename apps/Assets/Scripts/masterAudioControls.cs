using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class masterAudioControls : MonoBehaviour
{
    void Awake() {
        AudioListener.volume = 0.5f;
    }
    public void setVolume(float myVolume) {
        AudioListener.volume = myVolume;
    }

    public float getVolume() {
        return AudioListener.volume;
    }
}
