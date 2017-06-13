using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectileExplosion : MonoBehaviour {

    [HideInInspector]
    public GameObject player;

	// Use this for initialization
	void Start () {
        Invoke("DestroySelf", 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
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
