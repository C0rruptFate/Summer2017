using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectileExplosion : PlayerProjectile {

    // Use this for initialization
    public override void Start () {

        //Set's my element
        element = player.GetComponent<PlayerHealth>().element;

        //enables my collider as they start disabled.
        if (gameObject.GetComponent<Collider2D>().enabled == false)
        {
            gameObject.GetComponent<Collider2D>().enabled = true;
        }
        //Debug.Log("Radius!!!: " + gameObject.GetComponent<CircleCollider2D>().radius);
        Invoke("DestroySelf", 0.1f);
	}

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (hurtsPlayers == false)
        {
            if (other.tag == "Enemy")
            {
                other.GetComponent<EnemyHealth>().TakeDamage(gameObject, player.GetComponent<PlayerAttacks>().specialRangedDamage, player.GetComponent<PlayerAttacks>().specialRangedHitStun);
            }
        }
        else if (hurtsPlayers == true)
        {
            if (other.tag == "Player")
            {
                other.GetComponent<EnemyHealth>().TakeDamage(gameObject, player.GetComponent<PlayerAttacks>().specialRangedDamage, player.GetComponent<PlayerAttacks>().specialRangedHitStun);
            }
        }
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
