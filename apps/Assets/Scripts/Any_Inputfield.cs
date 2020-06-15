using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Any_Inputfield : MonoBehaviour
{
    [SerializeField]
    private Color colorRed = new Color(255, 158, 158, 255), colorNormal = new Color(1f, 1f, 1f, 1f);

    public void setInputError() {
        gameObject.GetComponent<Image>().color = colorRed;
    }

    public void setInputNormal() {
        if (gameObject.GetComponent<Image>().color != colorNormal) {
            gameObject.GetComponent<Image>().color = colorNormal;
        }

        // If it is the Register - Username inputfield
        if (gameObject.name.ToString() == "InputField_Username") {
            GameObject.Find("Holder_Check_Result").gameObject.transform.Find("Text").gameObject.GetComponent<Text>().text = "";
        }
    }
}
