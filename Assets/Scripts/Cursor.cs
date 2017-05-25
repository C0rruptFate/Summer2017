using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cursor : MonoBehaviour {

    //Find Cursor and who is belongs to.
    public int playerNumber = 1;
    public float speed;

    [HideInInspector]
    public bool activePlayer = false;
    [HideInInspector]
    public string playerSelect;
    [HideInInspector]
    public string playerCancel;

    [HideInInspector]
    public string horizontalMovement;
    [HideInInspector]
    public string verticalMovement;
    [HideInInspector]
    public bool mouseMovementAllowed = false;

    private float horizontalDir;
    private float verticalDir;

    //[HideInInspector]
    public bool characterSelected = false;
    //[HideInInspector]
    public GameObject characterSelectedPrefab = null;
    private GameObject possibleCharacter;

    private Rigidbody2D rb;
    private GameObject characterSelectionManager;

    // Use this for initialization
    void Start () {
        horizontalMovement = "Horizontal" + playerNumber;
        verticalMovement = "Vertical" + playerNumber;
        playerSelect = "Jump" + playerNumber;
        playerCancel = "Ranged" + playerNumber;
        gameObject.name = "Player " + playerNumber + " Cursor";

        rb = GetComponent<Rigidbody2D>();
        GetComponent<Image>().enabled = false;
        characterSelectionManager = GameObject.Find("Character Selection Manager");
    }
	
	// Update is called once per frame
	void Update () {

        horizontalDir = Input.GetAxis(horizontalMovement);
        verticalDir = Input.GetAxis(verticalMovement);

        //Enables the player
        if (Input.GetButtonDown(playerSelect) && !activePlayer)
        {
            activePlayer = true;
            characterSelectionManager.GetComponent<CharacterSelectionManager>().playerCount++;
            mouseMovementAllowed = true;
            //Debug.Log(activePlayer);
            GetComponent<Image>().enabled = true;
        }

        if (mouseMovementAllowed)
        {
            MouseMovement();
        }

        //Selects the character
        if (Input.GetButtonDown(playerSelect) && possibleCharacter != null)
        {
            if(characterSelected == false)
            {
                characterSelectionManager.GetComponent<CharacterSelectionManager>().playersReady++;
            }
            characterSelected = true;
            mouseMovementAllowed = false;
            characterSelectedPrefab = possibleCharacter.GetComponent<CharacterSelector>().characterSelectedPrefab;
            rb.velocity = new Vector2(0, 0);
        }
        //Deselects the character
        if (Input.GetButtonDown(playerCancel) && characterSelected)
        {
            characterSelected = false;
            mouseMovementAllowed = true;
            characterSelectedPrefab = null;
            characterSelectionManager.GetComponent<CharacterSelectionManager>().playersReady--;
        }

    }

    void MouseMovement()
    {
        Vector2 movement = new Vector2(horizontalDir, verticalDir);

        rb.AddForce(movement * speed);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.gameObject);
        possibleCharacter = other.gameObject;
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log(other.gameObject);
        possibleCharacter = null;
    }
}
