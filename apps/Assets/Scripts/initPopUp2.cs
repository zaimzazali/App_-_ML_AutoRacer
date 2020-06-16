using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class initPopUp2 : MonoBehaviour
{
    
    [SerializeField]
    private GameObject canvas_popup_00 = null, canvas_popup_01 = null;

    [SerializeField]
    private float openingTiming = 1.3f, closingTiming = 0.5f;

    private GameObject panel_parent = null, panel_blur = null, panel_child = null;
    private GameObject popObj = null;

    private Color color_red = new Color32(191, 47, 56, 30);
    private Color color_normal = new Color(0f, 0f, 0f, 0f);

    private void Start() {
        panel_parent = canvas_popup_00.transform.Find("Panel").gameObject;
        panel_blur = canvas_popup_01.transform.Find("Panel_Blur").gameObject;
        panel_child = canvas_popup_01.transform.Find("Panel").gameObject;
    }

    public void displayPopUp_One_Button(string message, bool isError) {
        if (isError) {
            panel_child.GetComponent<Image>().color = color_red;
        } else {
            panel_child.GetComponent<Image>().color = color_normal;
        }

        if (canvas_popup_00.activeSelf) {
            panel_parent.GetComponent<Image>().enabled = false;
            panel_blur.SetActive(true);
        }

        panel_child.SetActive(true);

        popObj = canvas_popup_01.transform.GetChild(1).GetChild(1).gameObject;
        popObj.transform.GetChild(0).GetChild(0).Find("Text").GetComponent<Text>().text = message;

        popObj.transform.localScale = Vector3.zero;
        popObj.SetActive(true);

        canvas_popup_01.SetActive(true);

        LeanTween.scale(popObj, new Vector3(1f,1f,1f), openingTiming).setEaseOutElastic();
    }

    public void closePopUp_One_Button() {
        popObj = canvas_popup_01.transform.GetChild(1).GetChild(1).gameObject;
        LeanTween.scale(popObj, Vector3.zero, closingTiming).setEaseInBack();
        Invoke("hideAllObjects", closingTiming);
    }

    private void hideAllObjects() {
        canvas_popup_01.SetActive(false);
        popObj.SetActive(false);
        panel_child.SetActive(false);

        if (canvas_popup_00.activeSelf) {
            panel_parent.GetComponent<Image>().enabled = true;
            panel_blur.SetActive(false);
        }
    }
}
