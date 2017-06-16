using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeWaterBasic : PlayerMelee
{
    [HideInInspector]
    public GameObject wetEffect;
    [HideInInspector]
    public float wetEffectDuration;

    public override void OnCollisionEnter2D(Collision2D other)
    {
        //lets me do damage to enemies when touching them.
        if (other.transform.tag == ("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            EnemyHealth health = other.gameObject.GetComponent<EnemyHealth>();

            if (enemy && health)
            {
                GameObject newWetEffect = Instantiate(wetEffect, other.transform.position, other.transform.rotation);
                newWetEffect.transform.parent = other.transform;
                newWetEffect.gameObject.name = player.GetComponent<AttacksWater>().wetEffect.ToString();
                newWetEffect.GetComponent<DestroySelfRightAway>().waitToDestroyTime = wetEffectDuration;
                health.TakeDamage(gameObject, meleeDamage, stunLockOut);
                //Uncomment if you only want it to hit a single guy, we can add a bool for hitting multipule guys if we want.
                //Destroy(gameObject);
            }

        }
    }
}
