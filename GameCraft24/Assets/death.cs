using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 respawnPoint = new Vector3(0, 0, 0); // Define the respawn point at (0, 0, 0)

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player hits a trigger with the tag "border"
        if (collision.CompareTag("border"))
        {
            Respawn();
        }
    }

    void Respawn()
    {
        // Move the player to the respawn point
        transform.position = respawnPoint;

        // Optionally, reset any other states or variables
        // For example, you could reset health, velocity, etc.
        Debug.Log("Player has respawned at (0,0).");
    }
}
