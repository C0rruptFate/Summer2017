using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeWaterSpecial : PlayerMelee
{
    public override void Start()
    {
        base.Start();
    }
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
                if (other.transform.Find("Wet Effect"))
                {
                    //Heal
                    player.GetComponent<PlayerHealth>().Heal(meleeDamage);
                }

                health.TakeDamage(gameObject, meleeDamage, stunLockOut);
                //Uncomment if you only want it to hit a single guy, we can add a bool for hitting multipule guys if we want.
                //Destroy(gameObject);
            }
        }
    }
}
