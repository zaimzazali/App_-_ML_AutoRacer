using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initiateSceneFading : MonoBehaviour
{
    [SerializeField]
    private GameObject canvas_fader = null;

    private void Awake() {
        canvas_fader.SetActive(true);
        destroyAnyDontDestroy();
    }

    private void destroyAnyDontDestroy() {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("dont_destroy");
        for (int i=0; i<objs.Length; i++) {
            Destroy(objs[i]);
        }
    }
}
