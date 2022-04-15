using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerRequest : MonoBehaviour
{

    private string url = "http://localhost:81";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeRequest(string query, string filePath)
    {
        StartCoroutine(postRequest(query, filePath));
    }

    private IEnumerator postRequest(string query, string filePath)
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("query=" + query));
        formData.Add(new MultipartFormFileSection("screenshot", "myfile.txt"));

        UnityWebRequest uwr = UnityWebRequest.Post(this.url, formData);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
        }
    }
}