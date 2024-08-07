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
    private bool hasShotInJump = false;
    private bool hasShotWhileInFlight = false;
    private GameObject rotatePointObject;

    private PlayerController playerController;
    private bool activatecalldown;
    private float calldown;
    private float finshedcalldown = 0.1f;
    public  bool ishided=false;
    public SpriteRenderer rotatepointSpriteRenderer; 

    void Start()
    {
        
        maincam = Camera.main;
        playerController = FindObjectOfType<PlayerController>();
        rotatePointObject = GameObject.Find("rotatepoint");

        

        // Ensure rotatePointObject starts as active
      
       
    }

    void Update()
    {
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
                    ishided = false;// Make rotate point object visible again
                }
            }
        }
        if (activatecalldown)
        {
            rotatepointSpriteRenderer.enabled = false;

          

            calldown += Time.deltaTime;
            if (calldown > finshedcalldown)
            {
                activatecalldown = false;
                calldown = 0;

            }



        }

        bool isJumping = playerController.jumpState == PlayerController.JumpState.Jumping ||
                         playerController.jumpState == PlayerController.JumpState.InFlight;

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
                        rotatepointSpriteRenderer.enabled = false;

                        // Hide rotate point object when shooting
                    }
                    GameObject bullet = Instantiate(bulletPrefab, bulletTr.position, Quaternion.identity);
                    bullet.GetComponent<bulletscript>().Initialize(this);
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
                    rotatepointSpriteRenderer.enabled = false;

                    // Hide rotate point object when shooting
                }
                GameObject bullet = Instantiate(bulletPrefab, bulletTr.position, Quaternion.identity);
                bullet.GetComponent<bulletscript>().Initialize(this);
            }
            else
            {
                Debug.Log("Cannot shoot more than once while on the ground.");
            }
        }
        
        if (!isJumping && !bulletInAir  && !activatecalldown)
        {
            Debug.Log("halllol");
            hasShotWhileInFlight = false;
            hasShotInJump = false;
            rotatepointSpriteRenderer.enabled = true;



        }
        
    }

    public void BulletHit()
    {
        bulletInAir = false;
        Debug.Log("hits");
        if (rotatePointObject != null)
        {
            rotatepointSpriteRenderer.enabled = false; 
            // Make rotate point object visible again when the bullet hits something
        }
        activatecalldown = true;


    }
}
