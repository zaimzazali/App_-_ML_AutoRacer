using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goNextScene : MonoBehaviour
{
    [SerializeField]
    private sceneFader sceneFader = null;

    public IEnumerator changeScene() {
        sceneFader.gameObject.transform.parent.gameObject.SetActive(true);
        StartCoroutine(sceneFader.fadeIn());
        yield return new WaitForSeconds(sceneFader.getTotalWaitingTime());
        SceneManager.LoadScene(2);
    }
}
