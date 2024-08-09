using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 respawnPoint = new Vector3(0, 0, 0); // Define the respawn point at (0, 0, 0)
    private shooting shootingScript;

    void Start()
    {
        // Retrieve the shooting script directly
        shootingScript = FindObjectOfType<shooting>();

        if (shootingScript == null)
        {
            Debug.LogError("Shooting script not found.");
        }
    }

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
        // Destroy all objects with the tag "bullette"
        DestroyAllBullets();

        // Reset the shooting variables
        ResetShootingVariables();

        // Move the player to the respawn point
        transform.position = respawnPoint;

        // Optionally, reset any other states or variables
        // For example, you could reset health, velocity, etc.
        Debug.Log("Player has respawned at (0,0).");
    }

    void DestroyAllBullets()
    {
        // Find all GameObjects with the tag "bullette"
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("bullette");

        // Loop through and destroy each one
        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }

        Debug.Log("All bullets have been destroyed.");
    }

    void ResetShootingVariables()
    {
        if (shootingScript != null)
        {
            shootingScript.bulletInAir = false;
            shootingScript.hasShotInJump = false;
            shootingScript.canFire = false;
            shootingScript.activatecalldown = false;

            Debug.Log("Shooting variables have been reset.");
        }
    }
}
