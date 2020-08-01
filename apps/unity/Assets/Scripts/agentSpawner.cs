using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class agentSpawner : MonoBehaviour
{
    private string externalContent = "";
    private string filePath = @"D:\Git_Clone\App_-_ML_AutoRacer\apps\settings\training_variables.txt";
    // private string filePath = @"D:\Git_Clone\App_-_ML_AutoRacer\apps\settings\training_variables.txt";
    [SerializeField]
    private GameObject parent = null;
    [SerializeField]
    private GameObject agent = null;
    private int numInstancesToSpawn = 0;
    private float newTopSpeed = 0f;
    private GameObject[] agents;
    [SerializeField]
    private GameObject statusBackground = null;
    [SerializeField]
    private Text statusText = null;

    private void Awake() {
        if (File.Exists(filePath)) {
            externalContent = File.ReadAllText(filePath);

            externalContent = externalContent.Replace(" ", "").Trim();
            string[] linesInFile = externalContent.Split('\n');

            try {
                foreach (string line in linesInFile) {
                    if (line.Contains("num_instances=")) {
                        numInstancesToSpawn = int.Parse(line.Replace("num_instances=", "").Trim());
                    } else if (line.Contains("top_speed=")) {
                        newTopSpeed = float.Parse(line.Replace("top_speed=", "").Trim());
                    }
                }
                statusBackground.SetActive(false);
            } catch (FormatException e) {
                Debug.Log(e);
            }

            for (int i=0; i<numInstancesToSpawn; i++) {
                var newInstance = Instantiate(agent, new Vector3(0, 0, 0), Quaternion.identity);
                newInstance.transform.parent = parent.transform;
            }

            agents = GameObject.FindGameObjectsWithTag("agentKart");
            for (int i=0; i<agents.Length; i++) {
                agents[i].GetComponent<agentKart>().setTopSpeed(newTopSpeed);
            }
        } else {
            statusBackground.GetComponent<Image>().color = Color.red;
            statusText.text = "Cannot find training_variables.txt";
        }
    }
}
