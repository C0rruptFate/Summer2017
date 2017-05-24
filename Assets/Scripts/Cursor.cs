using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour {

    public int playerNumber = 1;
    public float speed;

    //[SerializeField]
    //private float runForce;
    [HideInInspector]
    public string horizontalMovement;
    [HideInInspector]
    public string verticalMovement;
    //[SerializeField]
    //private float decelerationForce;

    private float horizontalDir;
    private float verticalDir;

    private Rigidbody2D rb;

    // Use this for initialization
    void Start () {
        horizontalMovement = "Horizontal" + playerNumber;
        verticalMovement = "Vertical" + playerNumber;
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        horizontalDir = Input.GetAxis(horizontalMovement);
        verticalDir = Input.GetAxis(verticalMovement);

        MouseMovement();
    }

    void MouseMovement()
    {
        Vector2 movement = new Vector2(horizontalDir, verticalDir);

        rb.AddForce(movement * speed);
    }
}
