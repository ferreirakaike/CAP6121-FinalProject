using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CameraCapture : MonoBehaviour
{
    public int fileCounter;
    public KeyCode screenshotKey;

    private OVRInput.Controller controller;

    private Camera Camera;

    private void Start()
    {
        Camera = this.gameObject.GetComponent<Camera>();
        controller = OVRInput.Controller.RTouch;
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(screenshotKey))
        {
            Capture();
        }

        if (OVRInput.GetDown(OVRInput.Button.One, controller))
        {
            Capture();
        }
    }

    public void Capture()
    {
        RenderTexture activeRenderTexture = RenderTexture.active;
        RenderTexture.active = Camera.targetTexture;

        Camera.Render();

        Texture2D image = new Texture2D(Camera.targetTexture.width, Camera.targetTexture.height);
        image.ReadPixels(new Rect(0, 0, Camera.targetTexture.width, Camera.targetTexture.height), 0, 0);
        image.Apply();
        RenderTexture.active = activeRenderTexture;

        byte[] bytes = image.EncodeToPNG();
        Destroy(image);

        File.WriteAllBytes(Application.dataPath + "/Captures/" + fileCounter + ".png", bytes);
        fileCounter++;
        Debug.Log("Saved Capture at " + Application.dataPath + "/Captures/" + fileCounter + ".png"); //
    }
}
