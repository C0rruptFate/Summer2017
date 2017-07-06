﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour {

    public Element element;
    public float damage;
    [Tooltip("How much time must take place between swings.")]
    public float swingTimer = 0.5f;

    //How long it has been sense the enemy last attacked, use so that the enemy can't attack every frame.
    private float newSwingTimer = 0f;
    public float hitStun = 0;

    public bool lowerHazard;
    public bool raiseHazard;
    public bool rollingHazard;
    protected Vector3 startPosition;
    protected Vector3 endPosition;
    [SerializeField]
    protected float moveSpeed;
    protected Vector3 currentPos;
    protected Vector3 lastPos;
    protected bool rolling = false;

    private bool isFallingHazard = false;
    // Use this for initialization
    public virtual void Start () {
        startPosition = transform.position;
        endPosition = new Vector3(startPosition.x, startPosition.y - 0.8f, startPosition.z);

        if (transform.parent != null)
        {
            if (transform.parent.GetComponent<FallingHazard>() != null)
            {
                isFallingHazard = true;
            }
            else if (rollingHazard)
            {
                rolling = true;
            }
        }
    }
	
	// Update is called once per frame
	public virtual void Update () {
		
        if(raiseHazard)
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            if (transform.position.y >= startPosition.y)
            {
                transform.position = startPosition;
                raiseHazard = false;
                lowerHazard = false;
            }
        }
        else if (lowerHazard)
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
            if (transform.position.y <= endPosition.y)
            {
                transform.position = endPosition;
                lowerHazard = false;
            }
        }
	}

    public virtual void FixedUpdate()
    {
        if (rollingHazard)
        {
            currentPos = transform.position;
            if (currentPos == lastPos && rolling)
            {
                //Put not moving script here.
                //GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                GetComponent<PointEffector2D>().enabled = false;
                rolling = false;
            }
            else
            {
                rolling = true;
                GetComponent<PointEffector2D>().enabled = true;
            }
            lastPos = currentPos;
        }
    }

    public virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (transform.GetComponent<Rigidbody2D>() != null && other.gameObject.CompareTag("Ground") && isFallingHazard)//Makes sure we are destroyed when hitting the ground.
        {
            DestroyMyself();
        }
    }

    public virtual void OnCollisionStay2D(Collision2D other)
    {
        //Deals damage to the player as long as they are touching this enemy.
        if (other.transform.tag == ("Player") && Time.time > newSwingTimer)
        {
            //Debug.Log("Player should take damage");
            newSwingTimer = Time.time + swingTimer;
            PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
            PlayerHealth health = other.gameObject.GetComponent<PlayerHealth>();
            //PlayerAttacks playerAttacks = health.playerAttacks;
            //Rigidbody otherRB = other.gameObject.GetComponent<Rigidbody>();

            //If what I am colliding with has both a player Controller and Health script, deal damage to them and knock them back.
            if (playerMovement && health)
            {
                //float distX = (other.transform.position.x - transform.position.x) * knockback;
                //otherRB.velocity = new Vector3(0.0f, 0.0f, otherRB.velocity.z);
                //otherRB.AddForce(new Vector3(distX, otherRB.velocity.y, 0), ForceMode.Impulse);
                health.TakeDamage(gameObject, damage, hitStun);
                
                if(isFallingHazard)//Removes itself if it is a falling hazard once it hits a player.
                {
                    DestroyMyself();
                }

            }
        }
        else if (other.transform.tag == ("Enemy") && Time.time > newSwingTimer)//Lets it hurt enemies
        {
            //Debug.Log("Player should take damage");
            newSwingTimer = Time.time + swingTimer;
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            EnemyHealth health = other.gameObject.GetComponent<EnemyHealth>();
            //PlayerAttacks playerAttacks = health.playerAttacks;
            //Rigidbody otherRB = other.gameObject.GetComponent<Rigidbody>();

            //If what I am colliding with has both a player Controller and Health script, deal damage to them and knock them back.
            if (enemy && health)
            {
                //float distX = (other.transform.position.x - transform.position.x) * knockback;
                //otherRB.velocity = new Vector3(0.0f, 0.0f, otherRB.velocity.z);
                //otherRB.AddForce(new Vector3(distX, otherRB.velocity.y, 0), ForceMode.Impulse);
                health.TakeDamage(gameObject, damage, hitStun);

                if (isFallingHazard)//Removes itself if it is a falling hazard once it hits a player.
                {
                    DestroyMyself();
                }
            }
        }
    }

    public virtual void DestroyMyself()
    {
        //[TODO] Destroy effect
        Destroy(gameObject);
    }
}
