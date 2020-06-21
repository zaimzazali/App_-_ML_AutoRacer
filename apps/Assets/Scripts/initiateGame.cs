using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class initiateGame : MonoBehaviour
{
    private double videoLength = 0.0;

    private void Awake() {
        videoLength = GameObject.Find("Video Player").gameObject.GetComponent<VideoPlayer>().length;
        StartCoroutine("waitForVideo");
        PlayerPrefs.SetInt("nextSceneIndex", 0);
    }

    private IEnumerator waitForVideo() {
        yield return new WaitForSeconds((float)videoLength);
        SceneManager.LoadScene(1);
    }
}
