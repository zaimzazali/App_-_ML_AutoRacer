using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class initiateGame : MonoBehaviour
{
    private double videoLength = 0.0;
    private bool keyPressed = false;

    private void Awake() {
        videoLength = GameObject.Find("Video Player").gameObject.GetComponent<VideoPlayer>().length;
        StartCoroutine("waitForVideo");
        PlayerPrefs.SetInt("nextSceneIndex", 0);
    }

    private void Update() {
        StartCoroutine("toSkipCutscene");
    }

    private IEnumerator waitForVideo() {
        yield return new WaitForSeconds((float)videoLength);
        SceneManager.LoadScene(1);
    }

    private IEnumerator toSkipCutscene() {
        if (!keyPressed && Input.anyKey) {
            keyPressed = true;
            AsyncOperation gameLevel = SceneManager.LoadSceneAsync(1);
            while (gameLevel.progress < 1f) {
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
