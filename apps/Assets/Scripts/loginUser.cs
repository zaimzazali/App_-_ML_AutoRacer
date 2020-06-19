using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class loginUser : MonoBehaviour
{
    private GameObject parentObj = null;
    private TMP_InputField inputfield_username = null;
    private TMP_InputField inputfield_password = null;

    private Any_Colours Any_Colours = new Any_Colours();

    private controlCanvas01 controlCanvas01 = null;
    private controlCanvas02 controlCanvas02 = null;
    private serverAPI serverAPI = null;
    private waitForServer waitForServer = null;

    private void Awake() {
        parentObj = gameObject.transform.parent.parent.gameObject;
        inputfield_username = parentObj.transform.Find("Holder_Input_00/InputField_Login_Username").gameObject.GetComponent<TMP_InputField>();
        inputfield_password = parentObj.transform.Find("Holder_Input_01/InputField_Login_Password").gameObject.GetComponent<TMP_InputField>();
    
        controlCanvas01 = gameObject.GetComponent<controlCanvas01>();
        controlCanvas02 = gameObject.GetComponent<controlCanvas02>();
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

        Debug.Log("A");

        // Check credentials in Database
        StartCoroutine(serverAPI.initLogin(username, password, result => {
            Debug.Log("B");
            StartCoroutine(waitForServer.hideWaitingText(callback => {
                Debug.Log("C");
                Debug.Log(result);
                /*
                if (result == "OK") {
                    // statusText.text = "Available";
                    // statusText.color = new Color(0f, 1f, 0f, 1f);
                } else if (result == "not active") {
                    // statusText.text = "Not Available";
                    // statusText.color = new Color(1f, 0f, 0f, 1f);
                } else if (result == "invalid") {
                    // statusText.text = "Not Available";
                    // statusText.color = new Color(1f, 0f, 0f, 1f);
                } else {
                    // Error
                    controlCanvas02.displayPopUp_One_Button("There was an error occurred while checking for the username.\nPlease try again.", true);
                }
                */
            }));
        })); 
    }
    
}
