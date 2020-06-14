using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class registerUser : MonoBehaviour
{
    void Start()
    {
        
    }

    public void initRegistration() {
        bool passedInputCheck = false;

        passedInputCheck = checkInput();

        if (passedInputCheck) {
            // Push to Database
        } else {
            // Tell user to re-check the inputs
        }
    }

    private bool checkInput() {

        // Check for Name

        // Check for Username

        // Check for Email

        // Check for Password

        // Check if Password Same


        return true;
    }
}
