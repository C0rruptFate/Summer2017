using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour {

	// Use this for initialization
	void Start () {
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
            //Debug.Log("Reflected my own projectile");
        }

        if (other.CompareTag("Projectile"))
        {
             Quaternion otherRotation = other.transform.rotation;
            otherRotation.y = other.transform.rotation.y - 180;
            //Debug.Log("Reflected A projectile");
        }


    }
}
