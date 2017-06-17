using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeWaterBasic : PlayerMelee
{
    [HideInInspector]
    public GameObject wetEffect;
    [HideInInspector]
    public float wetEffectDuration = 3;

    //private GameObject newWetEffect;

    public override void OnCollisionEnter2D(Collision2D other)
    {
        //lets me do damage to enemies when touching them.
        if (other.transform.tag == ("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            EnemyHealth health = other.gameObject.GetComponent<EnemyHealth>();

            if (enemy && health)
            {
                //Checks if they already have a wet effect on them, if they do resets it duration. If they don't add one.
                if(other.transform.Find("Wet Effect") != null)
                {
                    other.transform.Find("Wet Effect").GetComponent<DestroySelfRightAway>().waitToDestroyTime = wetEffectDuration;
                    Debug.Log("Reset Wet Duration: " + wetEffectDuration);
                }
                else
                {
                    Debug.Log("Granted new Wet");
                    GameObject newWetEffect = Instantiate(wetEffect, other.transform.position, other.transform.rotation);
                    newWetEffect.transform.parent = other.transform;
                    newWetEffect.gameObject.name = "Wet Effect";/*player.GetComponent<AttacksWater>().wetEffect.ToString()*/;
                    newWetEffect.GetComponent<DestroySelfRightAway>().waitToDestroyTime = wetEffectDuration;
                }

                health.TakeDamage(gameObject, meleeDamage, stunLockOut);
                //Uncomment if you only want it to hit a single guy, we can add a bool for hitting multipule guys if we want.
                //Destroy(gameObject);
            }
        }
    }
}
