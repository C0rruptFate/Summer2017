using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour {
    
    public bool hurtsPlayers = true;

    public Transform reflectPoint;

	// Use this for initialization
	void Start () {
        reflectPoint.transform.position = transform.position;
        Debug.Log("reflector has spawned.");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerProjectileAirBasic>() != null)
        {
            other.GetComponent<PlayerProjectileAirBasic>().currentLife = Time.time + other.GetComponent<PlayerProjectileAirBasic>().projectileMaxDuration;
            other.GetComponent<PlayerProjectileAirBasic>().returnToPlayer = false;
            other.GetComponent<Projectiles>().hurtsPlayers = hurtsPlayers;
            //Debug.Log("Reflected my own projectile");
        }
        else if (other.GetComponent<Projectiles>() != null)
        {
            Debug.Log(other.gameObject.name + " Hit me");
            Debug.Log("other.transform.rotation after changes: " + other.transform.rotation);
            other.GetComponent<Projectiles>().usesConstantForceProjectile = false;
            other.GetComponent<Projectiles>().reflectedPoint = reflectPoint;
            other.GetComponent<Projectiles>().hurtsPlayers = hurtsPlayers;
            //Debug.Log("Reflected A projectile");
        }
    }
}
