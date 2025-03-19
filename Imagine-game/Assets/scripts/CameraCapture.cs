using UnityEngine;
using System.IO;

public class CameraCapture : MonoBehaviour
{
    public Camera camera;
    public int imageWidth = 1080;
    public int imageHeight = 1350; // Aspect ratio similar to a polaroid
    public string screenshotFolder = "Screenshots";
    private int screenshotCount = 0;

    void Start()
    {
        string folderPath = Path.Combine(Application.dataPath, "Resources", screenshotFolder);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
            Debug.Log("Created folder: " + folderPath);
        }
    }

    void Update()
    {
        // Check if right click is held down and left click is pressed
        if (Input.GetMouseButton(1) && Input.GetMouseButtonDown(0))
        {
            TakePolaroidScreenshot();
        }
    }

    void TakePolaroidScreenshot()
    {
        // Create a RenderTexture for capturing the screenshot
        RenderTexture renderTex = new RenderTexture(imageWidth, imageHeight, 24);
        camera.targetTexture = renderTex;

        // Render the camera view
        Texture2D screenshot = new Texture2D(imageWidth, imageHeight, TextureFormat.RGB24, false);
        camera.Render();
        RenderTexture.active = renderTex;
        screenshot.ReadPixels(new Rect(0, 0, imageWidth, imageHeight), 0, 0);
        camera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(renderTex);

        // Add a polaroid frame
        Texture2D polaroid = AddPolaroidFrame(screenshot);

        // Save the screenshot
        string filePath = Path.Combine(Application.dataPath, "Resources", screenshotFolder, $"screenshot_{screenshotCount}.png");
        File.WriteAllBytes(filePath, polaroid.EncodeToPNG());
        screenshotCount++;

        Debug.Log($"Screenshot with polaroid frame saved: {filePath}");
    }

    Texture2D AddPolaroidFrame(Texture2D original)
    {
        int frameWidth = original.width + 40;
        int frameHeight = original.height + 100;

        // Create a new texture with a larger size for the frame
        Texture2D framedTexture = new Texture2D(frameWidth, frameHeight);
        Color white = Color.white;

        // Fill the frame with white
        for (int x = 0; x < frameWidth; x++)
        {
            for (int y = 0; y < frameHeight; y++)
            {
                framedTexture.SetPixel(x, y, white);
            }
        }

        // Paste the original screenshot with an offset for the bottom margin
        for (int x = 0; x < original.width; x++)
        {
            for (int y = 0; y < original.height; y++)
            {
                framedTexture.SetPixel(x + 20, y + 80, original.GetPixel(x, y)); // Bottom margin for polaroid look
            }
        }

        framedTexture.Apply();
        return framedTexture;
    }
}
