using UnityEngine;
using System.IO;

public class CameraCapture : MonoBehaviour
{
    public Camera camera;
    public int imageWidth = 1080;
    public int imageHeight = 1350; // Aspect ratio similar to a polaroid
    public string screenshotFolder = "Screenshots";
    private int screenshotCount = 0;

    private string folderPath;

    void Start()
    {
        folderPath = Path.Combine(Application.persistentDataPath, screenshotFolder);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
            Debug.Log("Created folder: " + folderPath);
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(1) && Input.GetMouseButtonDown(0))
        {
            TakePolaroidScreenshot();
        }
    }

    void TakePolaroidScreenshot()
    {
        RenderTexture renderTex = new RenderTexture(imageWidth, imageHeight, 24);
        camera.targetTexture = renderTex;

        Texture2D screenshot = new Texture2D(imageWidth, imageHeight, TextureFormat.RGB24, false);
        camera.Render();
        RenderTexture.active = renderTex;
        screenshot.ReadPixels(new Rect(0, 0, imageWidth, imageHeight), 0, 0);
        camera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(renderTex);

        Texture2D polaroid = AddPolaroidFrame(screenshot);

        string filePath = Path.Combine(folderPath, $"screenshot_{screenshotCount}.png");
        File.WriteAllBytes(filePath, polaroid.EncodeToPNG());
        screenshotCount++;

        Debug.Log($"Screenshot with polaroid frame saved: {filePath}");
    }

    Texture2D AddPolaroidFrame(Texture2D original)
    {
        int frameWidth = original.width + 40;
        int frameHeight = original.height + 100;

        Texture2D framedTexture = new Texture2D(frameWidth, frameHeight);
        Color white = Color.white;

        for (int x = 0; x < frameWidth; x++)
        {
            for (int y = 0; y < frameHeight; y++)
            {
                framedTexture.SetPixel(x, y, white);
            }
        }

        for (int x = 0; x < original.width; x++)
        {
            for (int y = 0; y < original.height; y++)
            {
                framedTexture.SetPixel(x + 20, y + 80, original.GetPixel(x, y));
            }
        }

        framedTexture.Apply();
        return framedTexture;
    }
}