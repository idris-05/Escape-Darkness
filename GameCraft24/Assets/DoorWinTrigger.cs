using Platformer.Mechanics;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class DoorWinTrigger : MonoBehaviour
{
    public int indx;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object is the player
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // Retrieve the PlayerController component from the player
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

            // Check if the player has the key
            if (playerController != null && playerController.hasKey)
            {
                // Display a win message
                Debug.Log("Congratulations! You won!");

                // Scene transition code would go here
                // Uncomment the following line to load the next scene
                 SceneManager.LoadScene(indx); 
            }
            else
            {
                // Display a message if the player doesn't have the key
                Debug.Log("You need the key to win!");
            }
        }
    }
}
