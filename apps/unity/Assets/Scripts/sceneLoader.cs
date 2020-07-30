using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class sceneLoader : MonoBehaviour
{
    [SerializeField]
    private Image fillImg = null;

    private void Awake() {
        StartCoroutine("LoadScene");
    }

    private IEnumerator LoadScene() {
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(PlayerPrefs.GetInt("nextSceneIndex"));
        while (gameLevel.progress < 1f) {
            fillImg.fillAmount = gameLevel.progress;
            yield return new WaitForEndOfFrame();
        }
    }
}
