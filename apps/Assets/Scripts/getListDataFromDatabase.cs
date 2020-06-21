using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getListDataFromDatabase : MonoBehaviour
{
    [SerializeField]
    private string tableName = null;

    [SerializeField]
    private bool isDropdown = true;

    private serverAPI serverAPI = null;
    private setDropdownOptions setDropdownOptions = null;

    private void Awake() {
        serverAPI = gameObject.GetComponent<serverAPI>();
        setDropdownOptions = gameObject.GetComponent<setDropdownOptions>();

        if (validTable(tableName)) {
            setDropDown();
        }
    }

    private bool validTable(string tableName) {
        bool result = false;

        switch (tableName)
        {
            case "list_country":
                result = true;
                break;
            case "list_gender":
                result = true;
                break;
            default:
                // Do Nothing
                break;
        }

        return result;
    }

    private void setDropDown() {
        StartCoroutine(serverAPI.selectAllData(tableName, result => {
            /*
            // Parse into Table
            List<string[]> returnTable = new List<string[]>();
            string[] rows = result.ToString().Split(';');
            foreach (string row in rows) {
                returnTable.Add(row.Split(','));
            }
            */
            //List<string[]> returnTable = JsonUtility.FromJson<List<string[]>>(result);

            //Debug.Log(returnTable);

            if (isDropdown) {
                //setDropdownOptions.setOptions(result, tableName);
            }
        })); 
    }
}
