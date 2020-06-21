using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class underlineEvents : MonoBehaviour
{
    private float transitionTiming = 0.1f;

    private float underLineHeight = 0f;

    private GameObject parent = null;

    private void Awake() {
        parent = gameObject.transform.parent.parent.gameObject;
        underLineHeight = parent.transform.Find("Underline").gameObject.GetComponent<RectTransform>().sizeDelta.y;
    }

    public void stretchLine() {
        gameObject.GetComponent<AudioSource>().Play();
        float maxWidth = gameObject.transform.parent.parent.parent.gameObject.GetComponent<RectTransform>().sizeDelta.x;
        LeanTween.value(gameObject, 0f, maxWidth, transitionTiming).setOnUpdate((float val) =>
        {
            parent = gameObject.transform.parent.gameObject;
            parent.transform.parent.Find("Underline").gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(val, underLineHeight);
        });
    }
    
    public void shrinkLine() {
        float currentWidth = parent.transform.parent.Find("Underline").gameObject.GetComponent<RectTransform>().sizeDelta.x;
        LeanTween.value(gameObject, currentWidth, 0f, transitionTiming).setOnUpdate((float val) =>
        {
            parent = gameObject.transform.parent.parent.gameObject;
            parent.transform.Find("Underline").gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(val, underLineHeight);
        });
    }
}
