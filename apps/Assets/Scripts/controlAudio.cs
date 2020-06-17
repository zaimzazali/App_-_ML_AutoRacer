using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class controlAudio : MonoBehaviour
{
    private masterAudioControls masterAudioControls;
    private Text theText;
    private Image audioImage;
    
    [SerializeField]
    private Sprite[] images = null;
    
    private void Awake() {
        masterAudioControls = GameObject.Find("Main Camera").GetComponent<masterAudioControls>();
        theText = GameObject.Find("Slider_Text").GetComponent<Text>();
        audioImage = GameObject.Find("Image_Speaker").GetComponent<Image>();

        gameObject.GetComponent<Slider>().value = masterAudioControls.getVolume();
        theText.text = (gameObject.GetComponent<Slider>().value * 100).ToString("F0") + "%";
    }

    public void updateText() {
        theText.text = (gameObject.GetComponent<Slider>().value * 100).ToString("F0") + "%";
        if (gameObject.GetComponent<Slider>().value == 0f) {
            audioImage.overrideSprite = images[2];
        } else if (gameObject.GetComponent<Slider>().value == 1f) {
            audioImage.overrideSprite = images[0];
        } else {
            audioImage.overrideSprite = images[1];
        }
    }

    public void updateVolume() {
        masterAudioControls.setVolume(gameObject.GetComponent<Slider>().value);
    }
}
