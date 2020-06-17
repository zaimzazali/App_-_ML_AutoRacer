using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class initPopUp2 : MonoBehaviour
{
    
    [SerializeField]
    private GameObject canvas_next = null;

    [SerializeField]
    private float openingTiming = 1.3f, closingTiming = 0.5f;

    private GameObject canvas_parent = null, panel_parent = null, panel_blur = null, panel_child = null;
    private GameObject popObj = null;

    private Color color_red = new Color32(191, 47, 56, 30);
    private Color color_normal = new Color(0f, 0f, 0f, 0f);

    private GameObjectSearcher GameObjectSearcher = null;

    private void Awake() {
        canvas_parent = GameObject.Find("Canvas_02").gameObject;

        panel_parent = canvas_parent.transform.Find("Panel").gameObject;
        panel_blur = canvas_next.transform.Find("Panel_Blur").gameObject;
        panel_child = canvas_next.transform.Find("Panel").gameObject;

        GameObjectSearcher = gameObject.GetComponent<GameObjectSearcher>();
    }

    public void displayPopUp_One_Button(string message, bool isError) {
        if (isError) {
            panel_child.GetComponent<Image>().color = color_red;
        } else {
            panel_child.GetComponent<Image>().color = color_normal;
        }

        if (canvas_parent.activeSelf) {
            panel_parent.GetComponent<Image>().enabled = false;
            panel_blur.SetActive(true);
        }

        panel_child.SetActive(true);

        popObj = canvas_next.transform.GetChild(1).GetChild(1).gameObject;
        popObj.transform.GetChild(0).GetChild(0).Find("Text").GetComponent<Text>().text = message;

        popObj.transform.localScale = Vector3.zero;
        popObj.SetActive(true);

        canvas_next.SetActive(true);

        LeanTween.scale(popObj, new Vector3(1f,1f,1f), openingTiming).setEaseOutElastic();
    }

    public void closePopUp_One_Button() {
        if (panel_child.GetComponent<Image>().color == color_normal) {
            Invoke("autoCloseDiv", closingTiming);
        }
        popObj = canvas_next.transform.GetChild(1).GetChild(1).gameObject;
        LeanTween.scale(popObj, Vector3.zero, closingTiming).setEaseInBack();
        Invoke("hideAllObjects", closingTiming);
    }

    private void autoCloseDiv() {
        List<GameObject> theObj = null;
        foreach(Transform child in canvas_parent.transform.GetChild(0).gameObject.transform) {
            if(child.gameObject.activeSelf) {
                theObj = GameObjectSearcher.getChildObjectsWithTag("button_close", child.gameObject);
                foreach (GameObject obj in theObj) {
                    obj.GetComponent<Button>().onClick.Invoke();
                }
            }
        }
    }

    private void hideAllObjects() {
        canvas_next.SetActive(false);
        popObj.SetActive(false);
        panel_child.SetActive(false);

        if (canvas_parent.activeSelf) {
            panel_parent.GetComponent<Image>().enabled = true;
            panel_blur.SetActive(false);
        }
    }
}
