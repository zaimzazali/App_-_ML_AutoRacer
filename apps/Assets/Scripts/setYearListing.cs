using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setYearListing : MonoBehaviour
{
    [SerializeField]
    private int bufferYear = 0;

    private int currentYear, startYear, endYear;
    private Dropdown thisDropDown;
    private int i;

    private void Start()
    {
        thisDropDown = gameObject.GetComponent<Dropdown>();
        thisDropDown.options.Clear();

        currentYear = int.Parse(System.DateTime.Now.ToString("yyyy"));
        endYear = currentYear;
        startYear = currentYear - bufferYear;

        for (i=endYear; i>=startYear; i--) {
            thisDropDown.options.Add(new Dropdown.OptionData(i.ToString()));
        }
    }
}
