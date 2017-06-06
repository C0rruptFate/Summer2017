﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour {
    [HideInInspector]
    public Element element;

    [Header("Movement and Target Acquisition")]
    [Tooltip("Select the elemental type the enemy will target.")]
    public EnemyTargetType enemyTargetType;
    [Tooltip("If reletless is turned on the enemy will not aquire new targets.")]
    public bool relentless = true;
    [Tooltip("How often the enemy will aquire new targets. Set this to 0 for a relentless enemy.")]
    public float newTargetAcquisition = 0f;
    [Tooltip("Speed the enemy moves at.")]
    public float speed = 13f;
    [Tooltip("Max speed of the enemy.")]
    public float maxMoveSpeed = 13f;

    [Header("Damage and Attacks")]
    [Tooltip("Damage dealt when colliding with the enemy.")]
    public float damage = 13f;
    [Tooltip("How long the player is locked out for.")]
    public float hitStun = 0.1f;
    [Tooltip("Damage dealt when colliding with the enemy. 'The size of what you are hitting will be important'")]
    public float knockback = 1f;
    [Tooltip("How much time must take place between swings.")]
    public float swingTimer = 0.5f;

    //private GameObject currentTarget;
    //private Animator animator;
    [HideInInspector]//My rigidbody
    public Rigidbody2D rb;
    [HideInInspector]//list of players that I can target.
    public GameObject[] targets;
    [HideInInspector]//What player I choose to target.
    public GameObject target;
    [HideInInspector]//Finds a new target when the player dies or relentless is not checked.
    public float newNextTarget;
    [HideInInspector]//decides what direction the enemy will move when roaming is set up.
    public Vector2 direction;
    [HideInInspector]//How long it has been sense the enemy last attacked, use so that the enemy can't attack every frame.
    public float newSwingTimer = 0f;
    [HideInInspector]//Enemy HP script.
    EnemyHealth enemyHealth;

    // Use this for initialization
    public virtual void Start()
    {
        //Sets up the components 
        //animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        element = gameObject.GetComponent<EnemyHealth>().element;

        TargetSelection();//Finds the target.
    }

    // Update is called once per frame
    public virtual void FixedUpdate () {

        //Moves the enemy
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxMoveSpeed, maxMoveSpeed), rb.velocity.y);

        //Finds a new target if my target dies or something happens.
        if (target == null)
        {
            if (enemyTargetType != EnemyTargetType.Roam)
            {
                TargetSelection();
            }
            else
            {
                // Roaming Enemy
                if (relentless == false && Time.time > newNextTarget)
                {
                    TargetSelection();
                    if (direction.x == 0)
                    {
                        direction = new Vector2(Random.Range(-1, 2), 0);
                    }
                }
                
                rb.AddForce(direction * speed,ForceMode2D.Force);
            } 
        }
        else
        {//Moves towards my target.
            if (enemyTargetType != EnemyTargetType.Roam)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, (speed * Time.deltaTime));
            }
            if (relentless == false && Time.time > newNextTarget)
            {
                TargetSelection();
            }
        }
    }

    public virtual void TargetSelection()
    {
        // Evaluate Targeting Type
        switch (enemyTargetType)
        {
            case EnemyTargetType.Element: //Finds the target depending on what element I counter
                targets = GameObject.FindGameObjectsWithTag("Player");
                foreach (var possibleTarget in targets)
                {
                    if (possibleTarget.GetComponent<PlayerHealth>().element == Constants.whatICounter(element) && possibleTarget.gameObject.activeSelf)
                    {
                        target = possibleTarget;
                    }
                }
                if (target == null)// if target dies or element doesn't exist change to proximity targeting.
                {
                    enemyTargetType = EnemyTargetType.Proximity;
                    TargetSelection();
                }
                break;
            case EnemyTargetType.Random://Finds a random target
                //Random Target selection
                targets = GameObject.FindGameObjectsWithTag("Player");
                target = targets[Random.Range(0, targets.Length)];
                break;
            case EnemyTargetType.Roam://Moves around randomly
                direction = new Vector2(Random.Range(-1, 2), 0);
                break;
            case EnemyTargetType.Proximity://Finds the target closest to me.
            default:
                targets = GameObject.FindGameObjectsWithTag("Player");//Finds all players tagged as "player".
                float closestTargetDist = 0.0f;
                foreach (var possibleTarget in targets)//Finds the closest target by looping though all players and seeing who is closest.
                {
                    float dist = Vector2.Distance(possibleTarget.transform.position, gameObject.transform.position);
                    if (dist < closestTargetDist)
                    {
                        closestTargetDist = dist;
                        target = possibleTarget;
                    }
                    else if (closestTargetDist == 0.0f)
                    {
                        closestTargetDist = dist;
                        target = possibleTarget;
                    }
                }
                break;
        }
        if (relentless == false)//If relentless is true they will stick on their target rather then updating to new ones.
        {
            newNextTarget = Time.time + newTargetAcquisition;
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
            }    
        }
    }
}
