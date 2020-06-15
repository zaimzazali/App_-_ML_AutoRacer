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

    public void checkUsername() {
        string theUsername;
        bool isNew = false;

        theUsername = usernameField.transform.Find("Text").gameObject.GetComponent<Text>().text.Trim();

        if (theUsername.Length > 0) {
            // Check username in Database

            if (isNew) {
                statusText.text = "Available";
                statusText.color = new Color(0f, 1f, 0f, 1f);
            }
            else {
                statusText.text = "Not Available";
                statusText.color = new Color(1f, 0f, 0f, 1f);
            }
        } else {
            usernameField.GetComponent<Any_Inputfield>().setInputError();
        }
    }
}
