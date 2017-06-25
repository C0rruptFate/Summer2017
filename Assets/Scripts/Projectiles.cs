using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour {
    //These are all set by the players attacks/actions script.
    [HideInInspector]//How fast the projectile moves.
    public float projectileSpeed;
    [HideInInspector]//How much damage the projectile deals.
    public float projectileDamage;
    [HideInInspector]//How long the projectile stuns for.
    public float projectileHitStun;

    [HideInInspector]//the projectile will expire after X seconds.
    public float projectileMaxDuration;
    [HideInInspector]//The chance it will break when hitting an enemy.
    public float projectileBreakChance;
    [HideInInspector]//Does this fly strait or is is lobbed? True to fly strait.
    public bool usesConstantForceProjectile = true;
    [HideInInspector]//Does this expire when hitting a wall?
    public bool breaksHittingWall = true;
    [HideInInspector]//The force this is lobbed at when the player is using a lobbed projectile.
    public Vector2 lobbedForce;
    [HideInInspector]//How long this is held for before being thrown.
    public float throwWaitTime;
    protected Transform formerParent;//Used when holding a lobbed projectile.

    [HideInInspector]
    public float currentLife;//How long this has been alive for.
    protected bool breaking = false;//used with the break change to decide if this will break.

    [HideInInspector]
    public GameObject player; //Who this belongs to.
    [HideInInspector]//What element is this projectile.
    public Element myElement;

    protected Rigidbody2D rb; //My Rigidibody

    [HideInInspector]//Make this false to hurt enemies and true to hurt players.
    public bool hurtsPlayers = false;
    [HideInInspector]
    public Transform reflectedPoint;
    // Start and fixed update are blocked out atm

    public virtual void Start()
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
    public virtual void FixedUpdate()
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
        else
        {//Reflects the projectile.
            transform.position = Vector3.MoveTowards(transform.position, reflectedPoint.position, -projectileSpeed * Time.deltaTime);
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (hurtsPlayers == false)
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
                    if (breaking)
                    {
                        Destroy(gameObject);
                    }
                }
            }
            else if (other.tag == ("Ground") && breaksHittingWall) //Gets destroyed when hitting the ground/walls
            {
                Destroy(gameObject);
            }
        }
        else if (hurtsPlayers == true)
        {
            if (other.transform.tag == ("Player"))
            {
                //Debug.Log("Player should take damage");
                PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
                PlayerHealth health = other.gameObject.GetComponent<PlayerHealth>();

                //If what I am colliding with has both a player Controller and Health script, deal damage to them and knock them back.
                if (playerMovement && health)
                {
                    health.TakeDamage(gameObject, projectileDamage, projectileHitStun);
                }
            }
            else if (other.tag == ("Ground") && breaksHittingWall) //Gets destroyed when hitting the ground/walls
            {
                Destroy(gameObject);
            }
        }
    }

    protected virtual void ThrowForce()
    {
        //Debug.Log("Threw object");
        //Throws the lobbed projectile.
        gameObject.AddComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
        if (player.GetComponent<Transform>().rotation.y != 0)//If the player is facing left throw to the left.
        {
            rb.AddForce(new Vector2(-lobbedForce.x, lobbedForce.y), ForceMode2D.Impulse);
        }
        else//if facing right throw to the right.
        {
            rb.AddForce(lobbedForce, ForceMode2D.Impulse);
        }
        transform.parent = formerParent;

    }

}

