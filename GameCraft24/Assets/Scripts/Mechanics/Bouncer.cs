using Platformer.Mechanics;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    public float launchForce = 10f;  // Force with which the player is launched

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with the player
        if (collision.gameObject.name == "playergamecraft")
        {
            // Get the PlayerController component of the player
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

            if (playerController != null)
            {
                // Launch the player upwards using the Bounce method
                playerController.Bounce(launchForce);
            }
        }
    }
}
