using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileFireSpecial : PlayerProjectile
{
    [Tooltip("Drag in the explosion object.")]
    public GameObject explosionObject;

    // Use this for initialization
    public void Start()
    {

        //Set's my element
        myElement = player.GetComponent<PlayerHealth>().element;

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

    public override void OnTriggerEnter2D(Collider2D other)
    {
        //Creates the explosion then distroys itself.


        //Should be removed.
        if (other.tag == ("Enemy") || other.tag == ("Ground"))//If this hits an enemy deals damage to them.
        {
            GameObject explosion = Instantiate(explosionObject, transform.position, transform.rotation);
            explosion.GetComponent<FireProjectileExplosion>().player = player;
            Destroy(gameObject);

        }
    }
}