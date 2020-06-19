using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controlCanvas03 : MonoBehaviour
{
    
    [SerializeField]
    private GameObject canvas_back = null, canvas_front = null;
    private GameObject panel_current = null, panel_front_blur = null, panel_front = null, popObj = null;

    private float openingTiming = 1.3f, closingTiming = 0.5f;

    private gameObjectSearcher gameObjectSearcher = null;

    private Any_Colours Any_Colours = new Any_Colours();

    private void Awake() {
        panel_current = canvas_back.transform.Find("Panel").gameObject;
        
        panel_front_blur = canvas_front.transform.Find("Panel_Blur").gameObject;
        panel_front = canvas_front.transform.Find("Panel").gameObject;

        gameObjectSearcher = gameObject.GetComponent<gameObjectSearcher>();
    }

    public void displayPopUp_One_Button(string message, bool isError) {
        if (isError) {
            panel_front.GetComponent<Image>().color = Any_Colours.get_Color_Panel_Red();
        } else {
            panel_front.GetComponent<Image>().color = Any_Colours.get_Color_Panel_Clear();
        }

        if (canvas_back.activeSelf) {
            panel_current.GetComponent<Image>().enabled = false;
            panel_front_blur.SetActive(true);
        }

        panel_front.SetActive(true);

        popObj = canvas_front.transform.GetChild(1).GetChild(1).gameObject;
        popObj.transform.GetChild(0).GetChild(0).Find("Text").GetComponent<Text>().text = message;

        popObj.transform.localScale = Vector3.zero;
        popObj.SetActive(true);

        canvas_front.SetActive(true);

        LeanTween.scale(popObj, new Vector3(1f,1f,1f), openingTiming).setEaseOutElastic();
    }

    public void closePopUp_One_Button() {
        if (panel_front.GetComponent<Image>().color == Any_Colours.get_Color_Panel_Clear()) {
            Invoke("autoCloseDiv", closingTiming);
        }

        if (!canvas_back.activeSelf) {
            Invoke("bringBackLoginDiv", closingTiming*2/3);
        }

        popObj = canvas_front.transform.GetChild(1).GetChild(1).gameObject;
        LeanTween.scale(popObj, Vector3.zero, closingTiming).setEaseInBack();
        Invoke("hideAllObjects", closingTiming);
    }

    private void bringBackLoginDiv() {
        LeanTween.moveY(GameObject.Find("Holder_Login").GetComponent<RectTransform>(), 0f, closingTiming).setEaseInOutCubic();
    }

    private void autoCloseDiv() {
        List<GameObject> theObj = null;
        foreach(Transform child in canvas_back.transform.GetChild(0).gameObject.transform) {
            if(child.gameObject.activeSelf) {
                theObj = gameObjectSearcher.getChildObjectsWithTag("button_close", child.gameObject);
                foreach (GameObject obj in theObj) {
                    obj.GetComponent<Button>().onClick.Invoke();
                }
            }
        }
    }

    private void hideAllObjects() {
        canvas_front.SetActive(false);
        popObj.SetActive(false);
        panel_front.SetActive(false);

        if (canvas_back.activeSelf) {
            panel_current.GetComponent<Image>().enabled = true;
            panel_front_blur.SetActive(false);
        }
    }
}
