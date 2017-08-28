using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokingLanternDamage : Hazard {
	
	// Update is called once per frame
	public override void Update () {
		
	}

    public override void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.tag == ("Player") && Time.time > newSwingTimer)
        {
            newSwingTimer = Time.time + swingTimer;
            //Debug.Log("Player should take damage");
            PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
            PlayerHealth health = other.gameObject.GetComponent<PlayerHealth>();

            //If what I am colliding with has both a player Controller and Health script, deal damage to them and knock them back.
            if (playerMovement && health)
            {
                health.TakeDamage(gameObject, damage, hitStun);
            }
        }
    }
}
