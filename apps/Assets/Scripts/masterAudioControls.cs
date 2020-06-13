using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class masterAudioControls : MonoBehaviour
{
    [SerializeField]
    private float initialMasterVolume = 0f;

    private void Awake() {
        AudioListener.volume = initialMasterVolume;
    }
    
    public void setVolume(float myVolume) {
        AudioListener.volume = myVolume;
    }

    public float getVolume() {
        return AudioListener.volume;
    }
}
