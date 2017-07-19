using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileAirBasic : PlayerProjectile {

    [HideInInspector]//When true causes the object to return to the player.
    public bool returnToPlayer = false;
    [HideInInspector]
    public int wallHitCount = 3;

    // Use this for initialization
    public override void Start()
    {

        //Set's my element
        element = shooter.GetComponent<PlayerHealth>().element;

        //enables my collider as they start disabled.
        if (gameObject.GetComponent<Collider2D>().enabled == false)
        {
            gameObject.GetComponent<Collider2D>().enabled = true;
        }
        
        //Used to set up lobbed projectiles.
        if (!usesConstantForceProjectile && GetComponent<Rigidbody2D>() == null)
        {
            formerParent = transform.parent;
            transform.parent = shooter.transform;
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
    public override void FixedUpdate()
    {
        //Debug.Log("Current Life: " + currentLife);
        //Reduces the life of the object at 0 it is destroyed.
        if (Time.time >= currentLife)
        {
            //uncomment to distroy the projectile at it's max duration.
            //Destroy(gameObject);
            //returns to player
            //Debug.Log("Returning to player Time: " + Time.time + "Current Life " + currentLife);
            returnToPlayer = true;
        }

        //Causes the object to fly forward.
        if (returnToPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, shooter.transform.position, projectileSpeed * Time.deltaTime);
        }
        else if (usesConstantForceProjectile)
        {
            transform.Translate(Vector2.right * projectileSpeed * Time.deltaTime);
        }

        //Destroyes the projectile if the player is dead and it is returning to them.
        if (returnToPlayer && shooter == null)
        {
            Destroy(gameObject);
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (hurtsPlayers == false)
        {
            if (other.tag == ("Enemy"))//If this hits an enemy deals damage to them.
            {
                Enemy enemy = other.gameObject.GetComponent<Enemy>();
                EnemyHealth health = other.gameObject.GetComponent<EnemyHealth>();

                if (enemy && health)
                {
                    float newProjectileDamage = projectileDamage;

                    if (other.transform.Find("Air Effect"))
                    {
                        newProjectileDamage = projectileDamage * 2;
                    }
                    //Debug.Log("Damage: " + newProjectileDamage);
                    health.TakeDamage(gameObject, newProjectileDamage, projectileHitStun);
                    //If this is true it will destroy itself after hitting a single enemy false lets it hit several enemies.
                    if (breaking)
                    {
                        Destroy(gameObject);
                    }
                }
            }
            else if (other.tag == ("Ground") && breaksHittingWall) //Gets destroyed when hitting the ground/walls
            {
                wallHitCount--;
                if (wallHitCount <= 0)
                {
                    Destroy(gameObject);
                }
                else
                {
                    currentLife = Time.time;
                }
            }
            else if (returnToPlayer && other.gameObject == shooter)
            {
                //Debug.Log("Touched player");
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
                //PlayerAttacks playerAttacks = health.playerAttacks;
                //Rigidbody otherRB = other.gameObject.GetComponent<Rigidbody>();

                //If what I am colliding with has both a player Controller and Health script, deal damage to them and knock them back.
                if (playerMovement && health)
                {
                    //float distX = (other.transform.position.x - transform.position.x) * knockback;
                    //otherRB.velocity = new Vector3(0.0f, 0.0f, otherRB.velocity.z);
                    //otherRB.AddForce(new Vector3(distX, otherRB.velocity.y, 0), ForceMode.Impulse);
                    health.TakeDamage(gameObject, projectileDamage, projectileHitStun);
                }
            }
            else if (other.tag == ("Ground") && breaksHittingWall) //Gets destroyed when hitting the ground/walls
            {
                Destroy(gameObject);
            }
            else if (returnToPlayer && other.gameObject == shooter)
            {
                //Debug.Log("Touched player");
                Destroy(gameObject);
            }
        }

    }
}
