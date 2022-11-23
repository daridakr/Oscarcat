using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;
using UnityEngine.UI;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
        private static PlayerController instance;

        public static PlayerController Instance
        {
            get
            {
                if (instance == null) instance = FindObjectOfType<PlayerController>();
                return instance;
            }
        }

        /// <summary>
        /// The player's joystick
        /// </summary>
        public Joystick joystick;

        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;

        [SerializeField]
        Image[] healthImages;

        int countOfLives = 3;

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
        /*internal new*/ public Collider2D collider2d;
        /*internal new*/ public AudioSource audioSource;
        public Health health;
        public float healthValue = 100;
        public bool controlEnabled = true;

        private int countOfFeed;
        private int countOfObediencePoints;

        [SerializeField]
        private Stat healthBar;
        [SerializeField]
        private Stat hungerBar;
        [SerializeField]
        private Stat thristBar;

        bool jump;
        Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public Bounds Bounds => collider2d.bounds;

        public int CountOfFeed { get => countOfFeed; set => countOfFeed = value; }
        public int CountOfObediencePoints { get => countOfObediencePoints; set => countOfObediencePoints = value; }
        public int CountOfLives { get => countOfLives; set => countOfLives = value; }
        public Image[] HealthImages { get => healthImages; set => healthImages = value; }
        public Stat HealthBar { get => healthBar; set => healthBar = value; }
        public Stat HungerBar { get => hungerBar; set => hungerBar = value; }
        public Stat ThristBar { get => thristBar; set => thristBar = value; }

        void Awake()
        {
            health = GetComponent<Health>();
            HealthBar.Initialize(healthValue, healthValue);
            HungerBar.Initialize(healthValue, healthValue);
            ThristBar.Initialize(healthValue, healthValue);
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            PlayerSpawn.playerPosition = model.spawnPoint.transform.position;
            
        }

        protected override void Update()
        {
            if (controlEnabled)
            {
                float verticalMove = joystick.Vertical;
                //move.x = Input.GetAxis("Horizontal");
                move.x = joystick.Horizontal;
                if (jumpState == JumpState.Grounded && verticalMove >= .5f)
                    jumpState = JumpState.PrepareToJump;
                else if (verticalMove >= 5f)
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

            //HungerBar.CurrentValue -= 0.006f;
            //ThristBar.CurrentValue -= 0.008f;

            //if (HungerBar.CurrentValue <= 0) HealthBar.CurrentValue -= 5;
            //if (ThristBar.CurrentValue <= 0) HealthBar.CurrentValue -= 10;
            if (HealthBar.CurrentValue <= 0) Simulation.Schedule<PlayerDeath>(0);

            base.Update();
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

            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }
       
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Obstacle"))
            {
                if (PlayerController.Instance.CountOfObediencePoints != 0) PlayerController.Instance.CountOfObediencePoints--;
                collision.attachedRigidbody.AddForce(transform.right * 10.0F, ForceMode2D.Force);
            }
            if (collision.CompareTag("Door"))
            {
                collision.attachedRigidbody.bodyType = RigidbodyType2D.Dynamic;
                collision.attachedRigidbody.AddForce(transform.up * -10.0F, ForceMode2D.Force);
            }
            if (collision.CompareTag("FirstTeacher"))
            {
                UIManager.Instance.SetDialogActive();
            }
            if (collision.CompareTag("Secret"))
            {
                GameObject gameObject = GameObject.FindWithTag("SecretTokens");
                gameObject.transform.position = new Vector3(1.340055f, -1.247523f, -2.370941f);
            }
        }
    }
}