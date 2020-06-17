using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputValidator : MonoBehaviour
{
    public bool isNameValid(string input) {
        if (input.Length >= 2) {
            return true;
        }

        return false;
    }
    
    public bool isEmailValid(string input) {
        string remainString = null;

        if (input.Length < 5) {
            return false;
        }

        if (input.IndexOf("@") == -1 || input.IndexOf("@") == 0 || 
            input.IndexOf("@") == input.Length-1 || input.LastIndexOf("@") == input.Length-1) {
            return false;
        }

        remainString = input.Substring(input.IndexOf("@")+1);

        if (remainString.IndexOf(".") == -1 || remainString.IndexOf(".") == 0 || 
            remainString.IndexOf(".") == remainString.Length-1 || remainString.LastIndexOf(".") == remainString.Length-1) {
            return false;
        }

        return true;
    }

    public bool isPasswordValid(string input) {
        if (input.Length < 8) {
            return false;
        }

        return true;
    }

    public bool isPassSame(string input1, string input2) {
        if (input1 != input2) {
            return false;
        }
        
        return true;
    }
}
