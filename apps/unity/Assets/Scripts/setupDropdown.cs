using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class setupDropdown : MonoBehaviour
{
    [SerializeField]
    private string list_name = null;

    private serverAPI serverAPI = null;

    private void Awake() {
        serverAPI = gameObject.GetComponent<serverAPI>();
        initGetData(list_name);
    }

    private void initGetData(string list_name) {
        JSONNode jsonData = null;
    
        switch (list_name)
        {
            case "gender":
                StartCoroutine(serverAPI.getListGender(result => {
                    try {
                        jsonData = JSON.Parse(result);
                        populateData(jsonData, "name_gender");
                    } catch (System.Exception) {
                        // Do Nothing
                    }
                })); 
                break;
            case "country":
                StartCoroutine(serverAPI.getListCountry(result => {
                    try {
                        jsonData = JSON.Parse(result);
                        populateData(jsonData, "name_country");
                    } catch (System.Exception) {
                        // Do Nothing
                    }
                })); 
                break;
            default:
                // Do Nothing
                break;
        }
    }

    private void populateData(JSONNode jsonData, string colName) {
        int count = jsonData.Count;
        int i = 0;
        Dropdown thisDropdown = gameObject.GetComponent<Dropdown>();

        for (i=0; i<count; i++) {
            thisDropdown.options.Add(new Dropdown.OptionData(jsonData[i][colName]));
        }
        
        setterChecker.doneSet();
    }
}
