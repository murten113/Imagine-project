using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class EndBook : MonoBehaviour
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

    void Start()
    {
        folderPath = Path.Combine(Application.persistentDataPath, screenshotFolder);
        LoadPhotos();
        DisplayCurrentPage();
        photoBookUI.SetActive(true);

        nextPageButton.onClick.AddListener(NextPage);
        prevPageButton.onClick.AddListener(PreviousPage);

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
