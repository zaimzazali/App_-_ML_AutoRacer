using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class initPopUp : MonoBehaviour
{
    private enum type
    {
        horizontal,
        vertical
    }

    private enum direction
    {
        right,
        left,
        top,
        down
    }

    [SerializeField]
    private GameObject targetCanvas = null, initialObj = null, newObj = null, serverStatusHolder = null;

    [SerializeField]
    private float to_horizontal = 0, to_vertical = 0, timing = 0f;

    [SerializeField]
    private type animType = type.horizontal;

    [SerializeField]
    private direction animDirectionTowards = direction.right;

    public void nextDiv() {

        switch (animDirectionTowards) {
            case direction.right:
                newObj.GetComponent<RectTransform>().localPosition = new Vector3(-1*to_horizontal, 0f, 0f);  
                break;

            case direction.left:
                newObj.GetComponent<RectTransform>().localPosition = new Vector3(to_horizontal, 0f, 0f); 
                break;

            default:
                // Do Nothing
                break;
        }

        targetCanvas.SetActive(true);

        switch (animType)
        {
            case type.horizontal: 
                newObj.SetActive(true);

                if (animDirectionTowards == direction.right) {
                    LeanTween.moveX(initialObj.GetComponent<RectTransform>(), to_horizontal, timing).setEaseInOutCubic();
                    LeanTween.moveX(newObj.GetComponent<RectTransform>(), 0, timing).setEaseInOutCubic();     
                } else if (animDirectionTowards == direction.left) {
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
                if (animDirectionTowards == direction.right) {
                    LeanTween.moveX(newObj.GetComponent<RectTransform>(), 0, timing).setEaseInOutCubic();
                    LeanTween.moveX(initialObj.GetComponent<RectTransform>(), -1*to_horizontal, timing).setEaseInOutCubic();   
                } else if (animDirectionTowards == direction.left) {
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

