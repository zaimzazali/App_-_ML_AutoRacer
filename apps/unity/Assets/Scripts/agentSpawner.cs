using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class agentSpawner : MonoBehaviour
{
    private string externalContent = "";
    private string filePath = @"D:\Git_Clone\App_-_ML_AutoRacer - Copy - Copy\apps\settings\training_variables.txt";
    // private string filePath = @"D:\Git_Clone\App_-_ML_AutoRacer - Copy - Copy\apps\settings\training_variables.txt";
    [SerializeField]
    private GameObject parent = null;
    [SerializeField]
    private GameObject agent = null;
    private int numInstancesToSpawn = 0;

    private void Awake() {
        if (File.Exists(filePath)){
            externalContent = File.ReadAllText(filePath);

            externalContent = externalContent.Replace(" ", "").Trim();
            string[] linesInFile = externalContent.Split('\n');

            try {
                foreach (string line in linesInFile) {
                    if (line.Contains("num_instances=")) {
                        numInstancesToSpawn = int.Parse(line.Replace("num_instances=", "").Trim());
                    }
                }
            } catch (FormatException e) {
                Debug.Log(e);
            }

            for (int i=0; i<numInstancesToSpawn; i++) {
                var newInstance = Instantiate(agent, new Vector3(0, 0, 0), Quaternion.identity);
                newInstance.transform.parent = parent.transform;
            }

        } else {
            Debug.Log("No file");
        }
    }
}
