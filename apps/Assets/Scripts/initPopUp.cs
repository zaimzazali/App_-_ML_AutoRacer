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
    private GameObject targetCanvas = null, initialObj = null, newObj = null;

    [SerializeField]
    private float to_horizontal = 0, to_vertical = 0, timing = 0f;

    [SerializeField]
    private type animType = type.horizontal;

    [SerializeField]
    private direction animDirectionTowards = direction.right;

    public void nextDiv() {

        if (newObj != null) {
            newObj.SetActive(true);

            // Setup Position
            switch (animDirectionTowards) {
                case direction.right:
                    newObj.GetComponent<RectTransform>().localPosition = new Vector3(-1*to_horizontal, 0f, 0f);  
                    break;

                case direction.left:
                    newObj.GetComponent<RectTransform>().localPosition = new Vector3(to_horizontal, 0f, 0f); 
                    break;

                case direction.bottom:
                    newObj.GetComponent<RectTransform>().localPosition = new Vector3(0f, to_vertical, 0f); 
                    break;

                default:
                    // Do Nothing
                    break;
            }
        }

        targetCanvas.SetActive(true);

        // Animate
        switch (animType)
        {
            case type.horizontal: 
                if (animDirectionTowards == direction.right) {
                    LeanTween.moveX(initialObj.GetComponent<RectTransform>(), to_horizontal, timing).setEaseInOutCubic();
                    LeanTween.moveX(newObj.GetComponent<RectTransform>(), 0, timing).setEaseInOutCubic();     
                } else if (animDirectionTowards == direction.left) {
                    LeanTween.moveX(initialObj.GetComponent<RectTransform>(), -1*to_horizontal, timing).setEaseInOutCubic();
                    LeanTween.moveX(newObj.GetComponent<RectTransform>(), 0, timing).setEaseInOutCubic();   
                }
                break;

            case type.vertical: 
                if (animDirectionTowards == direction.bottom) {
                    LeanTween.moveY(initialObj.GetComponent<RectTransform>(), -1*to_vertical, timing).setEaseInOutCubic();
                    LeanTween.moveY(newObj.GetComponent<RectTransform>(), 0, timing).setEaseInOutCubic();     
                } else if (animDirectionTowards == direction.top) {
                    if (newObj == null) {
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
                if (animDirectionTowards == direction.bottom) {
                    LeanTween.moveY(newObj.GetComponent<RectTransform>(), 0, timing).setEaseInOutCubic();
                    LeanTween.moveY(initialObj.GetComponent<RectTransform>(), to_vertical, timing).setEaseInOutCubic();   
                } else if (animDirectionTowards == direction.top) {
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

