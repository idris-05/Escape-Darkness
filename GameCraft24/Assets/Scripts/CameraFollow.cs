using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
   public float Followspeed = 2f;
    public float yOffset = 1f;  
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        

        Vector3 newpos = new Vector3(target.position.x,target.position.y + yOffset, -10f);
        transform.position = Vector3.Slerp(transform.position, newpos ,Followspeed* Time.deltaTime);
    }
}
