using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CameraCapture : MonoBehaviour
{
    public int fileCounter;
    public KeyCode screenshotKey;
    public OVRCameraRig ovrCameraRig;
    public GameObject centerEyeAnchor;
    private Camera Camera;

    private void Start()
    {
        // Camera = this.gameObject.GetComponent<Camera>();
        // Camera = this.ovrCameraRig.centerEyeAnchor.GetComponent<Camera>();
        // Debug.Log(this.Camera);
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(screenshotKey))
        {
            Capture();
        }
    }

    public string Capture()
    {
        // Debug.Log("Camera 1: ");
        // OVRCameraRig ovrCameraRigScript = this.ovrCameraRig.GetComponent<OVRCameraRig>();
        // this.Camera = ovrCameraRigScript._centerEyeCamera;
        // Debug.Log(this.Camera);
        Debug.Log("Camera 2: ");
        this.Camera = this.centerEyeAnchor.GetComponent<Camera>();
        Debug.Log(this.Camera);
        RenderTexture activeRenderTexture = RenderTexture.active;
        RenderTexture.active = Camera.targetTexture;

        Camera.Render();

        Texture2D image = new Texture2D(Camera.targetTexture.width, Camera.targetTexture.height);
        image.ReadPixels(new Rect(0, 0, Camera.targetTexture.width, Camera.targetTexture.height), 0, 0);
        image.Apply();
        RenderTexture.active = activeRenderTexture;

        byte[] bytes = image.EncodeToPNG();
        Destroy(image);

        string filePath = Application.dataPath + "/Captures/" + fileCounter + ".png";

        File.WriteAllBytes(filePath, bytes);
        fileCounter++;
        Debug.Log("Saved Capture at " + Application.dataPath + "/Captures/" + fileCounter + ".png"); //

        return filePath;
    }
}
