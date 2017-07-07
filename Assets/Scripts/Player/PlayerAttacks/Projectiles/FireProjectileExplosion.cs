using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectileExplosion : PlayerProjectile {

    public GameObject explosionEffect;

    // Use this for initialization
    public override void Start () {

        //Set's my element
        element = player.GetComponent<PlayerHealth>().element;

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

        }
        else if (GetComponent<CircleCollider2D>().radius == 6)
        {

        }
        else if (GetComponent<CircleCollider2D>().radius == 7)
        {

        }
        else if (GetComponent<CircleCollider2D>().radius == 8)
        {

        }
        //Debug.Log("Radius!!!: " + gameObject.GetComponent<CircleCollider2D>().radius);
        Invoke("DestroySelf", 0.5f);
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
