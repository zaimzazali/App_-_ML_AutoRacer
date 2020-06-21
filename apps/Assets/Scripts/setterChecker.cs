using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setterChecker : MonoBehaviour
{
    public static int setterDone = 0;

    public static void doneSet() {
        setterDone += 1;
    }

    public static void clearSet() {
        setterDone = 0;
    }
}
