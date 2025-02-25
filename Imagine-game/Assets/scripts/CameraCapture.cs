using UnityEngine;
using System.IO;

public class CameraCapture : MonoBehaviour
{
    public int imageWidth = 1920;  // Desired width
    public int imageHeight = 1080; // Desired height
    public string screenshotFolder = "Screenshots";
    private int screenshotCount = 0;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();

        // Create the screenshot folder if it doesn't exist
        if (!Directory.Exists(screenshotFolder))
        {
            Directory.CreateDirectory(screenshotFolder);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Input.GetMouseButton(1)) // Left-click to take a picture
        {
            TakeScreenshot();
        }
    }

    void CheckMouseButton()
    {



    }



    void TakeScreenshot()
    {
        // Create a temporary RenderTexture
        RenderTexture rt = new RenderTexture(imageWidth, imageHeight, 24);

        // Store the original settings
        RenderTexture originalRT = RenderTexture.active;
        RenderTexture originalCamRT = cam.targetTexture;
        float originalAspect = cam.aspect;

        // Set the camera to match the desired aspect ratio
        cam.targetTexture = rt;
        cam.aspect = (float)imageWidth / imageHeight;
        cam.Render();

        // Read pixels from the camera
        RenderTexture.active = rt;
        Texture2D screenShot = new Texture2D(imageWidth, imageHeight, TextureFormat.RGB24, false);
        screenShot.ReadPixels(new Rect(0, 0, imageWidth, imageHeight), 0, 0);
        screenShot.Apply();

        // Reset the camera back to original settings
        cam.targetTexture = originalCamRT;
        RenderTexture.active = originalRT;
        cam.aspect = originalAspect;

        // Save the image
        string filePath = Path.Combine(screenshotFolder, $"screenshot_{screenshotCount}.png");
        File.WriteAllBytes(filePath, screenShot.EncodeToPNG());
        screenshotCount++;

        Debug.Log("Screenshot saved: " + filePath);
    }
}