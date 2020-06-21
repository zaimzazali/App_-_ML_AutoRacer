using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class checkUsername : MonoBehaviour
{
    private GameObject usernameField = null;
    private Text statusText = null;
    private controlCanvas03 controlCanvas03 = null;
    private serverAPI serverAPI = null;
    private waitForServer waitForServer = null;
    private Any_Colours Any_Colours = new Any_Colours();

    private void Awake() {
        GameObject parentObj = gameObject.transform.parent.parent.parent.gameObject;
        usernameField = parentObj.transform.GetChild(0).gameObject.transform.Find("InputField_Register_Username").gameObject;
        statusText = parentObj.transform.GetChild(2).gameObject.transform.Find("Text").gameObject.GetComponent<Text>();

        controlCanvas03 = gameObject.GetComponent<controlCanvas03>();
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
                        statusText.color = Any_Colours.get_Color_Text_Green();
                    } else if (result == "not available") {
                        statusText.text = "Not Available";
                        statusText.color = Any_Colours.get_Color_Text_Red();
                    } else {
                        // Error
                        controlCanvas03.displayPopUp_One_Button("There was an error occurred while checking for the username.\nPlease try again.", true);
                    }
                }));
            })); 
        } else {
            usernameField.GetComponent<Any_Inputfield>().setError();
        }
    }
}
