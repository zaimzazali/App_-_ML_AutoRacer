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

    public void initRegistration() {
        bool passedInputCheck = false;

        passedInputCheck = checkInput();

        if (passedInputCheck) {
            // Push to Database
            
        }
    }

    private bool checkInput() {
        string theInput = null;
        ArrayList resultArray = new ArrayList();
        string tmpString = null;

        // Check for Name
        theInput = inputObj_Name.transform.Find("Text").gameObject.GetComponent<Text>().text.Trim();
        if (inputValidator.isNameValid(theInput)) {
            resultArray.Add(theInput);
        } else {
            inputObj_Name.GetComponent<Any_Inputfield>().setInputError();
        }

        // Check for Gender
        theInput = inputObj_Gender.GetComponent<Any_Dropdown>().getSelectedValueText();
        if (theInput != "") {
            resultArray.Add(theInput);
        } else {
            inputObj_Gender.GetComponent<Any_Dropdown>().setDropError();
        }
        // Check for Year
        theInput = inputObj_Year.GetComponent<Any_Dropdown>().getSelectedValueText();
        if (theInput != "") {
            resultArray.Add(theInput);
        } else {
            inputObj_Year.GetComponent<Any_Dropdown>().setDropError();
        }
        // Check for Country
        theInput = inputObj_Country.GetComponent<Any_Dropdown>().getSelectedValueText();
        if (theInput != "") {
            resultArray.Add(theInput);
        } else {
            inputObj_Country.GetComponent<Any_Dropdown>().setDropError();
        }

        // Check for Username
        theInput = inputObj_Status.transform.Find("Text").gameObject.GetComponent<Text>().text.Trim();
        if (theInput == "Available") {
            resultArray.Add(theInput);
        }
        else {
            inputObj_Username.GetComponent<Any_Inputfield>().setInputError();
        }

        // Check for Email
        theInput = inputObj_Email.transform.Find("Text").gameObject.GetComponent<Text>().text.Trim();
        if (inputValidator.isEmailValid(theInput)) {
            resultArray.Add(theInput);
        } else {
            inputObj_Email.GetComponent<Any_Inputfield>().setInputError();
        }

        // Check for Password
        theInput = inputObj_Pass_00.transform.Find("Text").gameObject.GetComponent<Text>().text.Trim();
        if (inputValidator.isPasswordValid(theInput)) {
            resultArray.Add(theInput);
        } else {
            inputObj_Pass_00.GetComponent<Any_Inputfield>().setInputError();
        }

        tmpString = theInput;

        // Check if Password Same
        theInput = inputObj_Pass_01.transform.Find("Text").gameObject.GetComponent<Text>().text.Trim();
        if (theInput == "") {
            inputObj_Pass_01.GetComponent<Any_Inputfield>().setInputError();
        } else if (inputValidator.isPassSame(theInput, tmpString)) {
            resultArray.Add(theInput);
        } else {
            inputObj_Pass_01.GetComponent<Any_Inputfield>().setInputError();
        }

        return true;
    }
}
