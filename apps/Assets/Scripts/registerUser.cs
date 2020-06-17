using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class registerUser : MonoBehaviour
{
    [SerializeField]
    private inputValidator inputValidator = null;

    [SerializeField]
    private GameObject inputObj_Name = null, inputObj_Gender = null, inputObj_Year = null, inputObj_Country = null,
    inputObj_Username = null, inputObj_Email = null, inputObj_Pass_00 = null, inputObj_Pass_01 = null, inputObj_Status = null;

    private ArrayList resultArray = null;

    [SerializeField]
    private initPopUp2 initPopUp2 = null;

    [SerializeField]
    private serverAPI serverAPI = null;

    public void initRegistration() {
        resultArray = new ArrayList();

        bool passedInputCheck = false;

        passedInputCheck = checkInput();

        if (passedInputCheck) {
            Debug.Log("Passed");

            // Push to Database
            StartCoroutine(serverAPI.registerUser(resultArray, result => {
                if (result == "OK") {
                    //statusText.text = "Available";
                    //statusText.color = new Color(0f, 1f, 0f, 1f);
                }
                else if (result == "not available") {
                    //statusText.text = "Not Available";
                    //statusText.color = new Color(1f, 0f, 0f, 1f);
                }
                else {
                    // Error
                    initPopUp2.displayPopUp_One_Button("There was an error occurred during the Registration.\nPlease try again.", true);
                }
            })); 
        }
    }

    private bool checkInput() {
        string theInput = null;
        string tmpString = null;

        // Check for Name
        theInput = inputObj_Name.transform.Find("Text").gameObject.GetComponent<Text>().text.Trim();
        if (inputValidator.isNameValid(theInput)) {
            resultArray.Add(theInput);
        } else {
            inputObj_Name.GetComponent<Any_Inputfield>().setError();
        }

        // Check for Gender
        theInput = inputObj_Gender.GetComponent<Any_Dropdown>().getSelectedValueText();
        if (theInput != "") {
            resultArray.Add(inputObj_Gender.GetComponent<Dropdown>().value.ToString());
        } else {
            inputObj_Gender.GetComponent<Any_Dropdown>().setError();
        }
        // Check for Year
        theInput = inputObj_Year.GetComponent<Any_Dropdown>().getSelectedValueText();
        if (theInput != "") {
            resultArray.Add(theInput);
        } else {
            inputObj_Year.GetComponent<Any_Dropdown>().setError();
        }
        // Check for Country
        theInput = inputObj_Country.GetComponent<Any_Dropdown>().getSelectedValueText();
        if (theInput != "") {
            resultArray.Add(inputObj_Country.GetComponent<Dropdown>().value.ToString());
        } else {
            inputObj_Country.GetComponent<Any_Dropdown>().setError();
        }

        // Check for Username
        theInput = inputObj_Status.transform.Find("Text").gameObject.GetComponent<Text>().text.Trim();
        if (theInput == "Available") {
            resultArray.Add(inputObj_Username.transform.Find("Text").gameObject.GetComponent<Text>().text.Trim());
        }
        else {
            inputObj_Username.GetComponent<Any_Inputfield>().setError();
        }

        // Check for Email
        theInput = inputObj_Email.transform.Find("Text").gameObject.GetComponent<Text>().text.Trim();
        if (inputValidator.isEmailValid(theInput)) {
            resultArray.Add(theInput);
        } else {
            inputObj_Email.GetComponent<Any_Inputfield>().setError();
        }

        // Check for Password
        theInput = inputObj_Pass_00.transform.Find("Text").gameObject.GetComponent<Text>().text.Trim();
        if (inputValidator.isPasswordValid(theInput)) {
            resultArray.Add(theInput);
        } else {
            inputObj_Pass_00.GetComponent<Any_Inputfield>().setError();
        }

        tmpString = theInput;

        // Check if Password Same
        theInput = inputObj_Pass_01.transform.Find("Text").gameObject.GetComponent<Text>().text.Trim();
        if (theInput == "") {
            inputObj_Pass_01.GetComponent<Any_Inputfield>().setError();
        } else if (inputValidator.isPassSame(theInput, tmpString)) {
            resultArray.Add(theInput);
        } else {
            inputObj_Pass_01.GetComponent<Any_Inputfield>().setError();
        }

        if (resultArray.Count != 8) {
            return false;
        }

        return true;
    }
}
