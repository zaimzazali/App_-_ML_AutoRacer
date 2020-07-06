using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Any_Dropdown : MonoBehaviour
{
    private Any_Colours Any_Colours = new Any_Colours();
    
    public void setError() {
        gameObject.GetComponent<Image>().color = Any_Colours.get_Colour_Red();
    }

    public void setNormal() {
        if (gameObject.GetComponent<Image>().color != Any_Colours.get_Colour_Normal()) {
            gameObject.GetComponent<Image>().color = Any_Colours.get_Colour_Normal();
        }
    }

    public string getSelectedValueText() {
        return gameObject.GetComponent<Dropdown>().options[gameObject.GetComponent<Dropdown>().value].text;
    }
}
