using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class checkUsername : MonoBehaviour
{
    private GameObject usernameField = null;
    private Text statusText = null;
    private initPopUp2 initPopUp2 = null;
    private serverAPI serverAPI = null;
    private waitForServer waitForServer = null;

    private void Awake() {
        GameObject parentObj = gameObject.transform.parent.parent.parent.gameObject;
        usernameField = parentObj.transform.GetChild(0).gameObject.transform.Find("InputField_Register_Username").gameObject;
        statusText = parentObj.transform.GetChild(2).gameObject.transform.Find("Text").gameObject.GetComponent<Text>();

        initPopUp2 = gameObject.GetComponent<initPopUp2>();
        serverAPI = gameObject.GetComponent<serverAPI>();
        waitForServer = gameObject.GetComponent<waitForServer>();
    }

    public void usernameCheck() {
        string theUsername;

        usernameField.GetComponent<Any_Inputfield>().setNormal();

        theUsername = usernameField.GetComponent<InputField>().text.Trim();

        if (theUsername.Length > 0) {
            waitForServer.showWaitingText();

            // Check username in Database
            StartCoroutine(serverAPI.checkUsername(theUsername, result => {
                StartCoroutine(waitForServer.hideWaitingText(callback => {
                    if (result == "available") {
                        statusText.text = "Available";
                        statusText.color = new Color(0f, 1f, 0f, 1f);
                    } else if (result == "not available") {
                        statusText.text = "Not Available";
                        statusText.color = new Color(1f, 0f, 0f, 1f);
                    } else {
                        // Error
                        initPopUp2.displayPopUp_One_Button("There was an error occurred while checking for the username.\nPlease try again.", true);
                    }
                }));
            })); 
        } else {
            usernameField.GetComponent<Any_Inputfield>().setError();
        }
    }
}
