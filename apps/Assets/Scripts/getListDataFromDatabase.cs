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
        if (tableName != null || tableName.Trim() != "") {
            setDropDown();
        }
    }

    private void setDropDown() {
        StartCoroutine(serverAPI.selectAllData(tableName, result => {
            // Parse into Table
            List<string[]> returnTable = new List<string[]>();
            string[] rows = result.ToString().Split(';');
            foreach (string row in rows) {
                returnTable.Add(row.Split(','));
            }

            if (isDropdown) {
                setDropdownOptions.setOptions(returnTable);
            }
        })); 
    }
}
