using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setDropdownOptions : MonoBehaviour
{
    [SerializeField]
    private int interestedColumnNumber = 0;

    private Dropdown thisDropDown;
    private List<string[]> returnTable = new List<string[]>();

    private class list_gender {
        private int num;
        private string name_gender;
    }

    private class list_country {
        private int num;
        private string name_country;
    }

    private void Awake() {
        thisDropDown = gameObject.GetComponent<Dropdown>();
    }

    public void setOptions(string jsonString, string tableName) {

        Debug.Log((string)jsonString);

        switch (tableName)
        {
            case "list_country":
                list_country data1 = JsonUtility.FromJson<list_country>((string)jsonString);
                break;
            case "list_gender":
                list_gender data2 = JsonUtility.FromJson<list_gender>((string)jsonString);
                Debug.Log(data2);
                break;
            default:
                // Do Nothing
                break;
        }
/*
        foreach (string[] row in returnTable) {
            thisDropDown.options.Add(new Dropdown.OptionData(row[interestedColumnNumber].ToString()));
        }
        
    */
    }
}
