using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectileExplosion : PlayerProjectile {

    public GameObject explosionEffect;

    // Use this for initialization
    public override void Start () {

        //Set's my element
        element = shooter.GetComponent<PlayerHealth>().element;

        //enables my collider as they start disabled.
        if (gameObject.GetComponent<Collider2D>().enabled == false)
        {
            gameObject.GetComponent<Collider2D>().enabled = true;
        }

        if (GetComponent<CircleCollider2D>().radius == 3)
        {
            explosionEffect.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        }
        else if (GetComponent<CircleCollider2D>().radius == 4)
        {
            explosionEffect.transform.localScale = new Vector3(1.7f, 1.7f, 1.7f);
        }
        else if (GetComponent<CircleCollider2D>().radius == 5)
        {
            explosionEffect.transform.localScale = new Vector3(2.1f, 2.1f, 2.1f);
        }
        else if (GetComponent<CircleCollider2D>().radius == 6)
        {
            explosionEffect.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        }
        else if (GetComponent<CircleCollider2D>().radius == 7)
        {
            explosionEffect.transform.localScale = new Vector3(2.9f, 2.9f, 2.9f);
        }
        else if (GetComponent<CircleCollider2D>().radius == 8)
        {
            explosionEffect.transform.localScale = new Vector3(3.3f, 3.3f, 3.3f);
        }
        //Debug.Log("Radius!!!: " + gameObject.GetComponent<CircleCollider2D>().radius);
        Invoke("DestroySelf", 0.5f);
	}

    public override void FixedUpdate()
    {
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (hurtsPlayers == false)
        {
            if (other.tag == "Enemy")
            {
                other.GetComponent<EnemyHealth>().TakeDamage(gameObject, shooter.GetComponent<PlayerAttacks>().specialRangedDamage, shooter.GetComponent<PlayerAttacks>().specialRangedHitStun);
            }
        }
        else if (hurtsPlayers == true)
        {
            if (other.tag == "Player")
            {
                other.GetComponent<EnemyHealth>().TakeDamage(gameObject, shooter.GetComponent<PlayerAttacks>().specialRangedDamage, shooter.GetComponent<PlayerAttacks>().specialRangedHitStun);
            }
        }
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
