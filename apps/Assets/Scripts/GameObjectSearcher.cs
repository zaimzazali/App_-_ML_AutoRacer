using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectSearcher : MonoBehaviour
{
    private List<GameObject> actors = new List<GameObject>();

    public List<GameObject> getChildObjectsWithTag(string _tag, GameObject parent) {
        actors.Clear();
        Transform _parent = parent.transform;
        GetChildObject(_parent, _tag);

        return actors;
    }

    private void GetChildObject(Transform parent, string _tag) {
        for (int i = 0; i < parent.childCount; i++) {
            Transform child = parent.GetChild(i);
            if (child.tag == _tag) {
                actors.Add(child.gameObject);
            }
            if (child.childCount > 0) {
                GetChildObject(child, _tag);
            }
        }
    }
}
