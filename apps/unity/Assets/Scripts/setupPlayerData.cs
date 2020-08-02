using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setupPlayerData : MonoBehaviour
{
    [SerializeField]
    private Text text_username = null;

    private playerData playerData = null;
    
    private void Awake() {
        try {
            playerData = GameObject.Find("Player_Data").GetComponent<playerData>();
            if (playerData != null) {
                setUsername();
            }
        } catch (System.Exception) {
            text_username.text = "Offline Player";
        }
    }

    private void setUsername() {
        if (playerData.getPlayer_Username() != null) {
            text_username.text = playerData.getPlayer_Username();
        } else {
            text_username.text = "Offline Player";
        }
    }
}
