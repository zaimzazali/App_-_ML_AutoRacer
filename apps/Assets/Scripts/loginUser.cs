using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class loginUser : MonoBehaviour
{
    private GameObject parentObj = null;
    private TMP_InputField inputfield_username = null;
    private TMP_InputField inputfield_password = null;


    private initPopUp initPopUp = null;
    private initPopUp2 initPopUp2 = null;
    private serverAPI serverAPI = null;
    private waitForServer waitForServer = null;

    private void Awake() {
        parentObj = gameObject.transform.parent.parent.gameObject;
        inputfield_username = parentObj.transform.Find("Holder_Input_00/InputField_Login_Username").gameObject.GetComponent<TMP_InputField>();
        inputfield_password = parentObj.transform.Find("Holder_Input_01/InputField_Login_Password").gameObject.GetComponent<TMP_InputField>();
    
        initPopUp = gameObject.GetComponent<initPopUp>();
        initPopUp2 = gameObject.GetComponent<initPopUp2>();
        serverAPI = gameObject.GetComponent<serverAPI>();
        waitForServer = gameObject.GetComponent<waitForServer>();
    }

    public void initLogin() {
        string username, password;

        username = inputfield_username.text.Trim();
        password = inputfield_password.text.Trim();

        if (username == "" || password == "") {
            return;
        }

        initPopUp.nextDiv();
        waitForServer.showWaitingText();

        // Check credentials in Database
        StartCoroutine(serverAPI.initLogin(username, password, result => {
            StartCoroutine(waitForServer.hideWaitingText(callback => {
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
                    initPopUp2.displayPopUp_One_Button("There was an error occurred while checking for the username.\nPlease try again.", true);
                }
                */
            }));
        })); 
    }
    
}
