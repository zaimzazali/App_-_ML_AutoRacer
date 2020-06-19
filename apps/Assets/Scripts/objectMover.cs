using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class objectMover : MonoBehaviour
{
    private gameObjectSearcher gameObjectSearcher = null;

    private void Awake() {
        gameObjectSearcher = gameObject.GetComponent<gameObjectSearcher>();
    }

    public void goNextState(GameObject[] gameObjects, float[] floats, string animTypeStr, bool[] bools) {
        GameObject canvas = gameObjects[0], objToMove = gameObjects[1];
        float from_position = floats[0], to_position = floats[1], timing = floats[2];
        bool toSetInitialPosition = bools[0], toClearInputs = bools[1], toHideAfter = bools[2];

        objToMove.SetActive(true);
        if (toSetInitialPosition) {
            setObjPosition(objToMove, from_position, animTypeStr);
        }
        if (toClearInputs) {
            clearInputs(objToMove);
        }
        canvas.SetActive(true);
        animateDivs(objToMove, to_position, animTypeStr, timing);
        if (toHideAfter) {
            StartCoroutine(deactivateElement(objToMove, timing));
        }
    }   

    private void setObjPosition(GameObject objToMove , float from_position, string animTypeStr) {
        switch (animTypeStr) {
            case "horizontal":
                objToMove.GetComponent<RectTransform>().localPosition = new Vector3(from_position, 0f, 0f);  
                break;

            case "vertical":
                objToMove.GetComponent<RectTransform>().localPosition = new Vector3(0f, from_position, 0f); 
                break;

            default:
                // Do Nothing
                break;
        }
    }

    private void clearInputs(GameObject objToMove) {
        List<GameObject> theObj = null;

        theObj = gameObjectSearcher.getChildObjectsWithTag("input_field", objToMove);
        foreach (GameObject obj in theObj) {
            obj.GetComponent<InputField>().text = "";
            obj.GetComponent<Any_Inputfield>().setNormal();
        }

        theObj = gameObjectSearcher.getChildObjectsWithTag("drop_down", objToMove);
        foreach (GameObject obj in theObj) {
            obj.GetComponent<Dropdown>().value = 0;
            obj.GetComponent<Any_Dropdown>().setNormal();
        }

        theObj = gameObjectSearcher.getChildObjectsWithTag("input_field_tmp", objToMove);
        foreach (GameObject obj in theObj) {
            obj.GetComponent<TMP_InputField>().text = "";
        }

        // For Registration Div
        if (objToMove.name == "PopUp_Register") {
            GameObject theText = objToMove.transform.Find("PopUp_Window/Holder_Body/Holder_01/Holder_Username/Holder_Content/Holder_Check_Result/Text").gameObject;
            theText.GetComponent<Text>().text = "";
        }
    }

    public void toClearInput(GameObject obj) {
        clearInputs(obj);
    }

    private void animateDivs(GameObject objToMove , float to_position, string animTypeStr, float timing) {
        switch (animTypeStr) {
            case "horizontal":
                LeanTween.moveX(objToMove.GetComponent<RectTransform>(), to_position, timing).setEaseInOutCubic();
                break;

            case "vertical":
                LeanTween.moveY(objToMove.GetComponent<RectTransform>(), to_position, timing).setEaseInOutCubic(); 
                break;

            default:
                // Do Nothing
                break;
        }
    }

    IEnumerator deactivateElement(GameObject objToMove, float timing) {
        yield return new WaitForSeconds(timing);
        objToMove.SetActive(false);

        if (objToMove.transform.parent.parent.gameObject.name != "Canvas_01") {
            objToMove.transform.parent.parent.gameObject.SetActive(false);
        }
    }
}
