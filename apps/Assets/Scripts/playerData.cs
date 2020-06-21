using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class playerData : MonoBehaviour
{
    private bool account_active = false;
    private string account_type = null;
    private string account_password = null;
    private bool password_require_reset = false;
    private string user_name = null;
    private string user_gender = null;
    private string user_year_birth = null;
    private string user_country = null;
    private string user_email = null;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    public void setPlayerInfo(JSONNode jsonData) {
        account_active = bool.Parse((string)jsonData["account_active"]);
        account_type = (string)jsonData["account_type"];
        account_password = (string)jsonData["account_password"];
        password_require_reset = bool.Parse((string)jsonData["password_require_reset"]);
        user_name = (string)jsonData["user_name"];
        user_gender = (string)jsonData["user_gender"];
        user_year_birth = (string)jsonData["user_year_birth"];
        user_country = (string)jsonData["user_country"];
        user_email = (string)jsonData["user_email"];
    }
}
