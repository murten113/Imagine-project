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


    void TakeScreenshot()
    {
        // Create a RenderTexture to capture the image
        RenderTexture rt = new RenderTexture(imageWidth, imageHeight, 24);
        RenderTexture originalRT = RenderTexture.active;
        RenderTexture originalCamRT = cam.targetTexture;
        float originalAspect = cam.aspect;

        cam.targetTexture = rt;
        cam.aspect = (float)imageWidth / imageHeight;
        cam.Render();

        // Capture the screenshot
        RenderTexture.active = rt;
        Texture2D screenshot = new Texture2D(imageWidth, imageHeight, TextureFormat.RGB24, false);
        screenshot.ReadPixels(new Rect(0, 0, imageWidth, imageHeight), 0, 0);
        screenshot.Apply();

        // Reset camera settings
        cam.targetTexture = originalCamRT;
        RenderTexture.active = originalRT;
        cam.aspect = originalAspect;

        // Add Polaroid frame
        Texture2D finalImage = ApplyPolaroidFrame(screenshot);

        // Save the final image
        string filePath = Path.Combine(screenshotFolder, $"polaroid_{screenshotCount}.png");
        File.WriteAllBytes(filePath, finalImage.EncodeToPNG());
        screenshotCount++;

        Debug.Log("Polaroid screenshot saved: " + filePath);
    }

    Texture2D ApplyPolaroidFrame(Texture2D screenshot)
    {
        // Load the Polaroid frame from Resources folder
        Texture2D frame = Resources.Load<Texture2D>("polaroid_frame");
        if (frame == null)
        {
            Debug.LogError("Polaroid frame not found in Resources folder!");
            return screenshot;
        }

        // Ensure the frame has the same size as the screenshot
        Texture2D resizedFrame = ResizeTexture(frame, imageWidth, imageHeight);

        // Blend the screenshot and frame
        Texture2D finalImage = new Texture2D(imageWidth, imageHeight);
        for (int y = 0; y < imageHeight; y++)
        {
            for (int x = 0; x < imageWidth; x++)
            {
                Color framePixel = resizedFrame.GetPixel(x, y);
                Color screenshotPixel = screenshot.GetPixel(x, y);

                // If the frame has transparency, keep the screenshot pixel
                finalImage.SetPixel(x, y, framePixel.a > 0 ? framePixel : screenshotPixel);
            }
        }

        finalImage.Apply();
        return finalImage;
    }

    Texture2D ResizeTexture(Texture2D source, int width, int height)
    {
        RenderTexture rt = RenderTexture.GetTemporary(width, height);
        Graphics.Blit(source, rt);
        RenderTexture.active = rt;
        Texture2D result = new Texture2D(width, height);
        result.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        result.Apply();
        RenderTexture.ReleaseTemporary(rt);
        return result;
    }
}