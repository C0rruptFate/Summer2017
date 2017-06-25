using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileWaterSpecial : PlayerProjectile
{
    [HideInInspector]
    public float baseHealReduction = 0.5f;
    [HideInInspector]
    public float healMultiplierThreshold = 0.25f;
    [HideInInspector]
    public float healMultiplier = 1.5f;

    // Use this for initialization
    public override void Start () {
        //Set's my element

        Debug.Log("I'm the special ranged attack");
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

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ("Enemy"))//If this hits an enemy deals damage to them.
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            EnemyHealth health = other.gameObject.GetComponent<EnemyHealth>();
            //Rigidbody otherRB = other.gameObject.GetComponent<Rigidbody>();

            if (enemy && health)
            {
                //float distX = (other.transform.position.x - transform.position.x) * knockback;
                //float distY = (other.transform.position.y - transform.position.y) * knockback;
                //otherRB.velocity = new Vector3(0.0f, 0.0f, otherRB.velocity.z);
                //otherRB.AddForce(new Vector3(distX, distY, 0), ForceMode.Impulse);
                health.TakeDamage(gameObject, projectileDamage, projectileHitStun);
                //Heals the player for half the damage their projectile would do.
                float healAmount = projectileDamage * baseHealReduction;
                //If the player is below 25% hp then heal for 3X as much.
                if (player.GetComponent<PlayerHealth>().health <= (healMultiplierThreshold * player.GetComponent<PlayerHealth>().startingHealth))
                {
                    healAmount = projectileDamage * healMultiplier;
                    //Debug.Log("Bigger heal: " + healAmount);
                }
                else
                {
                    //Debug.Log("Small heal: " + healAmount);
                }
                player.GetComponent<PlayerHealth>().Heal(healAmount);

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
