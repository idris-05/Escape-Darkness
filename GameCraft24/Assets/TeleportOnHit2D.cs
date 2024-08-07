using UnityEngine;

public class TeleportOnHit2D : MonoBehaviour
{
    private GameObject player;  // Reference to the player GameObject
    public float offset = 0.5f; // Offset to prevent player from getting stuck

    void Start()
    {
        // Find the player GameObject by name
        player = GameObject.Find("playergamecraft");

        // Check if the player GameObject was found
        if (player == null)
        {
            Debug.LogError("Player GameObject with name 'playergamecraft' not found.");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player GameObject was found
        if (player != null)
        {
            // Get the collision point and normal
            Vector2 collisionPoint = collision.contacts[0].point;
            Vector2 collisionNormal = collision.contacts[0].normal;

            // Calculate the new position with an offset away from the wall
            Vector2 newPosition = collisionPoint + collisionNormal * offset;

            // Teleport the player to the new position
            player.transform.position = newPosition;

            // Destroy the bullet
            Destroy(gameObject);
        }
    }
}
