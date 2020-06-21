using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setYearListing : MonoBehaviour
{
    private int bufferYear = 100;

    private int currentYear, startYear, endYear;
    private Dropdown thisDropDown;

    private void Awake() {
        int i;

        thisDropDown = gameObject.GetComponent<Dropdown>();

        currentYear = int.Parse(System.DateTime.Now.ToString("yyyy"));
        endYear = currentYear;
        startYear = currentYear - bufferYear;

        for (i=endYear; i>=startYear; i--) {
            thisDropDown.options.Add(new Dropdown.OptionData(i.ToString()));
        }
    }
}
