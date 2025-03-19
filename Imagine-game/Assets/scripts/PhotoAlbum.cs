using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PhotoAlbum : MonoBehaviour
{
    public GameObject albumUI;
    public Image leftPage;
    public Image rightPage;
    public Button nextButton;
    public Button prevButton;
    public Button closeButton;

    private List<Sprite> photos = new List<Sprite>();
    private int currentPage = 0;
    private bool isAlbumOpen = false;

    void Start()
    {
        albumUI.SetActive(false); // Initially hidden
        LoadPhotos();

        nextButton.onClick.AddListener(NextPage);
        prevButton.onClick.AddListener(PreviousPage);
        closeButton.onClick.AddListener(CloseAlbum);

        UpdatePages();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) // Open/Close the album with 'J'
        {
            if (isAlbumOpen)
                CloseAlbum();
            else
                OpenAlbum();
        }
    }

    void LoadPhotos()
    {
        Sprite[] loadedPhotos = Resources.LoadAll<Sprite>("Screenshots");
        photos.AddRange(loadedPhotos);
    }

    void OpenAlbum()
    {
        isAlbumOpen = true;
        albumUI.SetActive(true);

        // Unlock the cursor for UI interaction
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        UpdatePages();
    }

    void CloseAlbum()
    {
        isAlbumOpen = false;
        albumUI.SetActive(false);

        // Lock the cursor back to the center for the first-person controller
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UpdatePages()
    {
        // Show photos or keep blank if out of range
        leftPage.sprite = currentPage * 2 < photos.Count ? photos[currentPage * 2] : null;
        rightPage.sprite = currentPage * 2 + 1 < photos.Count ? photos[currentPage * 2 + 1] : null;

        prevButton.interactable = currentPage > 0;
        nextButton.interactable = (currentPage + 1) * 2 < photos.Count;
    }

    void NextPage()
    {
        if ((currentPage + 1) * 2 < photos.Count)
        {
            currentPage++;
            UpdatePages();
        }
    }

    void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdatePages();
        }
    }
}
