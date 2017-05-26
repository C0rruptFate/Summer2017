﻿using System.Collections;
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

    [HideInInspector]
    public bool characterSelected = false;

    public Element element = Element.None;
    public Element possibleElement = Element.None;

    private Rigidbody2D rb;
    private GameObject characterSelectionManager;

    private float player_width;
    private float player_height;


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

        //initialize size attributes
        player_width = GetComponent<RectTransform>().localScale.x;
        player_height = GetComponent<RectTransform>().localScale.y;
    }
	
	// Update is called once per frame
	void Update () {

        horizontalDir = Input.GetAxis(horizontalMovement);
        verticalDir = Input.GetAxis(verticalMovement);

        //Enables the player
        if (Input.GetButtonDown(playerSelect) && !activePlayer)
        {
            activePlayer = true;
            Constants.playerCount++;
            mouseMovementAllowed = true;
            //Debug.Log(activePlayer);
            GetComponent<Image>().enabled = true;
        }

        if (mouseMovementAllowed)
        {
            MouseMovement();
        }

        //Selects the character
        if (Input.GetButtonDown(playerSelect) && possibleElement != Element.None)
        {
            if(characterSelected == false)
            {
                characterSelectionManager.GetComponent<CharacterSelectionManager>().playersReady++;
            }
            characterSelected = true;
            mouseMovementAllowed = false;
            element = possibleElement;
            //characterSelectedPrefab = possibleCharacter.GetComponent<CharacterSelector>().characterSelectedPrefab;
            rb.velocity = new Vector2(0, 0);
        }
        //Deselects the character
        if (Input.GetButtonDown(playerCancel) && characterSelected)
        {
            characterSelected = false;
            mouseMovementAllowed = true;
            //characterSelectedPrefab = null;
            characterSelectionManager.GetComponent<CharacterSelectionManager>().playersReady--;
        }

    }

    void MouseMovement()
    {
        Vector2 movement = new Vector2(horizontalDir, verticalDir);

        rb.AddForce(movement * speed);

        ScreenCollisions();
        //rb.position = new Vector2(Mathf.Clamp(rb.position.x, 0, maxWidth), Mathf.Clamp(rb.position.y, 0, maxHeight));
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

    public void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.gameObject);
        possibleElement = other.GetComponent<CharacterSelector>().element;
        //possibleCharacter = other.gameObject;
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log(other.gameObject);
        possibleElement = Element.None;
        //possibleCharacter = null;
    }
}
