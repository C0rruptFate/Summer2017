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

    public void Start()
    {
        //shockOrbRadius = player.GetComponent<PlayerAttacks>().
        //Set's my element
        myElement = player.GetComponent<PlayerHealth>().element;

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

    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        //if this hits an enemy spawns a "Shock orb" at that location, the shock orb has a med size and damages all enemies inside of the blast.
        if (other.tag == ("Enemy"))//If this hits an enemy deals damage to them.
        {
            GameObject myShockOrb = Instantiate(shockOrb, transform.position, transform.rotation);
            myShockOrb.GetComponent<ShockOrbScript>().player = player;
            Destroy(gameObject);
            //myShockOrb.GetComponent<ShockOrbScript>().element = myElement;
            //myShockOrb.GetComponent<ShockOrbScript>().projectileDamage = projectileDamage;
        }
        else if (other.tag == ("Ground") && breaksHittingWall) //Gets destroyed when hitting the ground/walls
        {
            Destroy(gameObject);
        }
    }
}
