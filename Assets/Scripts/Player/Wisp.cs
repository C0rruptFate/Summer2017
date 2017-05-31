using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wisp : MonoBehaviour {

    [HideInInspector] //The transform location that the wisp will be flying to.
    public Transform targetLocation;
    private ParticleSystem ps;
    public ParticleSystemShapeMultiModeValue arcMode = ParticleSystemShapeMultiModeValue.Loop;

    [HideInInspector]
    public bool moving = false;
    public float speed;

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

        if(transform.position == targetLocation.position && moving)
        {
            moving = false;
            //Causes the particles to move clockwise
            shape.arcMode = ParticleSystemShapeMultiModeValue.Loop;
        }

	}

    void OnTriggerEnter2D(Collider2D other)
    {

    }

    void MoveToTarget()
    {
        //Moves to the target transform over time.
        transform.position = Vector2.MoveTowards(transform.position, targetLocation.position, speed * Time.deltaTime);
    }

    void AttackToPlayer()
    {
        //If the Wisp is touching a player and the player calls the wisp attach and follow the player rather than the targetLocation.
    }

}
