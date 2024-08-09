using UnityEngine;

public class EscapeKeyHandler : MonoBehaviour
{
    // Reference to the GameObject you want to activate/deactivate
    public GameObject InGameMenu;

    public static bool gameIsPaused = false;

    private void Update()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle the GameObject's active state
            if (InGameMenu != null)
            {
                if (gameIsPaused) ResumeGame();
                else PauseGame();
            }
        }
    }

    public void ResumeGame()
    {

        gameIsPaused = false;
        Time.timeScale = 1f;
        InGameMenu.SetActive(false);
    }

    public void PauseGame()
    {
        gameIsPaused = true;
        Time.timeScale = 0f;
        InGameMenu.SetActive(true);
    }
}
