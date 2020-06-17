using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class serverAPI : MonoBehaviour
{
    string url_checkUsername = "http://localhost:1111/routes/checkUsername.php";
    string url_registerUser = "http://localhost:1111/routes/registerUser.php";

    string url_selectAllData = "http://localhost:1111/routes/selectAllData.php";

    public IEnumerator checkUsername(string theUsername, System.Action<string> result) {
        List<IMultipartFormSection> wwwForm = new List<IMultipartFormSection>();
        wwwForm.Add(new MultipartFormDataSection("isAllowed", "true"));
        wwwForm.Add(new MultipartFormDataSection("theUsername", theUsername));

        UnityWebRequest www = UnityWebRequest.Post(url_checkUsername, wwwForm);

        yield return www.SendWebRequest();
        
        if (www.isNetworkError || www.isHttpError) {
            Debug.LogError(www.error);
            result("error");
        }

        result(www.downloadHandler.text);
    }

    public IEnumerator registerUser(ArrayList myParameters, System.Action<string> result) {
        List<IMultipartFormSection> wwwForm = new List<IMultipartFormSection>();
        wwwForm.Add(new MultipartFormDataSection("isAllowed", "true"));
        wwwForm.Add(new MultipartFormDataSection("theName", (string)myParameters[0]));
        wwwForm.Add(new MultipartFormDataSection("theGender", (string)myParameters[1]));
        wwwForm.Add(new MultipartFormDataSection("theYear", (string)myParameters[2]));
        wwwForm.Add(new MultipartFormDataSection("theCountry", (string)myParameters[3]));
        wwwForm.Add(new MultipartFormDataSection("theUsername", (string)myParameters[4]));
        wwwForm.Add(new MultipartFormDataSection("theEmail", (string)myParameters[5]));
        wwwForm.Add(new MultipartFormDataSection("thePassword", (string)myParameters[6]));

        UnityWebRequest www = UnityWebRequest.Post(url_registerUser, wwwForm);

        yield return www.SendWebRequest();
        
        if (www.isNetworkError || www.isHttpError) {
            Debug.LogError(www.error);
            result("error");
        }

        result(www.downloadHandler.text);
    }

    public IEnumerator selectAllData(string tableName, System.Action<string> result) {
        List<IMultipartFormSection> wwwForm = new List<IMultipartFormSection>();
        wwwForm.Add(new MultipartFormDataSection("isAllowed", "true"));
        wwwForm.Add(new MultipartFormDataSection("tableName", tableName));

        UnityWebRequest www = UnityWebRequest.Post(url_selectAllData, wwwForm);

        yield return www.SendWebRequest();
        
        if (www.isNetworkError || www.isHttpError) {
            Debug.LogError(www.error);
            result("error");
        }

        result(www.downloadHandler.text);
    }

}
