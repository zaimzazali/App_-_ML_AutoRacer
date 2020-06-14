using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Any_Dropdown : MonoBehaviour
{
    private Dropdown thisDropDown;

    private void Start() {
        thisDropDown = gameObject.GetComponent<Dropdown>();
    }

    public string getSelectedValueText() {
        return thisDropDown.options[thisDropDown.value].text;
    }
}
