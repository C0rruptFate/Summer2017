using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class LevelSelectCursor : MonoBehaviour {

    public float speed;

    private Player input_manager1;
    private Player input_manager2;
    private Player input_manager3;
    private Player input_manager4;
    private Rigidbody2D rb;//my rigidbody.
    private float horizontalDir1;//Used to set up the joystick movement.
    private float verticalDir1;//Used to set up the joystick movement.
    private float horizontalDir2;//Used to set up the joystick movement.
    private float verticalDir2;//Used to set up the joystick movement.
    private float horizontalDir3;//Used to set up the joystick movement.
    private float verticalDir3;//Used to set up the joystick movement.
    private float horizontalDir4;//Used to set up the joystick movement.
    private float verticalDir4;//Used to set up the joystick movement.

    private float player_width;//Used to make sure that the cursor doesn't leave the screen.
    private float player_height;//Used to make sure the cursor doesn't leave the screen.

    // Use this for initialization
    void Start () {
        input_manager1 = ReInput.players.GetPlayer(0);
        input_manager2 = ReInput.players.GetPlayer(1);
        input_manager3 = ReInput.players.GetPlayer(2);
        input_manager4 = ReInput.players.GetPlayer(3);
        rb = GetComponent<Rigidbody2D>();

        player_width = GetComponent<Transform>().localScale.x;
        player_height = GetComponent<Transform>().localScale.y;
    }
	
	// Update is called once per frame
	void Update () {
        horizontalDir1 = input_manager1.GetAxis("move_horizontal");
        verticalDir1 = input_manager1.GetAxis("move_vertical");
        horizontalDir2 = input_manager2.GetAxis("move_horizontal");
        verticalDir2 = input_manager2.GetAxis("move_vertical");
        horizontalDir3 = input_manager3.GetAxis("move_horizontal");
        verticalDir3 = input_manager3.GetAxis("move_vertical");
        horizontalDir4 = input_manager4.GetAxis("move_horizontal");
        verticalDir4 = input_manager4.GetAxis("move_vertical");

        MouseMovement1();
        MouseMovement2();
        MouseMovement3();
        MouseMovement4();
    }

    void MouseMovement1()
    {
        Vector2 movement = new Vector2(horizontalDir1, verticalDir1);

        rb.AddForce(movement * speed);

        //ScreenCollisions();
        //rb.position = new Vector2(Mathf.Clamp(rb.position.x, 0, maxWidth), Mathf.Clamp(rb.position.y, 0, maxHeight));
    }
    void MouseMovement2()
    {
        Vector2 movement = new Vector2(horizontalDir2, verticalDir2);

        rb.AddForce(movement * speed);

        ScreenCollisions();
        //rb.position = new Vector2(Mathf.Clamp(rb.position.x, 0, maxWidth), Mathf.Clamp(rb.position.y, 0, maxHeight));
    }
    void MouseMovement3()
    {
        Vector2 movement = new Vector2(horizontalDir3, verticalDir3);

        rb.AddForce(movement * speed);

        //ScreenCollisions();
        //rb.position = new Vector2(Mathf.Clamp(rb.position.x, 0, maxWidth), Mathf.Clamp(rb.position.y, 0, maxHeight));
    }
    void MouseMovement4()
    {
        Vector2 movement = new Vector2(horizontalDir4, verticalDir4);

        rb.AddForce(movement * speed);

        //ScreenCollisions();
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
}
