﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileEarthSpecial : PlayerProjectile
{
    public float heavyObjectMass = 45;

    [HideInInspector]
    public float forceMagnitude;

    private float rbMass = 5;
    private float rbLDrag = 2;

    private PointEffector2D pointEffector;

    [HideInInspector]
    public bool beingPulledIn = false;

    [HideInInspector]
    public GameObject pulledInTarget;

    [HideInInspector]
    public float pullSpeed;

    // Use this for initialization
    public override void Start()
    {
        //Debug.Log("Look here");
        //Set's my element
        element = shooter.GetComponent<PlayerHealth>().element;

        //enables my collider as they start disabled.
        if (gameObject.GetComponent<Collider2D>().enabled == false)
        {
            gameObject.GetComponent<Collider2D>().enabled = true;
        }

        //Debug.Log("Look here");
        formerParent = transform.parent;
        transform.parent = shooter.transform;
        Invoke("ThrowForce", throwWaitTime);
        Invoke("ChangeMass", throwWaitTime * 3);

        currentLife = Time.time + projectileMaxDuration;//sets the max life of this object.
        float breakNumber = Random.Range(0, 100);//Used to help decide if this will break when hitting an enemy.
        if (breakNumber <= projectileBreakChance)
        {
            breaking = true;
        }
    }

    // Update is called once per frame
    public override void FixedUpdate()
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
        //Debug.Log("Threw object 2");
        rb.mass = rbMass;
        rb.drag = rbLDrag;

        //Enable pull of the object
        pointEffector = gameObject.GetComponent<PointEffector2D>();
        pointEffector.enabled = true;
        pointEffector.forceMagnitude = forceMagnitude;
    }

    void ChangeMass()
    {
        GetComponent<Rigidbody2D>().mass = heavyObjectMass;
    }
}
