﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {

        public float MoveSpeed = 5f;
        public Transform MovePoint;
        public float DashPower = 10f;

        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;

        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 7;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 7;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        /*internal new*/
        public Collider2D collider2d;
        /*internal new*/
        public AudioSource audioSource;
        public Health health;
        public bool controlEnabled = true;

        bool jump;
        Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public Bounds Bounds => collider2d.bounds;

        Rigidbody2D _rigidbody2d;

        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();

            MovePoint.parent = null;
        }
        protected override void Start()
        {
            base.Start();
            _rigidbody2d = GetComponent<Rigidbody2D>();
            _rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
        }

        protected override void Update()
        {

            if (controlEnabled)
            {
                move.x = Input.GetAxis("Horizontal");

                if (jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
                    jumpState = JumpState.PrepareToJump;
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

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Dash();
            }
        }

        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
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
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
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

            //if (move.x > 0.01f)
            //    spriteRenderer.flipX = false;
            //else if (move.x < -0.01f)
            //    spriteRenderer.flipX = true;

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }

        private void NewGridMovement()
        {
            transform.position = Vector3.MoveTowards(transform.position, MovePoint.position, MoveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, MovePoint.position) <= 0.05f)
            {
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
                {
                    MovePoint.position = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                }
            }

            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;
        }

        private void Dash()
        {
            Debug.Log("trying to Dash");
            //animator.SetTrigger("DashTrigger");
            targetVelocity = new Vector2(DashPower, 0f);
            //transform.Translate(targetVelocity);
            //_rigidbody2d.AddForce(targetVelocity);
            //_rigidbody2d.MovePosition(new Vector2 (transform.position.x + targetVelocity.x, transform.position.y));
        }
    }
}