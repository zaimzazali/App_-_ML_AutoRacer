using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameMaster : MonoBehaviour
{
    [SerializeField]
    private GameObject canvas_fader = null;

    private void Awake() {
        canvas_fader.SetActive(true);
    }
}
