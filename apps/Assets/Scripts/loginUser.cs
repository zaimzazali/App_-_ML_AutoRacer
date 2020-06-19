using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SimpleJSON;

public class loginUser : MonoBehaviour
{
    private GameObject parentObj = null;
    private TMP_InputField inputfield_username = null;
    private TMP_InputField inputfield_password = null;

    private Any_Colours Any_Colours = new Any_Colours();

    private controlCanvas01 controlCanvas01 = null;
    private controlCanvas03 controlCanvas03 = null;
    private serverAPI serverAPI = null;
    private waitForServer waitForServer = null;

    [SerializeField]
    private goNextScene goNextScene = null;

    private void Awake() {
        parentObj = gameObject.transform.parent.parent.gameObject;
        inputfield_username = parentObj.transform.Find("Holder_Input_00/InputField_Login_Username").gameObject.GetComponent<TMP_InputField>();
        inputfield_password = parentObj.transform.Find("Holder_Input_01/InputField_Login_Password").gameObject.GetComponent<TMP_InputField>();
    
        controlCanvas01 = gameObject.GetComponent<controlCanvas01>();
        controlCanvas03 = gameObject.GetComponent<controlCanvas03>();
        serverAPI = gameObject.GetComponent<serverAPI>();
        waitForServer = gameObject.GetComponent<waitForServer>();
    }

    public void tryLoginUser() {
        string username, password;

        username = inputfield_username.text.Trim();
        password = inputfield_password.text.Trim();

        if (username == "" || password == "") {
            return;
        }

        controlCanvas01.initNext();
        waitForServer.showWaitingText();

        // Check credentials in Database
        StartCoroutine(serverAPI.initLogin(username, password, result => {
            JSONNode jsonData = null;
            try {
                jsonData = JSON.Parse(result);
            } catch (System.Exception) {
                controlCanvas03.displayPopUp_One_Button("There was an error occurred while checking for the credentials.\nPlease try again.", true);
                return;
            }
            
            string signal = (string)jsonData["signal"];

            if (signal == "OK") {
                PlayerPrefs.SetInt("nextSceneIndex", 3);
                StartCoroutine(prepareToChangeScene());
            } else {
                StartCoroutine(waitForServer.hideWaitingText(callback => {
                    if (signal == "not active") {
                        controlCanvas03.displayPopUp_One_Button("Your account is deactivated!\nPlease reach us if you think this was an error.", true);
                    } else if (signal == "not exist") {
                        controlCanvas03.displayPopUp_One_Button("We cannot find your record in our database!", true);
                    } else if (signal == "invalid") {
                        controlCanvas03.displayPopUp_One_Button("Invalid credentials!", true);
                    }
                }));
            }
        })); 
    }

    private IEnumerator prepareToChangeScene() {
        yield return new WaitForSeconds(1);
        StartCoroutine(goNextScene.changeScene());
    }
}
