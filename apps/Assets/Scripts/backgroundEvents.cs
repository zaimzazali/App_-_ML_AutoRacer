using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class backgroundEvents : MonoBehaviour
{
    [SerializeField]
    private Sprite[] images = null;

    [SerializeField]
    private GameObject canvasFader = null;  

    private List<GameObject> backgroundImages = new List<GameObject>();
    private float fromPoint = -50f, swingTiming = 24f;
    private int stateIndex = 0, imgIndex = 0;
    private float startTransitionAt = 12f, waitToTransition = 12f, fadingTiming = 2f;

    private void Awake() {
        backgroundImages.Add(gameObject.transform.GetChild(0).gameObject); // Back
        backgroundImages.Add(gameObject.transform.GetChild(1).gameObject); // Front

        gameObject.GetComponent<RectTransform>().localPosition = new Vector3(fromPoint, 0f, 0f);
        
        sceneFader sceneFader = canvasFader.transform.GetChild(0).gameObject.GetComponent<sceneFader>();
    }

    public IEnumerator initFunctions(float timeToWait) {
        yield return new WaitForSeconds(timeToWait);
        LeanTween.moveX(gameObject.GetComponent<RectTransform>(), -1*fromPoint, swingTiming).setEaseLinear().setLoopPingPong();
        InvokeRepeating("backgroundTransition", startTransitionAt, waitToTransition);

        backgroundImages[1].GetComponent<Image>().overrideSprite = images[imgIndex];
        backgroundImages[0].GetComponent<Image>().overrideSprite = images[getNextImageIndex()];
    }

    private void backgroundTransition() {
        Image theImg = backgroundImages[1].GetComponent<Image>();
        float to;

        if (stateIndex == 1) {
            // To hide
            stateIndex = 0;
            to = 1f;
        }
        else {
            // To show
            stateIndex = 1;
            to = 0f;
        }

        LeanTween.value(backgroundImages[1], stateIndex, to, fadingTiming).setOnUpdate((float value) =>
        {
            Color theColor = theImg.color;
            theColor.a = value;
            theImg.color = theColor;

            if (to == 0f && value == 0f) {
                backgroundImages[1].GetComponent<Image>().overrideSprite = images[getNextImageIndex()];
            }

            if (to == 1f && value == 1f) {
                backgroundImages[0].GetComponent<Image>().overrideSprite = images[getNextImageIndex()];
            }
            
        });
    }

    private int getNextImageIndex() {
        if (imgIndex == images.Length-1) {
            imgIndex = 0;
        } else {
            imgIndex += 1; 
        }
        
        return imgIndex;
    }
}
