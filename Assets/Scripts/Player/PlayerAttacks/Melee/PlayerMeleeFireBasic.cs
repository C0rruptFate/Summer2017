using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeFireBasic : PlayerMelee
{
    private bool alreadyGeneratedComboPoint = false;

    public override void OnCollisionEnter2D(Collision2D other)
    {
        //lets me do damage to enemies when touching them.
        if (other.transform.tag == ("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            EnemyHealth health = other.gameObject.GetComponent<EnemyHealth>();
            Instantiate(effectParticle, other.transform.position, other.transform.rotation, gameObject.transform.parent);

            if (enemy && health)
            {
                health.TakeDamage(gameObject, meleeDamage, stunLockOut);

                //Generates one combo point per attack when hitting an enemy. This can't go above max points.
                if (alreadyGeneratedComboPoint == false)
                {
                    alreadyGeneratedComboPoint = true;
                    player.GetComponent<AttacksFire>().currentComboPoints++;
                    if (player.GetComponent<AttacksFire>().currentComboPoints >= player.GetComponent<AttacksFire>().maxComboPoints)
                    {
                        player.GetComponent<AttacksFire>().currentComboPoints = player.GetComponent<AttacksFire>().maxComboPoints;
                    }
                    //Debug.Log("Generates a combo point" + player.GetComponent<AttacksFire>().currentComboPoints);
                    player.GetComponent<PlayerHealth>().playerUI.GetComponent<PlayerUI>().UpdateComboPointUI();
                }
                //Uncomment if you only want it to hit a single guy, we can add a bool for hitting multipule guys if we want.
                //Destroy(gameObject);
            }

        }
    }
}
