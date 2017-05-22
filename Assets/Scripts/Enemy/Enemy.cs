using System.Collections;
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
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public GameObject[] targets;
    [HideInInspector]public GameObject target;
    [HideInInspector]
    public float newNextTarget;
    [HideInInspector]
    public Vector2 direction;
    [HideInInspector]
    public float newSwingTimer = 0f;
    [HideInInspector]
    EnemyHealth enemyHealth;

    // Use this for initialization
    public virtual void Start()
    {
        //animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        element = gameObject.GetComponent<EnemyHealth>().element;

        TargetSelection();
    }

    // Update is called once per frame
    public virtual void FixedUpdate () {

        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxMoveSpeed, maxMoveSpeed), rb.velocity.y);


        if (target == null)
        {
            if (enemyTargetType != EnemyTargetType.Roam)
            {
                TargetSelection();
            }
            else
            {
                // Roaming Enemy [TODO] figure out what the goal is of the relentless flag here.
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
        {
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
            case EnemyTargetType.Element:
                targets = GameObject.FindGameObjectsWithTag("Player");
                foreach (var possibleTarget in targets)
                {
                    if (possibleTarget.GetComponent<PlayerHealth>().element == Constants.whatICounter(element))
                    {
                        target = possibleTarget;
                    }
                }
                if (target == null)
                {
                    enemyTargetType = EnemyTargetType.Proximity;
                    TargetSelection();
                }
                break;
            case EnemyTargetType.Random:
                //Random Target selection
                targets = GameObject.FindGameObjectsWithTag("Player");
                target = targets[Random.Range(0, targets.Length)];
                break;
            case EnemyTargetType.Roam:
                direction = new Vector2(Random.Range(-1, 2), 0);
                break;
            case EnemyTargetType.Proximity:
            default:
                targets = GameObject.FindGameObjectsWithTag("Player");
                float closestTargetDist = 0.0f;
                foreach (var possibleTarget in targets)
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
        if (relentless == false)
        {
            newNextTarget = Time.time + newTargetAcquisition;
        }
    }

    public virtual void OnCollisionStay2D(Collision2D other)
    {
        if (other.transform.tag == ("Player") && Time.time > newSwingTimer)
        {
            Debug.Log("Player should take damage");
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
