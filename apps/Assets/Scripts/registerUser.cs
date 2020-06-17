using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class registerUser : MonoBehaviour
{
    private inputValidator inputValidator = null;
    private initPopUp2 initPopUp2 = null;
    private serverAPI serverAPI = null;

    private GameObject inputObj_Name = null, inputObj_Gender = null, inputObj_Year = null, inputObj_Country = null,
    inputObj_Username = null, inputObj_Email = null, inputObj_Pass_00 = null, inputObj_Pass_01 = null, inputObj_Status = null;

    private ArrayList resultArray = null;

    private void Awake() {
        inputValidator = gameObject.GetComponent<inputValidator>();
        initPopUp2 = gameObject.GetComponent<initPopUp2>();
        serverAPI = gameObject.GetComponent<serverAPI>();

        GameObject parentObj = gameObject.transform.parent.parent.parent.gameObject;

        inputObj_Name = parentObj.transform.Find("Holder_00/Holder_Name/Holder_Content/Holder_InputField/InputField_Register_Name").gameObject;
        inputObj_Gender = parentObj.transform.Find("Holder_00/Holder_Details/Holder_Gender/Holder_Content/Dropdown_Gender").gameObject;
        inputObj_Year = parentObj.transform.Find("Holder_00/Holder_Details/Holder_Year/Holder_Content/Dropdown_Year").gameObject;
        inputObj_Country = parentObj.transform.Find("Holder_00/Holder_Details/Holder_Country/Holder_Content/Dropdown_Country").gameObject;

        inputObj_Username = parentObj.transform.Find("Holder_01/Holder_Username/Holder_Content/Holder_InputField/InputField_Register_Username").gameObject;
        inputObj_Status = parentObj.transform.Find("Holder_01/Holder_Username/Holder_Content/Holder_Check_Result").gameObject;

        inputObj_Email = parentObj.transform.Find("Holder_01/Holder_Email/Holder_Content/Holder_Text/InputField_Register_Email").gameObject;
        inputObj_Pass_00 = parentObj.transform.Find("Holder_01/Holder_Password/Holder_Pass_00/Holder_Content/InputField_Register_Pass_00").gameObject;
        inputObj_Pass_01 = parentObj.transform.Find("Holder_01/Holder_Password/Holder_Pass_01/Holder_Content/InputField_Register_Pass_01").gameObject;
    }

    public void initRegistration() {
        resultArray = new ArrayList();

        bool passedInputCheck = false;

        passedInputCheck = checkInput();

        if (passedInputCheck) {
            // Push to Database
            StartCoroutine(serverAPI.registerUser(resultArray, result => {
                if (result == "OK") {
                    initPopUp2.displayPopUp_One_Button("Thank you for registering!\nYou may now login!", false);
                } else {
                    // Error
                    initPopUp2.displayPopUp_One_Button("There was an error occurred during the registration.\nPlease try again.", true);
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
        } else {
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
