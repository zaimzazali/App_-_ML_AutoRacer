using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Any_Dropdown : MonoBehaviour
{
    [SerializeField]
    private Color colorRed = new Color(255, 158, 158, 255), colorNormal = new Color(1f, 1f, 1f, 1f);

    public string getSelectedValueText() {
        return gameObject.GetComponent<Dropdown>().options[gameObject.GetComponent<Dropdown>().value].text;
    }

    public void setDropError() {
        gameObject.GetComponent<Image>().color = colorRed;
    }

    public void setDropNormal() {
        if (gameObject.GetComponent<Image>().color != colorNormal) {
            gameObject.GetComponent<Image>().color = colorNormal;
        }
    }
}
