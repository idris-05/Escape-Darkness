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
            StartCoroutine(TeleportPlayerWithAnimation());
        }
    }

    IEnumerator TeleportPlayerWithAnimation()
    {
        // start every methode after the end of the previous one
        yield return StartCoroutine(AnimationControllerInGame.Instance.PlayDisappearAnimation(transform.position));
        TeleportPlayer();
        StartCoroutine(AnimationControllerInGame.Instance.PlayAppearAnimation(transform.position));
    }

    private void TeleportPlayer()
    {
        gameObject.SetActive(false);
        Vector3 currentPosition = transform.position;

        // section commented only for tests , and because teleporting is not working correctely
        // if (currentState == TeleportState.State1)
        // {
        //     currentPosition.y -= 75.95f;
        //     currentState = TeleportState.State2; // Change to the next state
        // }
        // else if (currentState == TeleportState.State2)
        // {
        //     currentPosition.y += 75.95f;
        //     currentState = TeleportState.State1; // Change to the next state
        // }
        // transform.position = currentPosition        

        transform.position = currentPosition + new Vector3(2f, 0, 0);

        gameObject.SetActive(true);

    }
}
