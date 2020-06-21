﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loadingtext : MonoBehaviour {

    private RectTransform rectComponent;
    private Image imageComp;

    public float speed = 200f;

    private void Start () {
        rectComponent = GetComponent<RectTransform>();
        imageComp = rectComponent.GetComponent<Image>();
        imageComp.fillAmount = 0.0f;
    }
    
	private void Update () {
        if (imageComp.fillAmount != 1f) {
            imageComp.fillAmount = imageComp.fillAmount + Time.deltaTime * speed;
        }
    }
}
