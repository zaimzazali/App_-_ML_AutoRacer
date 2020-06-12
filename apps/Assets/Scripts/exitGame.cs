using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitGame : MonoBehaviour
{
    public void quitGame() {
        Debug.Log("Game Application Closes");
        Application.Quit();
    }
}
