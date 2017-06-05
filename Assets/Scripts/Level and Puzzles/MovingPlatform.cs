using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    [Tooltip("How fast does this platform move?")]
    public float speed = 5;
    [Tooltip("Check if you want the platform to go vertically.")]
    public bool moveVertical = false;
    [Tooltip("Check if you want the platform to start going up first. This will not be used if the platform is going left-right.")]
    public bool moveUp = true;
    [Tooltip("Check if you want the platform to start going to the right first. This will not be used if the platform is going up-down.")]
    public bool moveRight = true;

    private Transform thisTransform;//Sets our own transform

    private Vector3 startPosition;//Where did we start.
    private Vector3 movingHorizontalVector;//used to decide if I am going left or right first.
    private Vector3 movingVerticalVector;//Used if I am going up or down first.

    private float velocity;//Used by moving platform function that I don't really understand.

    // Use this for initialization
    void Start () {
        thisTransform = transform;//Sets our transform to our own transform.
        startPosition = thisTransform.position;//Sets our start position to the position the object was playced at.

        if (moveRight)
        {
            movingHorizontalVector = Vector3.right;//Causes the platform to move to the right first.
        }
        else
        {
            movingHorizontalVector = Vector3.left;//Causes the platform to move to the left first.
        }

        if (moveUp)
        {
            movingVerticalVector = Vector3.up;//Causes the platform to move up first.
        }
        else
        {
            movingVerticalVector = Vector3.down;//Causes the platform to move down first.
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!moveVertical)//Moving horizontally
        {
            Vector3 pos = startPosition + movingHorizontalVector * Mathf.Sin(Time.time) * speed;
            velocity -= 14 * Time.deltaTime;

            if (velocity < 0)
                velocity = 0;

            pos.y = velocity + startPosition.y;
            thisTransform.position = pos; 
        }else
        {//Used to move up and down.
            Vector3 pos = startPosition + movingVerticalVector * Mathf.Sin(Time.time) * speed;
            velocity -= 14 * Time.deltaTime;

            if (velocity < 0)
                velocity = 0;

            pos.x = velocity + startPosition.x;
            thisTransform.position = pos;
        }
    }

    void OnCollisionEnter2D(Collision2D other)//Causes the player, enemy, or projectile to move with the moving platform.
    {
        if (other.transform.tag == ("Player") || other.transform.tag == ("Enemy") || other.transform.tag == ("Projectile"))
        {
            other.transform.parent = gameObject.transform;
        }
    }

    void OnCollisionExit2D(Collision2D other)//Removes the player, enemy, or projectile so it can move independently of the platform.
    {
        if (other.transform.tag == ("Player") || other.transform.tag == ("Enemy") || other.transform.tag == ("Projectile"))
        {
            other.transform.parent = null;
        }
    }
}
