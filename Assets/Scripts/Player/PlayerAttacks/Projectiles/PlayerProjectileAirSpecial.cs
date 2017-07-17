using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileAirSpecial : PlayerProjectile
{
    [Tooltip("Attach the shockOrb object here.")]
    public GameObject shockOrb;

    [HideInInspector]
    public float shockOrbRadius;

    //[HideInInspector]
    //public float specialProjectileSpeed;

    public override void Start()
    {
        //shockOrbRadius = player.GetComponent<PlayerAttacks>().
        //Set's my element
        element = shooter.GetComponent<PlayerHealth>().element;

        //enables my collider as they start disabled.
        if (gameObject.GetComponent<Collider2D>().enabled == false)
        {
            gameObject.GetComponent<Collider2D>().enabled = true;
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

    public override void OnTriggerEnter2D(Collider2D other)
    {
        //if this hits an enemy spawns a "Shock orb" at that location, the shock orb has a med size and damages all enemies inside of the blast.
        if (hurtsPlayers == false)
        {
            if (other.tag == ("Enemy"))//If this hits an enemy deals damage to them.
            {
                GameObject myShockOrb = Instantiate(shockOrb, transform.position, transform.rotation);
                myShockOrb.GetComponent<ShockOrbScript>().shooter = shooter;
                Destroy(gameObject);
                //myShockOrb.GetComponent<ShockOrbScript>().element = myElement;
                //myShockOrb.GetComponent<ShockOrbScript>().projectileDamage = projectileDamage;
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
        }

        if (other.tag == ("Ground") && breaksHittingWall) //Gets destroyed when hitting the ground/walls
        {
            Destroy(gameObject);
        }
    }
}
