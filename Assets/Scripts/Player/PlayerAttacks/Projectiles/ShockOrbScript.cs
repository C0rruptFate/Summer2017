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
    void Start () {

        Debug.Log("shock orb Spawned");
        myElement = player.GetComponent<PlayerHealth>().element;

        if (GetComponent<CircleCollider2D>().enabled == false)
        {
            GetComponent<CircleCollider2D>().enabled = true;
            
        }
        GetComponent<CircleCollider2D>().radius = player.GetComponent<AttacksAir>().shockOrbRadius;
        //Destroys self after damaging all targets.
        Invoke("ShockOrbVanish", player.GetComponent<PlayerAttacks>().specialRangedHitStun);
    }

    // Update is called once per frame
    void Update () {
        
	}

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" && other.GetComponent<EnemyHealth>())
        {
            GameObject miniShockOrb = Instantiate(shockOrbEffect, other.transform.position, other.transform.rotation);
            miniShockOrb.transform.parent = gameObject.transform;
            Debug.Log("Player: " + player + "damage: " + player.GetComponent<PlayerAttacks>().specialRangedDamage + "HitStun: " + player.GetComponent<PlayerAttacks>().specialRangedHitStun);
            other.GetComponent<EnemyHealth>().TakeDamage(gameObject, player.GetComponent<PlayerAttacks>().specialRangedDamage, player.GetComponent<PlayerAttacks>().specialRangedHitStun);
            
        }
    }

    void ShockOrbVanish()
    {
        Debug.Log("shock orb vanishing");
        Destroy(gameObject);
    }
}
