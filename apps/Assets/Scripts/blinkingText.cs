using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blinkingText : MonoBehaviour
{
    private float timingInterval = 2f, timingFading = 1f;

    private Color color_normal = new Color32(214, 214, 214, 0);

    public void startBlinking() {
        InvokeRepeating("blinkServerStatus", 0f, timingInterval);
    }

    private void blinkServerStatus() {
        Text theText;
        float to = 0f, from = 0f;
        bool proceed = false;

        theText = gameObject.GetComponent<Text>();

        if (theText.color.a == 1f) {
            proceed = true;
            from  = 1f;
            to = 0f;
        }
        else if (theText.color.a == 0f) {
            proceed = true;
            from  = 0f;
            to = 1f;
        }

        if (proceed) {
            LeanTween.value(gameObject, from, to, timingFading).setOnUpdate((float val) =>
            {
                Color theColor = theText.color;
                theColor.a = val;
                theText.color = theColor;
            });
        }
    }

    public void stopBlinking() {
        LeanTween.cancel(gameObject);
        CancelInvoke("blinkServerStatus");
        gameObject.GetComponent<Text>().color = color_normal;
    }
}
