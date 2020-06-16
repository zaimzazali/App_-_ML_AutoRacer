using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class checkCredentials : MonoBehaviour
{
    [SerializeField]
    private GameObject usernameField = null;

    [SerializeField]
    private Text statusText = null;

    [SerializeField]
    private initPopUp2 initPopUp2 = null;

    [SerializeField]
    private serverAPI serverAPI = null;

    public void checkUsername() {
        string theUsername;

        usernameField.GetComponent<Any_Inputfield>().setInputNormal();

        theUsername = usernameField.transform.Find("Text").gameObject.GetComponent<Text>().text.Trim();

        if (theUsername.Length > 0) {
            // Check username in Database
            StartCoroutine(serverAPI.checkUsername(theUsername, result => {
                if (result == "available") {
                    statusText.text = "Available";
                    statusText.color = new Color(0f, 1f, 0f, 1f);
                }
                else if (result == "not available") {
                    statusText.text = "Not Available";
                    statusText.color = new Color(1f, 0f, 0f, 1f);
                }
                else {
                    // Error
                    initPopUp2.displayPopUp_One_Button("There was an error occurred while checking for the Username.\nPlease try again.", true);
                }
            })); 
        } else {
            usernameField.GetComponent<Any_Inputfield>().setInputError();
        }
    }
}
