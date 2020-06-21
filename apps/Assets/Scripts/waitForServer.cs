using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class waitForServer : MonoBehaviour
{
    [SerializeField]
    private GameObject canvas_parent = null, canvas_next = null;

    private GameObject panel_parent = null, panel_blur = null, panel_child = null, textObj = null;
    private Any_Colours Any_Colours = new Any_Colours();
    private gameObjectSearcher gameObjectSearcher = null;
    private blinkingText blinkingText = null;

    private void Awake() {
        panel_parent = canvas_parent.transform.Find("Panel").gameObject;

        panel_blur = canvas_next.transform.Find("Panel_Blur").gameObject;
        panel_child = canvas_next.transform.Find("Panel").gameObject;

        textObj = panel_child.transform.Find("Wait_Server").gameObject;
        blinkingText = textObj.GetComponent<blinkingText>();

        gameObjectSearcher = gameObject.GetComponent<gameObjectSearcher>();
    }

    public void showWaitingText() {
        panel_child.GetComponent<Image>().color = Any_Colours.get_Color_Panel_Red();

        if (canvas_parent.activeSelf) {
            panel_parent.GetComponent<Image>().enabled = false;
            panel_blur.SetActive(true);
        }

        panel_child.SetActive(true);

        textObj.SetActive(true);
        blinkingText.startBlinking();

        canvas_next.SetActive(true);
    }

    public IEnumerator hideWaitingText(System.Action<string> callback) {
        yield return new WaitForSeconds(2);

        canvas_next.SetActive(false);

        blinkingText.stopBlinking();
        textObj.SetActive(false);

        panel_child.SetActive(false);

        if (canvas_parent.activeSelf) {
            panel_blur.SetActive(false);
            panel_parent.GetComponent<Image>().enabled = true;
        }

        callback("DONE");
    }
}
