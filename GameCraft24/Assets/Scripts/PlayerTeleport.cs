using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    public enum TeleportState
    {
        State1,
        State2
    }

    public TeleportState currentState = TeleportState.State1;

    private bool canTeleport = false; // Boolean to check if teleportation is allowed

    private void Start()
    {
        // Initial state setup if needed
        currentState = TeleportState.State1;
    }

    private void Update()
    {
        // Check if the player is in State1 or State2 and teleport accordingly
        if (Input.GetKeyDown(KeyCode.T) && canTeleport && !IsBulletPresent()) // Assuming "T" key is used for teleportation
        {
            TeleportPlayer();
        }
    }

    private void TeleportPlayer()
    {
        Vector3 currentPosition = transform.position;

        if (currentState == TeleportState.State1)
        {
            currentPosition.y -= 29.7f;
            currentState = TeleportState.State2; // Change to the next state
        }
        else if (currentState == TeleportState.State2)
        {
            currentPosition.y += 29.7f;
            currentState = TeleportState.State1; // Change to the next state
        }

        transform.position = currentPosition;
    }

    // This function is called when the player starts colliding with another object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("tpblocker"))
        {
            canTeleport = false; // Disable teleportation
            
        }
    }

    // This function is called when the player stops colliding with another object
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("tpblocker"))
        {
            canTeleport = true; // Re-enable teleportation
           
        }
    }

    // Method to check if any bullet with the tag 'bullette' exists in the scene
    private bool IsBulletPresent()
    {
        // Find the first GameObject with the tag 'bullette'
        GameObject bullet = GameObject.FindGameObjectWithTag("bullette");

        if (bullet != null)
        {
            Debug.Log("A bullet exists in the scene, you cannot teleport!");
            return true; // Bullet is present, teleportation is blocked
        }

        return false; // No bullet found, teleportation is allowed
    }
}
