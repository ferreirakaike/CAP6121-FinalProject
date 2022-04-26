using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Windows;

public class ServerRequest : MonoBehaviour
{
    private string url = "http://localhost:8080/";
    public RenderTexture texture;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Q key was pressed.");
            MakeRequest("TEST", "Assets/Captures/0.png");
        }
    }

    public void MakeRequest(string query, string filePath)
    {
        StartCoroutine(postRequest(query, filePath));
    }

    private IEnumerator postRequest(string query, string filePath)
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();

        Texture2D texture2D = toTexture2D(texture);
        byte[] bytes = texture2D.EncodeToPNG();
        formData.Add(new MultipartFormFileSection("capture.png", bytes));

        query = GroomString(query);

        formData.Add(new MultipartFormFileSection(query, "query.txt"));

        UnityWebRequest uwr = UnityWebRequest.Post(this.url, formData);
        yield return uwr.SendWebRequest();

        if (uwr.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
        }
    }

    Texture2D toTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(480, 320, TextureFormat.RGB24, false);
        // ReadPixels looks at the active RenderTexture.
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();

        SaveTextureAsPNG(tex, "Assets/Captures/testCapture.png");

        return tex;
    }

    // FOR DEBUGGING USE ONLY
    public static void SaveTextureAsPNG(Texture2D _texture, string _fullPath)
    {
        byte[] _bytes = _texture.EncodeToPNG();
        System.IO.File.WriteAllBytes(_fullPath, _bytes);
        Debug.Log(_bytes.Length / 1024 + "Kb was saved as: " + _fullPath);
    }

    private string GroomString(string text)
    {
        text = text.ToLower();

        StringBuilder sb = new StringBuilder();
        foreach (char c in text)
        {
            if ((c >= 'a' && c <= 'z') || c == ' ')
            {
                sb.Append(c);
            }
        }
        return sb.ToString();

    }
}