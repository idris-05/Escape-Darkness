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
    public bool bulletInAir = false;
    public  bool hasShotInJump = false;
  
    private GameObject rotatePointObject;

    private PlayerController playerController;
    public  bool activatecalldown;
    private float calldown;
    private float finishedCalldown = 0.1f;
    public bool ishided = false;
    public SpriteRenderer rotatepointSpriteRenderer;

    void Start()
    {
        maincam = Camera.main;
        playerController = FindObjectOfType<PlayerController>();
        rotatePointObject = GameObject.Find("rotatepoint");
    }

    void Update()
    {
        if (playerController.shootEnabled)
        {
        if (EscapeKeyHandler.gameIsPaused) return;

        mousePos = maincam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotz = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotz);

        // Reset canFire after the cooldown period
        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBtwFiring)
            {
                canFire = true;
                timer = 0;
                if (rotatePointObject != null)
                {
                    rotatepointSpriteRenderer.enabled = true;
                    ishided = false; // Make rotate point object visible again
                }
            }
        }

        if (activatecalldown)
        {
            rotatepointSpriteRenderer.enabled = false;
            calldown += Time.deltaTime;
            if (calldown > finishedCalldown)
            {
                activatecalldown = false;
                calldown = 0;
            }
        }

        bool isJumping = playerController.jumpState == PlayerController.JumpState.Jumping ||
                         playerController.jumpState == PlayerController.JumpState.InFlight;

        // Check if any PointInPolygonChecker has check set to true
        bool canShoot = CanShoot();
        if (canShoot)
        {
            if (Input.GetMouseButton(0) && canFire && !bulletInAir && !activatecalldown)
            {
                if (isJumping)
                {
                    if (!hasShotInJump)
                    {
                        Debug.Log("Shooting...");
                        canFire = false;
                        hasShotInJump = true;
                        bulletInAir = true;
                        if (rotatePointObject != null)
                        {
                            rotatepointSpriteRenderer.enabled = false; // Hide rotate point object when shooting
                        }
                        GameObject bulletInstance = Instantiate(bulletPrefab, bulletTr.position, Quaternion.identity);
                        bulletInstance.GetComponent<bulletscript>().Initialize(this);
                    }
                    else
                    {
                        Debug.Log("Cannot shoot more than once while in flight.");
                    }
                }
                else if (!hasShotInJump)
                {
                    Debug.Log("Shooting...");
                    hasShotInJump = true;
                    bulletInAir = true;
                    if (rotatePointObject != null)
                    {
                        rotatepointSpriteRenderer.enabled = false; // Hide rotate point object when shooting
                    }
                    GameObject bulletInstance = Instantiate(bulletPrefab, bulletTr.position, Quaternion.identity);
                    bulletInstance.GetComponent<bulletscript>().Initialize(this);
                }
                else
                {
                    Debug.Log("Cannot shoot more than once while on the ground.");
                }
            }
        }

        if (!isJumping && !bulletInAir && !activatecalldown)
        {
           
         
            hasShotInJump = false;
            if (rotatePointObject != null)
            {
                rotatepointSpriteRenderer.enabled = true; // Make rotate point object visible again
            }
        }
    }}

    public void BulletHit()
    {
        bulletInAir = false;
        Debug.Log("Bullet hit!");
        if (rotatePointObject != null)
        {
            rotatepointSpriteRenderer.enabled = false; // Hide rotate point object when the bullet hits something
        }
        activatecalldown = true;
    }

    private bool CanShoot()
    {
        PointInPolygonChecker[] checkers = FindObjectsOfType<PointInPolygonChecker>();
        foreach (PointInPolygonChecker checker in checkers)
        {
            if (checker.check)
            {
                return false; // If any checker has check as true, prevent shooting
            }
        }
        return true; // Allow shooting if all checkers have check as false
    }
}