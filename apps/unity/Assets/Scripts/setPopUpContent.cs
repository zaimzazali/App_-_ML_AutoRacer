using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setPopUpContent : MonoBehaviour
{
    [SerializeField]
    private float contentId = 0;
    [SerializeField]
    private GameObject popUpObj = null;

    private GameObject btnYes = null;

    private void Awake() {
        btnYes = popUpObj.transform.Find("PopUp_Window/Holder_Buttons/Holder_Yes/Button_Yes").gameObject;
    }

    public void setContentText() {
        string myText = null;
        btnYes.GetComponent<Button>().onClick.RemoveAllListeners();

        switch (contentId)
        {
            case 0:
                myText = "Are you sure you want to\nquit the game?";
                btnYes.GetComponent<Button>().onClick.AddListener(gameObject.GetComponent<exitGame>().quitGame);
                break;
            case 1:
                myText = "Are you sure you want to\nlogout?";
                btnYes.GetComponent<Button>().onClick.AddListener(toChangeScene);
                PlayerPrefs.SetInt("nextSceneIndex", 1);
                break;
            default:
                // Do Nothing
                break;
        }

        popUpObj.transform.Find("PopUp_Window/Holder_Content/Text").gameObject.GetComponent<Text>().text = myText;
    }

    private void toChangeScene() {
        StartCoroutine("prepareToChangeScene");
    }

    private IEnumerator prepareToChangeScene() {
        yield return new WaitForEndOfFrame();
        GameObject.Find("Background_Music").gameObject.tag = "dont_destroy";
        GameObject.Find("Cursor").gameObject.tag = "dont_destroy";
        GameObject.Find("Player_Data").gameObject.tag = "dont_destroy";
        StartCoroutine(btnYes.GetComponent<goNextScene>().changeScene());
    }
}
