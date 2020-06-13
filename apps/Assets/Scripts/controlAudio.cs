using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class controlAudio : MonoBehaviour
{
    public masterAudioControls masterAudioControls;
    public Text theText;
    public Image audioImage;
    public Sprite[] images;
    
    void Start()
    {
        gameObject.GetComponent<Slider>().value = masterAudioControls.getVolume();
        theText.text = (gameObject.GetComponent<Slider>().value * 100).ToString("F0") + "%";
    }
    public void updateText() {
        theText.text = (gameObject.GetComponent<Slider>().value * 100).ToString("F0") + "%";
        if (gameObject.GetComponent<Slider>().value == 0) {
            audioImage.overrideSprite = images[2];
        }
        else if (gameObject.GetComponent<Slider>().value == 1) {
            audioImage.overrideSprite = images[0];
        }
        else {
            audioImage.overrideSprite = images[1];
        }
    }

    public void updateVolume() {
        masterAudioControls.setVolume(gameObject.GetComponent<Slider>().value);
    }
}
