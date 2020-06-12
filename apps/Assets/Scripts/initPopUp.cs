using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initPopUp : MonoBehaviour
{
    public GameObject targetCanvas;
    public GameObject initialObj;
    public GameObject newObj;

    private float positive_to = 1500f;
    private float negative_to = -1500f;
    private float timing = 0.5f;

    public void nextDiv() {
        newObj.SetActive(true);
        targetCanvas.SetActive(true);
        LeanTween.moveX(initialObj.GetComponent<RectTransform>(), positive_to, timing).setEaseInOutCubic();
        LeanTween.moveX(newObj.GetComponent<RectTransform>(), 0, timing).setEaseInOutCubic();
        StartCoroutine("deactivateElement", initialObj);
    }

    public void previousDiv() {
        newObj.SetActive(true);
        LeanTween.moveX(newObj.GetComponent<RectTransform>(), 0, timing).setEaseInOutCubic();
        LeanTween.moveX(initialObj.GetComponent<RectTransform>(), negative_to, timing).setEaseInOutCubic();
        StartCoroutine("deactivateElement", targetCanvas);
        StartCoroutine("deactivateElement", initialObj);
    }

    IEnumerator deactivateElement(GameObject thisObj) {
        yield return new WaitForSeconds(timing);
        thisObj.SetActive(false);
    }
}

