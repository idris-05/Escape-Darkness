using System.Collections;
using System.Collections.Generic;
using Platformer.Mechanics;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the movement
    public float moveDistance ; // Distance to move before stopping

    private float distanceMoved = 0f;
    private Vector2 startPosition;
    public Rigidbody2D rigidbody;   // Rigidbody2D component of the GameObject
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        // rigidbody = GetComponent<Rigidbody2D>();
        FirstSceneStart();
    }

    // Update is called once per frame
    void Update()
    {
        // movement
        FirstSceneUpdate();
    }



    public void FirstSceneStart()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = Vector2.zero;
         startPosition = transform.position;
         moveDistance = 30f;

    }
    public void FirstSceneUpdate()
    {
        // movement
         if (distanceMoved < moveDistance)
        {
            float moveStep = moveSpeed * Time.deltaTime;
            transform.Translate(Vector2.right * moveStep);
            distanceMoved += moveStep;
            Debug.Log("distanceMoved: " + distanceMoved);
            Debug.Log("moveDistance: " + moveDistance);
        }else{
            PlayerController playerController = player.GetComponent<PlayerController>();
            playerController.shootEnabled = true;
            Destroy(this.gameObject);


        }
    }


}