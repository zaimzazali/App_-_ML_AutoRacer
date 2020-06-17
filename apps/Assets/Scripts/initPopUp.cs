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
        bottom
    }

    [SerializeField]
    private type animType = type.horizontal;

    [SerializeField]
    private direction directionTowards = direction.right;

    [SerializeField]
    private float to_horizontal = 0, to_vertical = 0, timing = 0.5f;

    [SerializeField]
    private GameObject initialObj = null, newObj = null;
    private GameObject targetCanvas = null;

    private void Awake() {
        targetCanvas = newObj.transform.parent.parent.gameObject;
    }  

    public void nextDiv() {
        if (newObj != null) {
            setNewObjPosition();
        }
        targetCanvas.SetActive(true);
        animateDivs();
        StartCoroutine("deactivateElement", initialObj);
    }

    private void setNewObjPosition() {
        newObj.SetActive(true);
        switch (directionTowards) {
            case direction.right:
                newObj.GetComponent<RectTransform>().localPosition = new Vector3(-1*to_horizontal, 0f, 0f);  
                break;

            case direction.left:
                newObj.GetComponent<RectTransform>().localPosition = new Vector3(to_horizontal, 0f, 0f); 
                break;

            case direction.bottom:
                newObj.GetComponent<RectTransform>().localPosition = new Vector3(0f, to_vertical, 0f); 
                break;

            case direction.top:
                newObj.GetComponent<RectTransform>().localPosition = new Vector3(0f, -1*to_vertical, 0f); 
                break;

            default:
                // Do Nothing
                break;
        }
    }

    private void animateDivs() {
        switch (animType)
        {
            case type.horizontal: 
                // Right
                if (directionTowards == direction.right) {
                    LeanTween.moveX(initialObj.GetComponent<RectTransform>(), to_horizontal, timing).setEaseInOutCubic();
                    LeanTween.moveX(newObj.GetComponent<RectTransform>(), 0, timing).setEaseInOutCubic();     
                } else {
                // Left
                    LeanTween.moveX(initialObj.GetComponent<RectTransform>(), -1*to_horizontal, timing).setEaseInOutCubic();
                    LeanTween.moveX(newObj.GetComponent<RectTransform>(), 0, timing).setEaseInOutCubic();   
                }
                break;

            case type.vertical: 
                // Bottom
                if (directionTowards == direction.bottom) {
                    LeanTween.moveY(initialObj.GetComponent<RectTransform>(), -1*to_vertical, timing).setEaseInOutCubic();
                    LeanTween.moveY(newObj.GetComponent<RectTransform>(), 0, timing).setEaseInOutCubic();     
                } else {
                // Top
                    if (newObj == null) {
                        // Server Status
                        LeanTween.moveY(initialObj.GetComponent<RectTransform>(), to_vertical, timing).setEaseInOutCubic();
                        InvokeRepeating("blinkServerStatus", timing*2/3, 2f);
                    } else {
                        LeanTween.moveY(initialObj.GetComponent<RectTransform>(), to_vertical, timing).setEaseInOutCubic();
                        LeanTween.moveY(newObj.GetComponent<RectTransform>(), 0, timing).setEaseInOutCubic();  
                    } 
                }
                break;

            default:
                // Do Nothing
                break;
        }
    }

    public void previousDiv() {
        newObj.SetActive(true);

        switch (animType)
        {
            case type.horizontal: 
                // Right
                if (directionTowards == direction.right) {
                    LeanTween.moveX(newObj.GetComponent<RectTransform>(), 0, timing).setEaseInOutCubic();
                    LeanTween.moveX(initialObj.GetComponent<RectTransform>(), -1*to_horizontal, timing).setEaseInOutCubic();   
                } else {
                // Left
                    LeanTween.moveX(newObj.GetComponent<RectTransform>(), 0, timing).setEaseInOutCubic();
                    LeanTween.moveX(initialObj.GetComponent<RectTransform>(), to_horizontal, timing).setEaseInOutCubic();  
                }
                break;

            case type.vertical: 
                // Bottom
                if (directionTowards == direction.bottom) {
                    LeanTween.moveY(newObj.GetComponent<RectTransform>(), 0, timing).setEaseInOutCubic();
                    LeanTween.moveY(initialObj.GetComponent<RectTransform>(), to_vertical, timing).setEaseInOutCubic();   
                } else {
                // Top
                    LeanTween.moveY(newObj.GetComponent<RectTransform>(), 0, timing).setEaseInOutCubic();
                    LeanTween.moveY(initialObj.GetComponent<RectTransform>(), -1*to_vertical, timing).setEaseInOutCubic();  
                }
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

}

