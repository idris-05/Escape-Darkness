using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletscript : MonoBehaviour
{
    private Vector3 mousepos;
    private Camera maincam;
    private Rigidbody2D rb;
    public float force = 10f;

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
    }

    void Update()
    {
        // Optional: add logic if needed
    }
}
