using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controlCanvas01 : MonoBehaviour
{
    [SerializeField]
    private GameObject canvas = null, objToMove = null;

    private enum type {
        horizontal, vertical
    }

    [SerializeField]
    private type animType = type.horizontal;

    [SerializeField]
    private float from_position = 0, to_position = 0, timing = 0.5f;

    [SerializeField]
    private bool toSetInitialPosition = false, toClearInputs = false, toHideAfter = false;

    private objectMover objectMover = null;

    private bool isActive;

    private void Awake() {
        objectMover = gameObject.GetComponent<objectMover>();
        if (gameObject.GetComponent<Button>() != null) { 
            isActive = gameObject.GetComponent<Button>().interactable;
        } else {
            isActive = gameObject.GetComponentInParent<Button>().interactable;
        }
    }

    public void initNext() {
        GameObject[] gameObjects = {canvas, objToMove};
        float[] floats = {from_position, to_position, timing};
        string animTypeStr = animType.ToString();
        bool[] bools = {toSetInitialPosition, toClearInputs, toHideAfter};

        if (isActive) {
            objectMover.goNextState(gameObjects, floats, animTypeStr, bools);
        }
    }
}
