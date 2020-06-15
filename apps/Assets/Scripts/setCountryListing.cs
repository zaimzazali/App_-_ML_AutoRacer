using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setCountryListing : MonoBehaviour
{
    [SerializeField]
    private TextAsset textFile = null;

    private string[] countryArray;

    private Dropdown thisDropDown;
    
    private void Start()
    {
        thisDropDown = gameObject.GetComponent<Dropdown>();
        // thisDropDown.options.Clear();

        countryArray = textFile.text.Split('\n');
        foreach (string str in countryArray) {
            thisDropDown.options.Add(new Dropdown.OptionData(str));
        }
    }
}
