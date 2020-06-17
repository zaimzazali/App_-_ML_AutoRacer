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

    private void Awake() {
        thisDropDown = gameObject.GetComponent<Dropdown>();
    }

    public void setOptions(List<string[]> returnTable) {
        foreach (string[] row in returnTable) {
            thisDropDown.options.Add(new Dropdown.OptionData(row[interestedColumnNumber].ToString()));
        }
    }
}
