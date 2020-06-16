using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class serverAPI : MonoBehaviour
{
    string url_checkUsername = "http://localhost:1111/routes/checkUsername.php";

    public IEnumerator checkUsername(string theUsername, System.Action<string> result) {
        List<IMultipartFormSection> wwwForm = new List<IMultipartFormSection>();
        wwwForm.Add(new MultipartFormDataSection("isAllowed", "false"));
        wwwForm.Add(new MultipartFormDataSection("theUsername", theUsername));

        UnityWebRequest www = UnityWebRequest.Post(url_checkUsername, wwwForm);

        yield return www.SendWebRequest();
        
        if (www.isNetworkError || www.isHttpError) {
            Debug.LogError(www.error);
            result("error");
        }

        result(www.downloadHandler.text);
    }
}
