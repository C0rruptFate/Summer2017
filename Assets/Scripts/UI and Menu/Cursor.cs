using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cursor : MonoBehaviour {

    //Find Cursor and who is belongs to.
    [Tooltip("The player # for this cursor.")]
    public int playerNumber = 1;
    [Tooltip("How fast the corsor moves.")]
    public float speed;

    [Tooltip("effect that starts to play when a player has locked in their character.")]
    public GameObject lockedInEffect;
    private GameObject ImLockedInEffect;//My copy of the locked in effect so that I can destroy it when I unselect a character.

    [HideInInspector]//this cursor becomes active when the player pushes A (might add other buttons too) to activate themselves.
    public bool activePlayer = false;
    [HideInInspector]//Button used to select their character.
    public string playerSelect;
    [HideInInspector]//Button used to deselect their chracter.
    public string playerCancel;

    [HideInInspector]//sets up the controls for the joystick of each player.
    public string horizontalMovement;
    [HideInInspector]//Sets up the controls for the joystick of each player.
    public string verticalMovement;
    [HideInInspector]//This becomes true when the player is active, and turned off again when they select a character.
    public bool mouseMovementAllowed = false;

    private float horizontalDir;//Used to set up the joystick movement.
    private float verticalDir;//Used to set up the joystick movement.

    [HideInInspector]//becomes true when I have selected a character.
    public bool characterSelected = false;

    [HideInInspector]//Element I have when it is locked in.
    public Element element = Element.None;
    [HideInInspector]//element of the character tile I am currently touching.
    public Element possibleElement = Element.None;

    private Rigidbody2D rb;//my rigidbody.
    private GameObject characterSelectionManager;//The manager that controls what characters each player has.

    private float player_width;//Used to make sure that the cursor doesn't leave the screen.
    private float player_height;//Used to make sure the cursor doesn't leave the screen.

    //Used to make it so you can't pick the same character as someone else.
    private GameObject currentlySelected;


    // Use this for initialization
    void Start () {

        //Sets up the controls for each character.
        horizontalMovement = "Horizontal" + playerNumber;
        verticalMovement = "Vertical" + playerNumber;
        playerSelect = "Jump" + playerNumber;
        playerCancel = "Ranged" + playerNumber;
        gameObject.name = "Player " + playerNumber + " Cursor";

        //Sets up the components for each character.
        rb = GetComponent<Rigidbody2D>();
        GetComponent<Image>().enabled = false;
        characterSelectionManager = GameObject.Find("Character Selection Manager");

        //initialize size attributes
        player_width = GetComponent<RectTransform>().localScale.x;
        player_height = GetComponent<RectTransform>().localScale.y;
    }
	
	// Update is called once per frame
	void Update () {

        //Used to control the movement of the cursor.
        horizontalDir = Input.GetAxis(horizontalMovement);
        verticalDir = Input.GetAxis(verticalMovement);

        //Enables the player's cursor increases player count and sets them active.
        if (Input.GetButtonDown(playerSelect) && !activePlayer)
        {
            activePlayer = true;
            Constants.playerCount++;
            mouseMovementAllowed = true;
            //Debug.Log(activePlayer);
            GetComponent<Image>().enabled = true;
        }

        if (mouseMovementAllowed)//Allows cursor movement, this disables when a character is selected.
        {
            MouseMovement();
        }

        //Selects the character, disables cursor movement, sets element to the element of what I am touching.
        if (Input.GetButtonDown(playerSelect) && possibleElement != Element.None && !currentlySelected.GetComponent<CharacterSelector>().alreadySelected)
        {
            if (characterSelected == false)
            {
                characterSelectionManager.GetComponent<CharacterSelectionManager>().playersReady++;
            }
            characterSelected = true;
            mouseMovementAllowed = false;
            element = possibleElement;
            //characterSelectedPrefab = possibleCharacter.GetComponent<CharacterSelector>().characterSelectedPrefab;
            rb.velocity = new Vector2(0, 0);
            currentlySelected.GetComponent<CharacterSelector>().alreadySelected = true;
            ImLockedInEffect = Instantiate(lockedInEffect, transform.position, lockedInEffect.transform.rotation);
        }
        //Deselects the character and allows them to select a new character.
        if (Input.GetButtonDown(playerCancel) && characterSelected)
        {
            characterSelected = false;
            mouseMovementAllowed = true;
            //characterSelectedPrefab = null;
            characterSelectionManager.GetComponent<CharacterSelectionManager>().playersReady--;
            currentlySelected.GetComponent<CharacterSelector>().alreadySelected = false;
            Destroy(ImLockedInEffect);
        }

    }

    //Controls how fast and where a cursor is moving to when active.
    void MouseMovement()
    {
        Vector2 movement = new Vector2(horizontalDir, verticalDir);

        rb.AddForce(movement * speed);

        ScreenCollisions();
        //rb.position = new Vector2(Mathf.Clamp(rb.position.x, 0, maxWidth), Mathf.Clamp(rb.position.y, 0, maxHeight));
    }

    //Makes sure the cursor can't move off screen.
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

    //Sets the possible element so that the player can lock it in.
    public void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.gameObject);
        possibleElement = other.GetComponent<CharacterSelector>().element;
        currentlySelected = other.gameObject;
        //possibleCharacter = other.gameObject;
    }

    //removes the possible element when the cursor is no longer touching the selection tile.
    public void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log(other.gameObject);
        possibleElement = Element.None;
        currentlySelected = null;
        //possibleCharacter = null;
    }
}
