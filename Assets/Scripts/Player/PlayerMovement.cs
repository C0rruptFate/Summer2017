using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    //Player
    [HideInInspector]
    public int playerNumber = 1;
    [HideInInspector]
    public string horizontalMovement;
    [HideInInspector]
    public string jumpMovement;

    //components
    private Rigidbody2D rb;
    [HideInInspector] public PlayerAttacks playerAttacks;
    [HideInInspector] public PlayerHealth playerHealth;

    //horizontal movement fields
    [HideInInspector]
    public float horizontalDir;
    [SerializeField]
    private float runForce;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float decelerationForce;
    [SerializeField]
    private float minSpeed;
    private float player_width;
    private float player_height;

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
    private Vector3 groundJumpForce;


    private bool groundJumpInitiated = false;
    private int currentJumpTimer = 0;
    [HideInInspector]public int bounceJumpsUsed;
    private int arialJumpsUsed;
    [HideInInspector]public bool grounded = false;
    [HideInInspector]public bool enemyBelow = false;
    [HideInInspector]public bool playerBelow = false;

    void Start () {
        GetComponent<PlayerHealth>().playerMovement = GetComponent<PlayerMovement>();
        GetComponent<PlayerAttacks>().playerMovement = GetComponent<PlayerMovement>();
        playerNumber = playerHealth.playerNumber;

        player_width = GetComponent<SpriteRenderer>().bounds.size.x;
        player_height = GetComponent<SpriteRenderer>().bounds.size.y;

        //Setup what player I control
        horizontalMovement = "Horizontal" + playerNumber;
        jumpMovement = "Jump" + playerNumber;
        //Debug.Log("horizontalMovement" + gameObject + horizontalMovement);
        //Debug.Log("jumpMovement" + gameObject + jumpMovement);

        //initialize components
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	private void Update () {

        PlayerFacing();
        ScreenCollisions();
        //get player horizontal input
        horizontalDir = Input.GetAxis(horizontalMovement);
        if (!playerAttacks.blocking)
        {
            MovingPlayer();
        }

        //Player jump

        //DISABLE this if we want to player to need to push jump to bounce off of enemies/players.
        if ((enemyBelow || playerBelow) && bounceJumpsUsed < bounceJumpsAllowed)
        {
            playerAttacks.JumpAttack();
        }//

        if (Input.GetButtonDown(jumpMovement))
        {
            if (grounded)
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
    }

    void FixedUpdate()
    {
        if (currentJumpTimer > 0)
        {
            currentJumpTimer--;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            
            case "Ground":
                grounded = true;
                arialJumpsUsed = 0;
                bounceJumpsUsed = 0;
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

    void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ground":
                grounded = false;
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

    private void PlayerJump()
    {
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
        {
            if (arialJumpsUsed < arialJumpsAllowed)
            {
                // Reset our velocity
                rb.velocity = new Vector2(rb.velocity.x, 0.0f);
                // Arial Jump
                //Debug.Log("Air Jump used" + rb.velocity.y);
                Vector2 arialJump = new Vector2();
                arialJump.y = arialJumpForce;
                rb.AddForce(arialJump, ForceMode2D.Impulse);
                arialJumpsUsed++;
            }
        }
    }

    public void PlayerFacing()
    {
        if (Input.GetAxisRaw(horizontalMovement) != 0)
        {
            if (Input.GetAxisRaw(horizontalMovement) < 0)
            {
                transform.eulerAngles = new Vector2(transform.eulerAngles.x, 180);
            }
            else
            {
                transform.eulerAngles = new Vector2(transform.eulerAngles.x, 0);
            }
        }
    }

    public void MovingPlayer()
    {
        if (horizontalDir != 0 && Mathf.Abs(rb.velocity.x) < maxSpeed) //if horizontal input is active and character is below max speed
            rb.AddForce(new Vector2(horizontalDir * runForce * Time.deltaTime, 0)); //apply horizontal movement force
        else if (horizontalDir == 0 && Mathf.Abs(rb.velocity.x) > minSpeed) //if horizontal is inactive but character is still moving
            rb.AddForce(new Vector2(-(Mathf.Sign(rb.velocity.x)) * decelerationForce * Time.deltaTime, 0f)); //apply deceleration force

    }

    private void ScreenCollisions()
    {
        //prevent object from leaving screen boundaries
        Vector3 screen_bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
        if (transform.position.x > (screen_bounds.x - (player_width / 2)))

            transform.position = new Vector3(screen_bounds.x - (player_width / 2), transform.position.y, 0);

        else if (transform.position.x < (-screen_bounds.x + (player_width / 2)))
            transform.position = new Vector3(-screen_bounds.x + (player_width / 2), transform.position.y, 0);
        else if (transform.position.y < (-screen_bounds.y + (player_height / 2)))
            transform.position = new Vector3(transform.position.x, -screen_bounds.y + (player_height / 2), 0);
        else if (transform.position.y > (screen_bounds.y - (player_height / 2)))

            transform.position = new Vector3(transform.position.x, screen_bounds.y - (player_height / 2), 0);
    }
}
