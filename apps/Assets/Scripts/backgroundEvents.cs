using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class backgroundEvents : MonoBehaviour
{
    private initSound initSoundScript;

    [SerializeField]
    private float fromPoint = 0f, swingTiming = 0f;

    [SerializeField]
    public GameObject[] backgroundImages = null;

    [SerializeField]
    private Sprite[] images = null;

    private int stateIndex = 0;
    private int imgIndex = 0;

    [SerializeField]
    private float startTransitionAt = 0f, waitToTransition = 0f, fadingTiming = 0f;

    private void Start()
    {
        initSoundScript = GameObject.Find("Background_Music").GetComponent<initSound>();

        gameObject.GetComponent<RectTransform>().localPosition = new Vector3(fromPoint, 0f, 0f);
        Invoke("initFunctions", initSoundScript.getWaitingTime());
    }

    private void initFunctions() {
        LeanTween.moveX(gameObject.GetComponent<RectTransform>(), -1*fromPoint, swingTiming).setEaseLinear().setLoopPingPong();
        InvokeRepeating("backgroundTransition", startTransitionAt, waitToTransition);

        backgroundImages[1].GetComponent<Image>().overrideSprite = images[imgIndex];
        backgroundImages[0].GetComponent<Image>().overrideSprite = images[getNextImageIndex()];
    }

    private void backgroundTransition() {
        Image theImg = backgroundImages[1].GetComponent<Image>();
        float to, from;

        if (stateIndex == 1) {
            // To hide
            stateIndex = 0;
            from = stateIndex;
            to = 1f;
        }
        else {
            // To show
            stateIndex = 1;
            from = stateIndex;
            to = 0f;
        }

        LeanTween.value(backgroundImages[1], from, to, fadingTiming).setOnUpdate((float val) =>
        {
            Color theColor = theImg.color;
            theColor.a = val;
            theImg.color = theColor;

            if (to == 0f && val == 0f) {
                backgroundImages[1].GetComponent<Image>().overrideSprite = images[getNextImageIndex()];
            }

            if (to == 1f && val == 1f) {
                backgroundImages[0].GetComponent<Image>().overrideSprite = images[getNextImageIndex()];
            }
            
        });
    }

    private int getNextImageIndex() {
        if (imgIndex == images.Length-1) {
            imgIndex = 0;
        }
        else {
            imgIndex += 1; 
        }
        return imgIndex;
    }
}
