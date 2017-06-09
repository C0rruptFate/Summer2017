using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileEarthBasic : PlayerProjectile {

    // Use this for initialization
    public void Start()
    {

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
