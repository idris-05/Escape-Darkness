/*using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool allowed = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object we collided with is on the "Ground" layer
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            allowed = false;
            Debug.Log("Entered ground - allowed is now: " + allowed);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the object we exited collision with is on the "Ground" layer
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            allowed = true;
            Debug.Log("Exited ground - allowed is now: " + allowed);
        }
    }
}

*/