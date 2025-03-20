using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class PhotoBookManager : MonoBehaviour
{
    public GameObject photoBookUI; // The UI panel for the photo book
    public Image[] photoSlots; // Assign 4 UI Image components in the Inspector
    public Button nextPageButton;
    public Button prevPageButton;
    public string screenshotFolder = "Screenshots";

    private List<string> photoPaths = new List<string>();
    private int currentPage = 0;
    private int photosPerPage = 4;
    private string folderPath;
    private bool isOpen = false;

    // Reference to FirstPersonController
    public FirstPersonController firstPersonController;

    void Start()
    {
        folderPath = Path.Combine(Application.persistentDataPath, screenshotFolder);
        LoadPhotos();
        DisplayCurrentPage();
        photoBookUI.SetActive(false);

        nextPageButton.onClick.AddListener(NextPage);
        prevPageButton.onClick.AddListener(PreviousPage);

        if (firstPersonController == null)
        {
            Debug.LogError("FirstPersonController is not assigned!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            LoadPhotos();
            TogglePhotoBook();
        }
    }

    void TogglePhotoBook()
    {
        isOpen = !isOpen;
        photoBookUI.SetActive(isOpen);

        if (isOpen)
        {
            // Unlock the cursor when the photo book is open
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Disable first-person camera mouse control
            if (firstPersonController != null)
            {
                firstPersonController.enabled = false;  // Disable the first-person controller
            }

            DisplayCurrentPage();
        }
        else
        {
            // Lock the cursor again when the photo book is closed
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Re-enable first-person camera mouse control
            if (firstPersonController != null)
            {
                firstPersonController.enabled = true;  // Re-enable the first-person controller
            }
        }
    }

    void LoadPhotos()
    {
        if (Directory.Exists(folderPath))
        {
            string[] files = Directory.GetFiles(folderPath, "*.png");
            photoPaths = new List<string>(files);
        }
        else
        {
            Debug.LogWarning("Photo folder not found!");
        }
    }

    void DisplayCurrentPage()
    {
        for (int i = 0; i < photoSlots.Length; i++)
        {
            int photoIndex = currentPage * photosPerPage + i;
            if (photoIndex < photoPaths.Count)
            {
                StartCoroutine(LoadImage(photoPaths[photoIndex], photoSlots[i]));
            }
            else
            {
                photoSlots[i].sprite = null;
                photoSlots[i].color = new Color(1, 1, 1, 0); // Hide empty slots
            }
        }

        prevPageButton.interactable = (currentPage > 0);
        nextPageButton.interactable = (currentPage + 1) * photosPerPage < photoPaths.Count;
    }

    IEnumerator LoadImage(string path, Image targetImage)
    {
        byte[] fileData = File.ReadAllBytes(path);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(fileData);
        targetImage.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        targetImage.color = Color.white;
        yield return null;
    }

    public void NextPage()
    {
        if ((currentPage + 1) * photosPerPage < photoPaths.Count)
        {
            currentPage++;
            DisplayCurrentPage();
        }
    }

    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            DisplayCurrentPage();
        }
    }
}
