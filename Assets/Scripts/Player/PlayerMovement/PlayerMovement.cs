using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    //Player
    [HideInInspector]//Set by game manager, used to determine the player # and find the controller attached to them.
    public int playerNumber = 1;
    [HideInInspector]//Used to find what controller this is attached to.
    public string horizontalMovement;
    [HideInInspector]//Used to find what controller this is attached to.
    public string jumpMovement;

    //components
    protected Rigidbody2D rb; //My Rigidbody
    protected Collider2D[] colliders; //My Colliders
    [HideInInspector] public PlayerAttacks playerAttacks; //My player Attacks and actions.
    [HideInInspector] public PlayerHealth playerHealth; //My player HP script.

    //horizontal movement fields
    [HideInInspector]//Use to find what direction the player is trying to move
    public float horizontalDir;
    //[SerializeField] //How fast the player is moving.
    public float runForce;
    //[SerializeField] //What is this unit's max speed.
    public float maxSpeed;
    [SerializeField] //how quickly the player stops.
    protected float decelerationForce;
    [HideInInspector]//Used if the character isn't getting input but is still moving.
    public float minSpeed = 0;

    //Jumping
    [Tooltip("Force applied to the short jump.")]
    public int shortJumpForce = 5; //Force applied to the short jump.
    [Tooltip("Used to set how long a jump needs to be held down.")]
    public int maxJumpTimer = 10;
    [Tooltip("Used to check for a short jump or full jump, if the button is held down for more frames than this it will do a full jump.")]
    public int fullJumpLimit = 5; //used to check for a short jump or full jump, if the button is held down for more frames than this it will do a full jump.
    [Tooltip("Force applied to the full jump.")]
    public int fullJumpForce = 10; //Force applied to the full jump.
    //bounce off people.
    [Tooltip("How many times can you bounce off an enemy before hitting the ground?")]
    public int bounceJumpsAllowed = 1; //How many times can you bounce off an enemy or ally before hitting the ground?
    [Tooltip("How many air jumps this unit can have.")]
    public int arialJumpsAllowed = 1; //How many air jumps we can have.
    //double jump
    [Tooltip("Force applied to the air jump.")]
    public float arialJumpForce = 10f; //Force applied to the air jump.
    protected Vector3 groundJumpForce; //what the jump force is applied to.
    public GameObject jumpEffect; //Plays the jump particle effect.
    protected Transform whatsBelowMeChecker;//Checks what is below me, used to check for jumps and jump attacks.

    //Water
    [HideInInspector]//Used to find what controller this is attached to.
    public string verticalMovement;
    [HideInInspector]//Use to find what direction the player is trying to move
    public float verticalDir; 
    [HideInInspector]
    public bool inWater = false;
    public float inWaterMass = 10;
    [HideInInspector]//Should be kept at 1, if this is changed we need to change the bubble too and let Mike know.
    public float outofWaterMass = 1;
    public float inWaterMaxSpeed;
    public float inWaterRunForce;
    public float waterJumpForceMultiplier = 5;


    protected bool groundJumpInitiated = false; //have I started to jump yet?
    protected int currentJumpTimer = 0; //How long I have been holding the jump button.
    [HideInInspector]
    public int bounceJumpsUsed; //how many bounce jumps have I used.
    [HideInInspector]
    public int arialJumpsUsed; //How many airal jumps have I used.
    [HideInInspector]
    public bool grounded = false; //am I currently grounded?
    //[HideInInspector]
    public bool onADropAblePlatform = false;
    [HideInInspector]
    public bool enemyBelow = false;//Is their an enemy below me?
    [HideInInspector]
    public bool playerBelow = false;//is their a player below me?
    [HideInInspector]
    public bool facingRight;

    protected float nextEnableColliders;
    protected float enableCollidersWait = 0.01f;

    protected Animator anim;

    protected virtual void Start () {
        //Sets up the player's hp and actions scripts.
        GetComponent<PlayerHealth>().playerMovement = GetComponent<PlayerMovement>();
        GetComponent<PlayerAttacks>().playerMovement = GetComponent<PlayerMovement>();
        //initialize components
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        colliders = GetComponents<Collider2D>();
        whatsBelowMeChecker = transform.Find("Whats Below Me");
        if (whatsBelowMeChecker == null)
        {
            Debug.LogError("Can't find: " + whatsBelowMeChecker);
        }

        //Set's the player's number.
        playerNumber = playerHealth.playerNumber;

        //Setup what player I control
        horizontalMovement = "Horizontal" + playerNumber;
        verticalMovement = "Vertical" + playerNumber;
        jumpMovement = "Jump" + playerNumber;
        //Debug.Log("horizontalMovement" + gameObject + horizontalMovement);
        //Debug.Log("jumpMovement" + gameObject + jumpMovement);
    }
	
	// Update is called once per frame
	public virtual void Update () {

        //Determins what direction the player is facing
        PlayerFacing();
        //Used to control the player animations.
        AnimationMachine();
        //ScreenCollisions();
        //get player horizontal input
        horizontalDir = Input.GetAxis(horizontalMovement);
        verticalDir = Input.GetAxis(verticalMovement);

        ////////////////////////////////////////////////////////////
        if (verticalDir == -1 && onADropAblePlatform)
        {
            //Find colliders that arn't triggers.
            foreach (BoxCollider2D bc in colliders)
            {
                if (!bc.isTrigger)
                {
                    bc.enabled = false;
                    nextEnableColliders = Time.time + enableCollidersWait;
                }
                if (bc.isTrigger)
                {
                    bc.enabled = true;
                }

                //if (!bc.isTrigger)
                //{
                //    bc.enabled = true;
                //}
            }
        }

        if (Time.time >= nextEnableColliders)
        {
            foreach (BoxCollider2D bc in colliders)
            {
                if (!bc.isTrigger)
                {
                    bc.enabled = true;
                }
                if (bc.isTrigger)
                {
                    bc.enabled = false;
                }
            }
        }
        ////////////////////////////////////////////////////////////

        //allows the player to move if they arn't holding the block button.
        if (!playerAttacks.blocking)
        {
            MovingPlayer();
        }

        //Player jump

        //DISABLE this if we want to player to need to push jump to bounce off of enemies/players.

        if ((enemyBelow || playerBelow) && bounceJumpsUsed < bounceJumpsAllowed && !grounded)
        {
            playerAttacks.JumpAttack();
            Instantiate(jumpEffect, whatsBelowMeChecker.position,whatsBelowMeChecker.rotation);
        }

        if (Input.GetButtonDown(jumpMovement))
        {
            if (inWater)
            {
                //Debug.Log("Water Jump");
                //Jump code for when in water
                // Reset our velocity
                rb.velocity = new Vector2(rb.velocity.x, 0.0f);
                // Arial Jump
                //Debug.Log("Air Jump used" + rb.velocity.y);
                Vector2 waterJump = new Vector2();
                waterJump.y = arialJumpForce * waterJumpForceMultiplier;
                rb.AddForce(waterJump, ForceMode2D.Impulse);
            }
            else if (grounded)
            {
                groundJumpForce.y = shortJumpForce;
                rb.AddForce(groundJumpForce, ForceMode2D.Impulse);
                //Debug.Log("player Attacks.blocking: " + playerAttacks.blocking);
                if (playerAttacks.blocking)
                {
                    playerAttacks.blocking = false;
                    playerAttacks.blockNextFire = Time.time + playerAttacks.blockFireRate;
                }
                //Debug.Log("player Attacks.blocking: " + playerAttacks.blocking);
                PlayerJump();
            }
            else
            {
                PlayerJump();
            }
        }

        if (Input.GetButton(jumpMovement) && groundJumpInitiated && (maxJumpTimer - currentJumpTimer == fullJumpLimit))
        {
            // do full jump
            groundJumpForce.y = fullJumpForce;
            //Debug.Log("full jump " + (maxJumpTimer - currentJumpTimer));
            rb.AddForce(groundJumpForce, ForceMode2D.Impulse);
            groundJumpInitiated = false;
        }

        //water failsafe
        if (!inWater && GetComponent<Rigidbody2D>().mass == inWaterMass)
        {
            GetComponent<Rigidbody2D>().mass = outofWaterMass;
        }
    }

    public virtual void FixedUpdate()
    {
        //Counts how long the jump button has been held down.
        if (currentJumpTimer > 0)
        {
            currentJumpTimer--;
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag) //Checks what's below me.
        {
            
            case "Ground":
                grounded = true;
                anim.SetBool("Grounded", true);
                arialJumpsUsed = 0;
                bounceJumpsUsed = 0;
                if (collision.GetComponent<PlatformEffector2D>())
                {
                    onADropAblePlatform = true;
                }
                //Debug.Log("Enter Ground");
                break;
            case "Enemy":
                enemyBelow = true;
                //Debug.Log("Enter Enemy");
                break;
            case "Player":
                playerBelow = true;
                //Debug.Log("Enter Player");
                break;
            default:
                break;
        }   
    }

    public virtual void OnTriggerStay2D(Collider2D collision)
    {
        switch (collision.gameObject.tag) //Checks what's below me.
        {

            case "Ground":

                if(grounded != true)
                {
                    grounded = true;
                    anim.SetBool("Grounded", true);
                    arialJumpsUsed = 0;
                    bounceJumpsUsed = 0;
                    if (collision.GetComponent<PlatformEffector2D>())
                    {
                        onADropAblePlatform = true;
                    }
                }
                //Debug.Log("Enter Ground");
                break;
            case "Enemy":

                if (enemyBelow != true)
                {
                    enemyBelow = true;
                }
                //Debug.Log("Enter Enemy");
                break;
            case "Player":

                if (playerBelow != true)
                {
                    playerBelow = true;
                }
                //Debug.Log("Enter Player");
                break;
            default:
                break;
        }
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.gameObject.tag) //removes what's below me when I leave.
        {
            case "Ground":
                grounded = false;
                anim.SetBool("Grounded", false);
                if (collision.GetComponent<PlatformEffector2D>())
                {
                    onADropAblePlatform = false;
                }
                //Debug.Log("Exit Ground");
                break;
            case "Enemy":
                enemyBelow = false;
                //Debug.Log("Exit Enemy");
                break;
            case "Player":
                playerBelow = false;
                //Debug.Log("Exit Player");
                break;
            default:
                break;
        }
    }

    public virtual void PlayerJump()
    {
        //Does ground jump
        if (grounded)
        {
            currentJumpTimer = maxJumpTimer;
            groundJumpInitiated = true;
            //Debug.Log("Ground Jump");
        }
        //ENABLE this if we want to player to need to push jump to bounce off of enemies/players.
        //else if ((enemyBelow || playerBelow) && bounceJumpsUsed < bounceJumpsAllowed)
        //{
        //    playerAttacks.JumpAttack();
        //}
        else
        {//uses air jump if I have any left.
            if (arialJumpsUsed < arialJumpsAllowed)
            {
                //play jump effect
                Instantiate(jumpEffect, whatsBelowMeChecker.position, whatsBelowMeChecker.rotation);
                // Reset our velocity
                //rb.velocity = new Vector2(rb.velocity.x, 0.0f);
                rb.velocity = new Vector2(0.0f, 0.0f);
                // Arial Jump
                //Debug.Log("Air Jump used" + rb.velocity.y);
                Vector2 arialJump = new Vector2();
                arialJump.y = arialJumpForce;
                rb.AddForce(arialJump, ForceMode2D.Impulse);
                arialJumpsUsed++;
            }
        }
    }

    public virtual void PlayerFacing()
    {//what direction is the player facing/last moving
        if (Input.GetAxisRaw(horizontalMovement) != 0)
        {
            if (Input.GetAxisRaw(horizontalMovement) < 0)
            {
                //Facing left
                facingRight = false;
                transform.eulerAngles = new Vector2(transform.eulerAngles.x, 180);
            }
            else
            {
                //Facing Right
                facingRight = true;
                transform.eulerAngles = new Vector2(transform.eulerAngles.x, 0);
            }
        }
    }

    public void AnimationMachine()
    {
        if (inWater)
        {
            anim.SetBool("Walking", false);
            anim.SetFloat("vSpeed", 0.0f);
            anim.SetBool("Swimming", true);
        }
        else if (!inWater && !grounded)
        {
            anim.SetBool("Swimming", false);
            anim.SetBool("Walking", false);
            anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);
        }
        // else if (Mathf.Abs(horizontalDir) > 0 && grounded && !inWater)
        if (Mathf.Abs(horizontalDir) > 0)
         {
            //anim.SetBool("Swimming", false);

            //anim.SetFloat("vSpeed", 0.0f);
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }
    }

    public virtual void MovingPlayer()
    {//moves the player by adjusting their velocity.
        
        if (inWater)
        {
            if (horizontalDir != 0 && Mathf.Abs(rb.velocity.x) < inWaterMaxSpeed) //if horizontal input is active and character is below max speed
            {
                rb.AddForce(new Vector2(horizontalDir * inWaterRunForce * Time.deltaTime, 0)); //apply horizontal movement force
            }
            else if (horizontalDir == 0 && Mathf.Abs(rb.velocity.x) > minSpeed) //if horizontal is inactive but character is still moving
            {
                rb.AddForce(new Vector2(-(Mathf.Sign(rb.velocity.x)) * decelerationForce * Time.deltaTime, 0f)); //apply deceleration force
            }
        }
        else
        {
            if (horizontalDir != 0 && Mathf.Abs(rb.velocity.x) < maxSpeed) //if horizontal input is active and character is below max speed
            {
                rb.AddForce(new Vector2(horizontalDir * runForce * Time.deltaTime, 0)); //apply horizontal movement force
            }
            else if (horizontalDir == 0 && Mathf.Abs(rb.velocity.x) > minSpeed) //if horizontal is inactive but character is still moving
            {
                rb.AddForce(new Vector2(-(Mathf.Sign(rb.velocity.x)) * decelerationForce * Time.deltaTime, 0f)); //apply deceleration force
            }
        }


    }
}
