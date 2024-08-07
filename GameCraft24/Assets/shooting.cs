using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class shooting : MonoBehaviour
{
    private Camera maincam;
    private Vector3 mousePos;
    public GameObject bulletPrefab;
    public Transform bulletTr;
    public bool canFire = true;
    private float timer;
    public float timeBtwFiring = 0.5f;
    private bool bulletInAir = false;

    private PlayerController playerController;
    private bool hasShotInJump = false;
    private GameObject rotatePointObject;

    void Start()
    {
        maincam = Camera.main;
        playerController = FindObjectOfType<PlayerController>();
        rotatePointObject = GameObject.Find("rotatepoint");

        // Ensure rotatePointObject starts as active
        if (rotatePointObject != null)
        {
            rotatePointObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Rotate point object 'rotatepoint' not found.");
        }
    }

    void Update()
    {
        mousePos = maincam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotz = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotz);

        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBtwFiring)
            {
                canFire = true;
                timer = 0;
                if (rotatePointObject != null)
                {
                    rotatePointObject.SetActive(true);  // Make rotate point object visible again
                }
            }
        }

        bool isJumping = playerController.jumpState == PlayerController.JumpState.Jumping ||
                         playerController.jumpState == PlayerController.JumpState.InFlight;

        if (Input.GetMouseButton(0) && canFire && !bulletInAir)
        {
            if (!isJumping || (isJumping && !hasShotInJump))
            {
                Debug.Log("Shooting...");
                canFire = false;
                hasShotInJump = true;
                bulletInAir = true;
                if (rotatePointObject != null)
                {
                    rotatePointObject.SetActive(false);  // Hide rotate point object when shooting
                }
                GameObject bullet = Instantiate(bulletPrefab, bulletTr.position, Quaternion.identity);
                bullet.GetComponent<bulletscript>().Initialize(this);
            }
            else
            {
                Debug.Log("Cannot shoot while jumping more than once.");
            }
        }

        if (playerController.jumpState == PlayerController.JumpState.Grounded)
        {
            hasShotInJump = false;
        }
    }

    public void BulletHit()
    {
        bulletInAir = false;
        if (rotatePointObject != null)
        {
            rotatePointObject.SetActive(true);  // Make rotate point object visible again when the bullet hits something
        }
    }
}
