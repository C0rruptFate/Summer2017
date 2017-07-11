using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wisp : MonoBehaviour {

    [HideInInspector] //The transform location that the wisp will be flying to.
    public Transform targetLocation;
    private ParticleSystem ps;// the wisps particle system used to show active/inactive

    [HideInInspector]
    public bool moving = false; //flipped on when the wisp is moving used to help the wisp find it's target
    [Tooltip("Speed of the Wisp.")]
    public float speed; //How fast the wisp moves
    //public float attachedSpeed; 

    [HideInInspector]
    public Collider2D circleCollider;

    // Use this for initialization
    void Start () {
        targetLocation = gameObject.transform;
        ps = gameObject.GetComponent<ParticleSystem>();

        circleCollider = GetComponent<CircleCollider2D>();

        GameObject.Find("Game Manager").transform.GetComponent<GameController>().wisp = gameObject;

	}
	
	// Update is called once per frame
	void Update () {
        //Used to change the Wisps particles.
        var shape = ps.shape;

        //if Moving will move to the target wisp location.
        if(moving)
        {
            //shape.arcMode = ParticleSystemShapeMultiModeValue.Random;
            MoveToTarget();
        }

        //Turns off moving and plays the particle for idel.
        if (transform.position == targetLocation.position && moving)
        {
            moving = false;
            //Causes the particles to move clockwise
            //shape.arcMode = ParticleSystemShapeMultiModeValue.Loop;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Wisp Stopper")
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
