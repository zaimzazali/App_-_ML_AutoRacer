using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class backgroundEvents : MonoBehaviour
{
    public float waitingTime = 0f;
    public float fromPoint = -5f;
    public float toPoint = 5f;
    public float swingTiming = 8f;
    public GameObject[] backgroundImages;
    public Sprite[] images;

    private int stateIndex = 0;
    private int imgIndex = 0;
    public float startTransitionAt = 0f;
    public float waitToTransition = 0f;
    public float fadingTiming = 1f;

    void Start()
    {
        gameObject.GetComponent<RectTransform>().localPosition = new Vector3(fromPoint, 0f, 0f);
        Invoke("initFunctions", waitingTime);
    }

    private void initFunctions() {
        LeanTween.moveX(gameObject.GetComponent<RectTransform>(), toPoint, swingTiming).setEaseLinear().setLoopPingPong();
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

            if (from == 0f && val == 0f) {
                backgroundImages[1].GetComponent<Image>().overrideSprite = images[getNextImageIndex()];
            }

            if (from == 1f && val == 1f) {
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
