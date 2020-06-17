using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class masterAudioControls : MonoBehaviour
{
    private float initialMasterVolume = 0.5f;

    private void Awake() {
        if (PlayerPrefs.HasKey("setMasterVolumeValue")) {
            AudioListener.volume = PlayerPrefs.GetFloat("setMasterVolumeValue");
        } else {
            AudioListener.volume = initialMasterVolume;
            PlayerPrefs.SetFloat("setMasterVolumeValue", initialMasterVolume);
        }
    }
    
    public void setVolume(float myVolume) {
        AudioListener.volume = myVolume;
        PlayerPrefs.SetFloat("setMasterVolumeValue", myVolume);
    }

    public float getVolume() {
        return AudioListener.volume;
    }
}
