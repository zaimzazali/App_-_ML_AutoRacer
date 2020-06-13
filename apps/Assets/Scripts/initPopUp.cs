using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class initPopUp : MonoBehaviour
{
    public enum type
    {
        horizontal,
        vertical
    }
    public enum direction
    {
        right,
        left,
        top,
        down
    }
    public GameObject targetCanvas;
    public GameObject initialObj;
    public GameObject newObj;
    public GameObject serverStatusHolder;

    private float to_horizontal = 1500f;
    private float to_vertical = 1000f;
    private float timing = 0.5f;

    public type animType;
    public direction animDirection;

    public void nextDiv() {
        if (animDirection == direction.right) {
            newObj.GetComponent<RectTransform>().localPosition = new Vector3(-1*to_horizontal, 0f, 0f);       
        } else if (animDirection == direction.left) {
            newObj.GetComponent<RectTransform>().localPosition = new Vector3(to_horizontal, 0f, 0f);       
        }

        targetCanvas.SetActive(true);

        switch (animType)
        {
            case type.horizontal: 
                newObj.SetActive(true);

                if (animDirection == direction.right) {
                    LeanTween.moveX(initialObj.GetComponent<RectTransform>(), to_horizontal, timing).setEaseInOutCubic();
                    LeanTween.moveX(newObj.GetComponent<RectTransform>(), 0, timing).setEaseInOutCubic();     
                } else if (animDirection == direction.left) {
                    LeanTween.moveX(initialObj.GetComponent<RectTransform>(), -1*to_horizontal, timing).setEaseInOutCubic();
                    LeanTween.moveX(newObj.GetComponent<RectTransform>(), 0, timing).setEaseInOutCubic();   
                }
                
                break;
            case type.vertical: 
                LeanTween.moveY(initialObj.GetComponent<RectTransform>(), to_vertical, timing).setEaseInOutCubic();
                InvokeRepeating("blinkServerStatus", timing*2/3, 2f);
                break;
            default:
                // Do Nothing
                break;
        }
        
        StartCoroutine("deactivateElement", initialObj);
    }

    public void previousDiv() {
        newObj.SetActive(true);

        switch (animType)
        {
            case type.horizontal: 
                if (animDirection == direction.right) {
                    LeanTween.moveX(newObj.GetComponent<RectTransform>(), 0, timing).setEaseInOutCubic();
                    LeanTween.moveX(initialObj.GetComponent<RectTransform>(), -1*to_horizontal, timing).setEaseInOutCubic();   
                } else if (animDirection == direction.left) {
                    LeanTween.moveX(newObj.GetComponent<RectTransform>(), 0, timing).setEaseInOutCubic();
                    LeanTween.moveX(initialObj.GetComponent<RectTransform>(), to_horizontal, timing).setEaseInOutCubic();  
                }
                
                break;
            case type.vertical: 
                // Do Nothing
                break;
            default:
                // Do Nothing
                break;
        }
        
        StartCoroutine("deactivateElement", targetCanvas);
        StartCoroutine("deactivateElement", initialObj);
    }

    IEnumerator deactivateElement(GameObject thisObj) {
        yield return new WaitForSeconds(timing);
        thisObj.SetActive(false);
    }

    private void blinkServerStatus() {
        Text theText;
        float to = 0f, from = 0f;
        bool proceed = false;

        if (serverStatusHolder.activeSelf == false) {
            serverStatusHolder.SetActive(true);
        }

        theText = serverStatusHolder.transform.GetChild(0).GetComponent<Text>();

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
            LeanTween.value(serverStatusHolder, from, to, 1f).setOnUpdate((float val) =>
            {
                Color theColor = theText.color;
                theColor.a = val;
                theText.color = theColor;
            });
        }
    }
}

