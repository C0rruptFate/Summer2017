using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectiles
{

    public override void Start()
    {
        element = shooter.GetComponent<PlayerHealth>().element;
        base.Start();
    }

    public override void OnTriggerEnter2D(Collider2D other)
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
        if (hurtsPlayers == true)
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
    }

    protected override void ThrowForce()
    {
        //Debug.Log("Threw object");
        //Throws the lobbed projectile.
        gameObject.AddComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
        if (shooter.GetComponent<Transform>().rotation.y != 0)//If the player is facing left throw to the left.
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
