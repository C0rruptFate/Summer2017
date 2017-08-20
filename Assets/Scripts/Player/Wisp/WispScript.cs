using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WispScript : MonoBehaviour
{
    [HideInInspector] //The transform location that the wisp will be flying to.
    public Transform targetLocation;

    [HideInInspector]
    public GameObject[] players;

    [HideInInspector]
    public bool moving = false; //flipped on when the wisp is moving used to help the wisp find it's target
    [Tooltip("Speed of the Wisp.")]
    public float speed; //How fast the wisp moves
    //public float attachedSpeed; 

    [HideInInspector]
    public Collider2D circleCollider;

    [Tooltip("Turn to false if you want the Wisp to startout inactive, something will need to be added to the scene to activate the Wisp.")]
    public bool wispActive = true;

    // Use this for initialization
    public void Start()
    {
        targetLocation = gameObject.transform;

        circleCollider = GetComponent<CircleCollider2D>();

        GameObject.Find("Game Manager").transform.GetComponent<GameController>().wisp = gameObject;

        players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    public void Update()
    {
        //if Moving will move to the target wisp location.
        if (moving && wispActive)
        {
            MoveToTarget();
        }

        //Turns off moving and plays the particle for idel.
        if (transform.position == targetLocation.position && moving)
        {
            moving = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wisp Stopper")
        {
            circleCollider.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Wisp Stopper")
        {
            circleCollider.enabled = false;
        }
    }

    void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetLocation.position, speed * Time.deltaTime);
        //Debug.Log("Moving To " + targetLocation.name);
    }
}
