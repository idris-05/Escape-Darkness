using UnityEngine;

public class EscapeKeyHandler : MonoBehaviour
{
    // Reference to the GameObject you want to activate/deactivate
    public GameObject InGameMenu;

    private void Update()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle the GameObject's active state
            if (InGameMenu != null)
            {
                InGameMenu.SetActive(!InGameMenu.activeSelf);
            }
        }
    }
}
