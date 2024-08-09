using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using System.Data.Common;

namespace Platformer.Mechanics
{
    public class PlayerController : KinematicObject
    {
        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;
        public float maxSpeed = 7;
        public float jumpTakeOffSpeed = 7;
        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        public Collider2D collider2d;
        public AudioSource audioSource;
        public Health health;
        public bool controlEnabled = true;
        public bool is_in_shooting_mod = false;
        public GameObject rotatepoint;
        private shooting shootingScript;  // Reference to the shooting script

        bool jump;
        Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();
        /// <summary>
        //added by adem
        //
        public Rigidbody2D rigidbody;

        public GameObject cat;
        public float moveSpeed = 5f; // Speed of the movement
        public float moveDistance = 30f; // Distance to move before stopping

        private float distanceMoved = 0f;
        private Vector2 startPosition;

        public bool shootEnabled;
        public bool hasKey = false;
        //added by adem

        public Bounds Bounds => collider2d.bounds;

        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();


            // Get the shooting script from the rotatepoint object
            if (rotatepoint != null)
            {
                shootingScript = rotatepoint.GetComponent<shooting>();
                if (shootingScript == null)
                {
                    Debug.LogError("Shooting script not found on rotatepoint object.");
                }
            }
            else
            {
                Debug.LogError("Rotatepoint object not found.");
            }
            //added by adem
            FirstSceneStart();
        }

        protected override void Update()
        {
            //added by adem
            if (cat != null)
            {
                FirstSceneUpdate();
            } else
            {

                if (controlEnabled)
                {
                    // the player can shoot

                    move.x = Input.GetAxis("Horizontal");
                    if (jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
                    {
                        jumpState = JumpState.PrepareToJump;
                    }
                    else if (Input.GetButtonUp("Jump"))
                    {
                        stopJump = true;
                        Schedule<PlayerStopJump>().player = this;
                    }
                }
                else
                {
                    move.x = 0;
                }
                UpdateJumpState();
                base.Update();
            }
        }


        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    // Play the before-jump animation
                    StartCoroutine(AnimationControllerInGame.Instance.PlayBeforeJumpAnimation(transform.position));
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        //play the after-jump animation
                        StartCoroutine(AnimationControllerInGame.Instance.PlayAfterJumpAnimation(transform.position));
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
                case JumpState.Grounded:
                    if (!IsGrounded)
                    {
                        jumpState = JumpState.InFlight;
                    }
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            if (jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * model.jumpDeceleration;
                }
            }

            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }

        public void Bounce(float force)
        {
            if (IsGrounded)
            {
                velocity.y = force;
                jumpState = JumpState.InFlight;
            }
            else
            {
                velocity.y = force;
                jumpState = JumpState.InFlight;
            }
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }





        public void FirstSceneStart()
        {
            startPosition = transform.position;
            shootEnabled = false;
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
            } else {

                // the player is stopped
                rigidbody.velocity = Vector2.zero;
                // the player is grounded
            }
         



        }
        public void OnTriggerEnter2D(Collider2D collision) 
            {
                // Check if the player collides with an object with the "key" layer
                if (collision.gameObject.layer == LayerMask.NameToLayer("key"))
                {
                    // Player has obtained the key
                    hasKey = true;

                    // Optionally, you can destroy the key object after picking it up
                    Destroy(collision.gameObject);

        // Display a message for testing
                    Debug.Log("Key obtained!");
                }
}
 
      
            // Other code...

        public void ResetSpeed()
            {
                velocity = Vector2.zero;
                targetVelocity = Vector2.zero; // Reset targetVelocity if accessible
            }
    }

}
   
