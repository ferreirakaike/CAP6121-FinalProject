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
    private Vector3 pos = new Vector3(200, 200, 0);

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

    public void DeleteObject()
    {
        RaycastHit hit;
        Ray ray = Camera.ScreenPointToRay(pos);
        //Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
        if (Physics.Raycast(ray, out hit))
        {
            GameObject gameObject = hit.transform.gameObject;

            // Do something with the object that was hit by the raycast
            Debug.Log("We have hit object: " + gameObject.name);
            if (gameObject.name == "RedCube")
            {
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        //DeleteObject();
    }
}
