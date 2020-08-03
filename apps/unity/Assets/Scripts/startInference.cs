using System.IO;
using Unity.Barracuda;
using Unity.MLAgents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.MLAgents.Policies;

public class startInference : MonoBehaviour
{
    [SerializeField]
    private InputField nnName = null;
    [SerializeField]
    private Agent theAgent = null;

    private void Awake() {
        PlayerPrefs.SetString("modelName", "none");
    }

    public void setNNname() {
        PlayerPrefs.SetString("modelName", nnName.text.Trim());
    }

    public void startInferencing() {
        byte[] model = File.ReadAllBytes("D:/Git_Clone/App_-_ML_AutoRacer/apps/results/test00/agentF1.nn");

        var asset = ScriptableObject.CreateInstance<NNModel>();
        asset.modelData = ScriptableObject.CreateInstance<NNModelData>();
        asset.modelData.Value = model;

        theAgent.SetModel("newSetModel", asset);
    }
}
