using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class initPopUp : MonoBehaviour
{
    private enum type {
        horizontal, vertical
    }

    private enum direction {
        right, left, top, bottom
    }

    [SerializeField]
    private type animType = type.horizontal;

    [SerializeField]
    private direction directionTowards = direction.right;

    [SerializeField]
    private float to_horizontal = 0, to_vertical = 0, timing = 0.5f;

    [SerializeField]
    private GameObject initialObj = null, newObj = null, targetCanvas = null;

    private GameObjectSearcher GameObjectSearcher = null;
    private blinkingText blinkingText = null;

    private void Awake() {
        GameObjectSearcher = gameObject.GetComponent<GameObjectSearcher>();
        blinkingText = gameObject.GetComponent<blinkingText>();
    }

    public void nextDiv() {
        if (newObj != null) {
            setNewObjPosition();
            clearInputs();
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
                        // InvokeRepeating("blinkServerStatus", timing*2/3, 2f);
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
        clearInputs();

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

    private void clearInputs() {
        List<GameObject> theObj = null;

        theObj = GameObjectSearcher.getChildObjectsWithTag("input_field", newObj);
        foreach (GameObject obj in theObj) {
            obj.GetComponent<InputField>().text = "";
            obj.GetComponent<Any_Inputfield>().setNormal();
        }

        theObj = GameObjectSearcher.getChildObjectsWithTag("drop_down", newObj);
        foreach (GameObject obj in theObj) {
            obj.GetComponent<Dropdown>().value = 0;
            obj.GetComponent<Any_Dropdown>().setNormal();
        }

        theObj = GameObjectSearcher.getChildObjectsWithTag("input_field_tmp", newObj);
        foreach (GameObject obj in theObj) {
            obj.GetComponent<TMP_InputField>().text = "";
        }

        // For Registration Div
        if (newObj.name == "PopUp_Register") {
            GameObject theText = newObj.transform.Find("PopUp_Window/Holder_Body/Holder_01/Holder_Username/Holder_Content/Holder_Check_Result/Text").gameObject;
            theText.GetComponent<Text>().text = "";
        }
    }
}
