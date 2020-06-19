using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sceneFader : MonoBehaviour
{
    private float waitingBeforeProceed = 0.5f;
    private float timingFading = 0.5f;
    private float buffer = 0f;

    private GameObject parentObj = null;
    private Image panelImg = null;

    private void Awake() {
        parentObj = gameObject.transform.parent.gameObject;
        panelImg = gameObject.GetComponent<Image>();
    }

    private void Start() {
        StartCoroutine("fadeOut");
    }

    // Open Scene
    public IEnumerator fadeOut() {
        Color theColor;

        yield return new WaitForSeconds(waitingBeforeProceed);

        // Set alpha to 1f
        theColor = panelImg.color;
        theColor.a = 1f;
        panelImg.color = theColor;

        parentObj.SetActive(true);

        LeanTween.value(gameObject, 1f, 0f, timingFading).setOnUpdate((float val) =>
        {
            theColor = panelImg.color;
            theColor.a = val;
            panelImg.color = theColor;

            if (val == 0f) {
                LeanTween.cancel(gameObject);
                parentObj.SetActive(false);
            }
        });
    }

    // Close Scene
    public IEnumerator fadeIn() {
        Color theColor;

        yield return new WaitForSeconds(waitingBeforeProceed);

        // Set alpha to 0f
        theColor = panelImg.color;
        theColor.a = 0f;
        panelImg.color = theColor;

        parentObj.SetActive(true);

        LeanTween.value(gameObject, 0f, 1f, timingFading).setOnUpdate((float val) =>
        {
            theColor = panelImg.color;
            theColor.a = val;
            panelImg.color = theColor;

            if (val == 1f) {
                LeanTween.cancel(gameObject);
            }
        });
    }

    public float getTotalWaitingTime() {
        return waitingBeforeProceed + timingFading + buffer;
    }
}
