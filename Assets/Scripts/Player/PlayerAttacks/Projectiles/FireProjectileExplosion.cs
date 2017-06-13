using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectileExplosion : PlayerProjectile {

	// Use this for initialization
	void Start () {

        //Set's my element
        myElement = player.GetComponent<PlayerHealth>().element;

        //enables my collider as they start disabled.
        if (gameObject.GetComponent<Collider2D>().enabled == false)
        {
            gameObject.GetComponent<Collider2D>().enabled = true;
        }

        Invoke("DestroySelf", 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealth>().TakeDamage(gameObject, player.GetComponent<PlayerAttacks>().specialRangedDamage, player.GetComponent<PlayerAttacks>().specialRangedHitStun);
        }
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
