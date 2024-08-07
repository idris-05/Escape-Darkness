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

    private void Start()
    {
        // Initial state setup if needed
        currentState = TeleportState.State1;
    }

    private void Update()
    {
        // Check if the player is in State1 or State2 and teleport accordingly
        if (Input.GetKeyDown(KeyCode.T)) // Assuming "T" key is used for teleportation
        {
            TeleportPlayer();
        }
    }

    private void TeleportPlayer()
    {
        Vector3 currentPosition = transform.position;

        if (currentState == TeleportState.State1)
        {
            currentPosition.y -= 75.95f;
            currentState = TeleportState.State2; // Change to the next state
        }
        else if (currentState == TeleportState.State2)
        {
            currentPosition.y += 75.95f;
            currentState = TeleportState.State1; // Change to the next state
        }

        transform.position = currentPosition;
    }
}
