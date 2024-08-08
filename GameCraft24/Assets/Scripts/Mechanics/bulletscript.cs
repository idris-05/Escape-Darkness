using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletscript : MonoBehaviour
{
    private Vector3 mousepos;
    private Camera maincam;
    private Rigidbody2D rb;
    public float force = 10f;

    private GameObject player;  // Reference to the player GameObject
    public float offset = 0.5f; // Offset to prevent player from getting stuck

    private shooting shooter;

    void Start()
    {
        maincam = Camera.main;
        rb = GetComponent<Rigidbody2D>();

        mousepos = maincam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousepos - transform.position;
        direction.z = 0; // Ensure the direction is strictly 2D
        rb.velocity = direction.normalized * force;

        float rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);

        Debug.Log("Bullet fired with velocity: " + rb.velocity);

        // Find the player GameObject by name
        player = GameObject.Find("playergamecraft");

        // Check if the player GameObject was found
        if (player == null)
        {
            Debug.LogError("Player GameObject with name 'playergamecraft' not found.");
        }
    }

    public void Initialize(shooting shooter)
    {
        this.shooter = shooter;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 newPosition = Vector2.zero;

        // Check if the player GameObject was found
        if (player != null)
        {
            // Get the collision point and normal
            Vector2 collisionPoint = collision.contacts[0].point;
            Vector2 collisionNormal = collision.contacts[0].normal;

            // Calculate the new position with an offset away from the wall
            newPosition = collisionPoint + collisionNormal * offset;
        }

        // Handle bullet hit logic
        if (shooter != null) shooter.BulletHit();

        if (player != null) StartCoroutine(PlayAnimationAndTeleport(player, newPosition));
        else Destroy(gameObject); // Destroy the bullet
    }

    IEnumerator PlayAnimationAndTeleport(GameObject player, Vector2 newPosition)
    {
        // Disable the sprite renderer and collider to prevent further physical interactions
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();

        if (spriteRenderer != null) spriteRenderer.enabled = false;

        if (collider != null) collider.enabled = false;

        // Teleport the player to the new position
        player.transform.position = newPosition;
        yield return StartCoroutine(AnimationControllerInGame.Instance.PlayteleportWithBulletAnimation((Vector3)newPosition));

        // Destroy the bullet
        Destroy(gameObject);
    }



    void OnTriggerEnter2D(Collider2D collider)
    {
        // Handle bullet hitting a "border" collider
        if (collider.CompareTag("border"))
        {
            if (shooter != null)
            {
                shooter.BulletHit();
            }

            Destroy(gameObject);
        }
    }
}
