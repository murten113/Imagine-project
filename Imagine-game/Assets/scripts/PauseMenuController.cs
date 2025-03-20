using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // For accessing UI elements like Buttons

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuUI;  // Reference to the pause menu UI panel
    public MonoBehaviour firstPersonMovementScript;  // Reference to the movement script (e.g., FirstPersonController script)
    public Camera playerCamera;  // Reference to the camera (optional, in case you want to lock/unlock it)

    private bool isPaused = false;

    void Update()
    {
        // Check for Escape key press to toggle pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // Function to open the pause menu
    public void Pause()
    {
        pauseMenuUI.SetActive(true);  // Activate the pause menu UI
        isPaused = true;

        // Disable the movement script so the player can't move while the menu is open
        if (firstPersonMovementScript != null)
        {
            firstPersonMovementScript.enabled = false;
        }

        // Lock the cursor to the screen and show it (so you can interact with the buttons)
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Function to resume the game
    public void Resume()
    {
        pauseMenuUI.SetActive(false);  // Hide the pause menu UI
        isPaused = false;

        // Enable the movement script so the player can move again
        if (firstPersonMovementScript != null)
        {
            firstPersonMovementScript.enabled = true;
        }

        // Lock the cursor back and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Function to load the main menu scene (replace "MainMenu" with your actual main menu scene name)
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Function to quit the game
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // Stops the game in the editor
#else
        Application.Quit();  // Quit the game in a build
#endif
    }
}
