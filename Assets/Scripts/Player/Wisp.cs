using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wisp : MonoBehaviour {

    [HideInInspector] //The transform location that the wisp will be flying to.
    public Transform targetLocation;
    private ParticleSystem ps;
    private ParticleSystemShapeMultiModeValue arcMode = ParticleSystemShapeMultiModeValue.Loop;
    private float wispNextCall = 0.0f;

    //[HideInInspector]
    public bool moving = false;
    public float speed;
    public float attachedSpeed;
    [Tooltip("How much time must take place between picking up and releasing the wisp")]
    public float wispCallTime = 1f;

    private bool attachedToPlayer = false;

    // Use this for initialization
    void Start () {
        targetLocation = gameObject.transform;
        ps = gameObject.GetComponent<ParticleSystem>();

	}
	
	// Update is called once per frame
	void Update () {
        var shape = ps.shape;
        if(moving)
        {
            shape.arcMode = ParticleSystemShapeMultiModeValue.Random;
            MoveToTarget();
        }

        if (transform.position == targetLocation.position && moving && !attachedToPlayer)
        {
            moving = false;
            //Causes the particles to move clockwise
            shape.arcMode = ParticleSystemShapeMultiModeValue.Loop;
        }

	}

    void OnTriggerStay2D(Collider2D other)
    {
        //Used for attaching to a player 
        if (other.tag == "Player" && other.GetComponent<PlayerAttacks>().callingWisp && Time.time > wispNextCall)
        {
            wispNextCall = Time.time + wispCallTime;
            CatchOrRelease(other.gameObject);
        }
    }

    void MoveToTarget()
    {
        //Moves to the target transform over time.
        //if (gameObject.transform.parent != null)
        //{
        //    gameObject.transform.parent = null;
        //}
        transform.position = Vector2.MoveTowards(transform.position, targetLocation.position, speed * Time.deltaTime);
    }

    void CatchOrRelease(GameObject other)
    {
        // Wisp becomes child of player who picked it up until it receives a new command.
        gameObject.transform.parent = gameObject.transform.parent == null ? other.transform : null;
    }

}
