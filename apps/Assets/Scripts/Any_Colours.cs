using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Any_Colours 
{
    private Color input_Red = new Color32(255, 158, 158, 255);
    private Color input_Normal = new Color(1f, 1f, 1f, 1f);

    private Color text_normal = new Color32(214, 214, 214, 0);
    private Color text_green = new Color(0f, 1f, 0f, 1f);
    private Color text_red = new Color(1f, 0f, 0f, 1f);

    private Color panel_red = new Color32(191, 47, 56, 30);
    private Color panel_clear = new Color(0f, 0f, 0f, 0f);


    public Color get_Colour_Red() {
        return input_Red;
    }

    public Color get_Colour_Normal() {
        return input_Normal;
    }

    public Color get_Color_Text_Normal() {
        return text_normal;
    }

    public Color get_Color_Text_Green() {
        return text_green;
    }

    public Color get_Color_Text_Red() {
        return text_red;
    }

    public Color get_Color_Panel_Red() {
        return panel_red;
    }

    public Color get_Color_Panel_Clear() {
        return panel_clear;
    }
}
