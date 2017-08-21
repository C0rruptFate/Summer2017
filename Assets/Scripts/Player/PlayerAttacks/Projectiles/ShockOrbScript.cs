using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockOrbScript : PlayerProjectile {

    public GameObject shockOrbEffect;

    //[HideInInspector]
    //public GameObject player;
    //[HideInInspector]
    //public Element element;
    [HideInInspector]
    public float shockOrbRadius;

    // Use this for initialization
    public override void Start () {
        
        element = shooter.GetComponent<PlayerHealth>().element;

        if (GetComponent<CircleCollider2D>().enabled == false)
        {
            GetComponent<CircleCollider2D>().enabled = true;
            
        }
        currentLife = Time.time + shooter.GetComponent<PlayerAttacks>().specialRangedHitStun;
        GetComponent<CircleCollider2D>().radius = shooter.GetComponent<AttacksAir>().shockOrbRadius;
        //Destroys self after damaging all targets.
        Invoke("ShockOrbVanish", shooter.GetComponent<PlayerAttacks>().specialRangedHitStun);
    }

    // Update is called once per frame
    void Update () {
        
	}

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" && other.GetComponent<EnemyHealth>())
        {
            //Debug.Log("I hit: " + other.name);
            GameObject miniShockOrb = Instantiate(shockOrbEffect, other.transform.position, other.transform.rotation);
            miniShockOrb.transform.parent = gameObject.transform;
            //Debug.Log("Player: " + player + "damage: " + player.GetComponent<PlayerAttacks>().specialRangedDamage + "HitStun: " + player.GetComponent<PlayerAttacks>().specialRangedHitStun);
            float newProjectileDamage = shooter.GetComponent<PlayerAttacks>().specialRangedDamage;

            if (other.transform.Find("Air Effect"))
            {
                newProjectileDamage = shooter.GetComponent<PlayerAttacks>().specialRangedDamage * 2;
            }
            //Debug.Log("Damage: " + newProjectileDamage);
            other.GetComponent<EnemyHealth>().TakeDamage(gameObject, newProjectileDamage, shooter.GetComponent<PlayerAttacks>().specialRangedHitStun);
            
        }
    }

    void ShockOrbVanish()
    {
        Debug.Log("shock orb vanishing");
        Destroy(gameObject);
    }
}
