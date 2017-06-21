using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileEarthBasic : PlayerProjectile {

    [HideInInspector]
    public bool beingPulledIn = false;

    [HideInInspector]
    public GameObject pulledInTarget;
    [HideInInspector]
    public float pullSpeed;
    [HideInInspector]
    public bool iveHitSomething;

    [HideInInspector]
    public float currentHitCount;
    public float maxHitCount = 2;

    // Use this for initialization
    public void Start()
    {
        Debug.Log("I am born!");
        //Set's my element
        myElement = player.GetComponent<PlayerHealth>().element;

        //enables my collider as they start disabled.
        if (gameObject.GetComponent<Collider2D>().enabled == false)
        {
            gameObject.GetComponent<Collider2D>().enabled = true;
        }

        //Used to set up lobbed projectiles.
        if (!usesConstantForceProjectile && GetComponent<Rigidbody2D>() == null)
        {
            formerParent = transform.parent;
            transform.parent = player.transform;
            Invoke("ThrowForce", throwWaitTime);
        }

        currentLife = Time.time + projectileMaxDuration;//sets the max life of this object.
        float breakNumber = Random.Range(0, 100);//Used to help decide if this will break when hitting an enemy.
        if (breakNumber <= projectileBreakChance)
        {
            breaking = true;
        }
    }

    // Update is called once per frame
    public void FixedUpdate()
    {

        //Reduces the life of the object at 0 it is destroyed.
        if (Time.time >= currentLife)
        {
            Destroy(gameObject);
        }

        //Causes the object to fly forward.
        if (usesConstantForceProjectile)
        {
            transform.Translate(Vector2.right * projectileSpeed * Time.deltaTime);
        }

        if(beingPulledIn && iveHitSomething == false)
        {
            //Debug.Log("Being Pulled in");
            if (pulledInTarget == null)
            {
                rb.gravityScale = 1;
                beingPulledIn = false;
            }
            else
            {
                transform.position = (Vector2.MoveTowards(transform.position, pulledInTarget.transform.position, pullSpeed * Time.deltaTime));
            }
        }
        //else
        //{
        //    if (pulledInTarget == null)
        //    {
        //        rb.gravityScale = 1;
        //        beingPulledIn = false;
        //    }
        //}
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ("Enemy"))//If this hits an enemy deals damage to them.
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            EnemyHealth health = other.gameObject.GetComponent<EnemyHealth>();
            //Rigidbody otherRB = other.gameObject.GetComponent<Rigidbody>();

            if (enemy && health)
            {
                //Debug.Log("Hit enemy");
                //float distX = (other.transform.position.x - transform.position.x) * knockback;
                //float distY = (other.transform.position.y - transform.position.y) * knockback;
                //otherRB.velocity = new Vector3(0.0f, 0.0f, otherRB.velocity.z);
                //otherRB.AddForce(new Vector3(distX, distY, 0), ForceMode.Impulse);
                health.TakeDamage(gameObject, projectileDamage, projectileHitStun);
                //If this is true it will destroy itself after hitting a single enemy false lets it hit several enemies.

                if (beingPulledIn && iveHitSomething == false)
                {
                    iveHitSomething = true;
                    rb.gravityScale = 1;
                    beingPulledIn = false;
                }

                currentHitCount++;
                Debug.Log("current Hit Count: " + currentHitCount);
                if (breaking || currentHitCount == maxHitCount)
                {
                    Debug.Log("current Hit Count when dieing: " + currentHitCount);
                    Destroy(gameObject);
                }
            }
        }
        else if (other.tag == ("Ground") && breaksHittingWall) //Gets destroyed when hitting the ground/walls
        {
            Destroy(gameObject);
        }
    }

    protected override void ThrowForce()
    {
        base.ThrowForce();
        BoxCollider2D triggerCollider = gameObject.GetComponent<BoxCollider2D>();
        triggerCollider.size = new Vector2(5f, 3f); //Change these numbers to make the hitbox larger or smaller if needed so that it matches what is below.
        //Adds physical coollider so that he can stay on the ground.
        BoxCollider2D physicalCollider = gameObject.AddComponent<BoxCollider2D>();
        physicalCollider.size = new Vector2(5f, 3f);//Change these numbers to make the hitbox larger or smaller if needed so that it will collide with the ground.
    }
}
