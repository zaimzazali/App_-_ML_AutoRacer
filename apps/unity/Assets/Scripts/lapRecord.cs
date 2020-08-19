using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lapRecord : MonoBehaviour
{
    private bool hasEnter = false;
    private int lapCount = 0;
    private float lapTimer = 0f;
    private float currentLapTime = 0f;
    private List<float> recordedTime = new List<float>();
    private float aveLapTime = 0f;
    private float idleChecker = 0f;
    private float maxTime = 3600f; // Max 1 hour per lap
    private bool toProceed = true;

    [SerializeField]
    private sceneFader sceneFader = null;
    [SerializeField]
    private Text textLapCount = null;
    [SerializeField]
    private Text[] textTime = null;
    private int index = 0;

    private void Update() {
        if (Time.time - idleChecker >= maxTime && toProceed) {
            toProceed = false;
            finishRace();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Kart")) {
            hasEnter = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (hasEnter) {
            hasEnter = false;
            lapCount += 1;

            if (lapCount > 0) {
                textLapCount.text = string.Format("Lap {0} / 3", lapCount);
                startLap();
            }

            if (lapCount == 4) {
                endRace();
            }
        }
    }

    private void startLap() {
        idleChecker = Time.time;
        if (lapTimer > 0f) {
            currentLapTime = Time.time - lapTimer;
            recordedTime.Add(currentLapTime);
            textTime[index].text = string.Format("{0}", FormatTime(currentLapTime));
            index += 1;
            lapTimer = Time.time;
        } else {
            lapTimer = Time.time;
        }
    }

    string FormatTime (float time){
        int intTime = (int)time;
        int minutes = intTime / 60;
        int seconds = intTime % 60;
        float fraction = time * 1000;
        fraction = (fraction % 1000);
        string timeText = string.Format ("{0:00}:{1:00}:{2:000}", minutes, seconds, fraction);
        return timeText;
    }

    private void endRace() {
        float cumTime = 0f;
        for (int i=0; i<recordedTime.Count; i++) {
            cumTime += recordedTime[i];
        }
        aveLapTime = cumTime / 3f;
        finishRace();
    }

    private void finishRace() {
        sceneFader.gameObject.transform.parent.gameObject.SetActive(true);
        StartCoroutine(sceneFader.fadeIn());
    }
}
