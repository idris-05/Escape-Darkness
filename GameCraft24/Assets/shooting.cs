using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    private Camera maincam;
    private Vector3 mousePos;
    public GameObject bullet;
    public Transform bulletTr;
    public bool canFire = true;
    private float timer;
    public float tumebtwfir = 0.5f;
    public

    void Start()
    {
        maincam = Camera.main;
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
            if (timer > tumebtwfir)
            {
                canFire = true;
                timer = 0;
            }
        }

        if (Input.GetMouseButton(0) && canFire)
        {
            Debug.Log("Shooting...");
            canFire = false;
            Instantiate(bullet, bulletTr.position, Quaternion.identity);
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                Debug.Log("try Shooting...");

            }

        }
    }
}
