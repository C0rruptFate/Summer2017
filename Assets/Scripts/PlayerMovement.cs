using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [Tooltip("Force applied to the short jump.")]
    public int shortJumpForce = 5; //Force applied to the short jump.

    //components
    private Rigidbody2D rb;

    //horizontal movement fields
    [HideInInspector]
    public float hDir;
    [SerializeField]
    private float runForce;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float decelerationForce;
    [SerializeField]
    private float minSpeed;

    private Vector3 groundJumpForce;

    //Jumping
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


    private bool groundJumpInitiated = false;
    private int currentJumpTimer = 0;
    [HideInInspector]public int bounceJumpsUsed;
    private int arialJumpsUsed;
    [HideInInspector]public bool grounded = false;
    private bool enemyBelow = false;
    private bool playerBelow = false;


    [HideInInspector]public bool blocking;

    void Start () {
        //initialize components
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	private void Update () {
        //get player horizontal input
        hDir = Input.GetAxis("Horizontal");
        if (!blocking)
        {
            MovingPlayer();
        }

        //Player jump
        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                groundJumpForce.y = shortJumpForce;
                rb.AddForce(groundJumpForce, ForceMode2D.Impulse);
                blocking = false;
                PlayerJump();
            }
            else
            {
                PlayerJump();
            }
        }

        if (Input.GetButton("Jump") && groundJumpInitiated && (maxJumpTimer - currentJumpTimer == fullJumpLimit))
        {
            // do full jump
            groundJumpForce.y = fullJumpForce;
            Debug.Log("full jump " + (maxJumpTimer - currentJumpTimer));
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
                Debug.Log("Enter Ground");
                break;
            case "Enemy":
                enemyBelow = true;
                Debug.Log("Enter Enemy");
                break;
            case "Player":
                playerBelow = true;
                Debug.Log("Enter Player");
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
                Debug.Log("Exit Ground");
                break;
            case "Enemy":
                enemyBelow = false;
                Debug.Log("Exit Enemy");
                break;
            case "Player":
                playerBelow = false;
                Debug.Log("Exit Player");
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
            Debug.Log("Ground Jump");
        }
        else if ((enemyBelow || playerBelow) && bounceJumpsUsed < bounceJumpsAllowed)
        {
                //[TODO]Attach to the JumpAttack script from the attack script
                //JumpAttack();
        }
        else
        {
            if (arialJumpsUsed < arialJumpsAllowed)
            {
                // Reset our velocity
                rb.velocity = new Vector2(rb.velocity.x, 0.0f);
                // Arial Jump
                Debug.Log("Air Jump used" + rb.velocity.y);
                Vector2 arialJump = new Vector2();
                arialJump.y = arialJumpForce;
                rb.AddForce(arialJump, ForceMode2D.Impulse);
                arialJumpsUsed++;
            }
        }
    }

    public void PlayerFacing()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
            }
            else
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
            }
        }
    }

    public void MovingPlayer()
    {
        if (hDir != 0 && Mathf.Abs(rb.velocity.x) < maxSpeed) //if horizontal input is active and character is below max speed
            rb.AddForce(new Vector2(hDir * runForce * Time.deltaTime, 0)); //apply horizontal movement force
        else if (hDir == 0 && Mathf.Abs(rb.velocity.x) > minSpeed) //if horizontal is inactive but character is still moving
            rb.AddForce(new Vector2(-(Mathf.Sign(rb.velocity.x)) * decelerationForce * Time.deltaTime, 0f)); //apply deceleration force
    }
}
