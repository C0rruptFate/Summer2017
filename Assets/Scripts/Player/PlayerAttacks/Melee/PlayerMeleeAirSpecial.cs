using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAirSpecial : PlayerMelee
{
    [Tooltip("Drag in this air Melee special Effect prefab")]
    public GameObject airEffect;

    [HideInInspector]
    public float airEffectDuration = 3f;

    public override void OnCollisionEnter2D(Collision2D other)
    {
        //lets me do damage to enemies when touching them.
        if (other.transform.tag == ("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            EnemyHealth health = other.gameObject.GetComponent<EnemyHealth>();

            if (enemy && health)
            {
                health.TakeDamage(gameObject, meleeDamage, stunLockOut);
                GameObject newAirEffect = Instantiate(airEffect, other.transform.position, other.transform.rotation);
                newAirEffect.transform.parent = other.transform;
                newAirEffect.gameObject.name = "Air Effect";/*player.GetComponent<AttacksWater>().wetEffect.ToString()*/;
                newAirEffect.GetComponent<DestroySelfRightAway>().waitToDestroyTime = airEffectDuration;
                //Uncomment if you only want it to hit a single guy, we can add a bool for hitting multipule guys if we want.
                //Destroy(gameObject);
            }
        }
    }
}
