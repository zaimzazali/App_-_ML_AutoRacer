using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blinkingText : MonoBehaviour
{
    [SerializeField]
    private float timingInterval = 0f, timingFading = 0f;

    void Start()
    {
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
}
