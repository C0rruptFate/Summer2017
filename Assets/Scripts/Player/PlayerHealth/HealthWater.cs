using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthWater : PlayerHealth {

    [HideInInspector]
    public GameObject[] targets;
    [HideInInspector]
    public GameObject target;


    public override void TakeDamage(GameObject whatHitMe, float damage, float hitStun)
    {
        //damage reduction.
        float totalDamageModifier = 0;

        //if I am hit by a projectile or melee enemy
        if (whatHitMe.CompareTag("Projectile"))
        {
            if (whatHitMe.GetComponent<Projectiles>().element == Constants.whatCountersMe(element) && whatHitMe.GetComponent<Projectiles>().hurtsPlayers)
            {
                totalDamageModifier = totalDamageModifier - counterDamageModifier;
            }
            else if (whatHitMe.GetComponent<Projectiles>().element == Constants.whatICounter(element) && whatHitMe.GetComponent<Projectiles>().hurtsPlayers)
            {
                totalDamageModifier = totalDamageModifier + counterResistanceModifier;
            }
        }
        else
        {//if I am hit by an enemy
            if (whatHitMe.GetComponent<EnemyHealth>().element == Constants.whatCountersMe(element))
            {
                totalDamageModifier = totalDamageModifier - counterDamageModifier;
            }
            else if (whatHitMe.GetComponent<EnemyHealth>().element == Constants.whatICounter(element))
            {
                totalDamageModifier = totalDamageModifier + counterResistanceModifier;
            }
        }
        if (gameObject.GetComponent<PlayerAttacks>().blocking)//If I am blocking take reduced damage and no hitstun.
        {
            totalDamageModifier = totalDamageModifier + gameObject.GetComponent<PlayerAttacks>().blockingResistanceModifier;
            hitStun = 0;
        }
        if(gameObject.GetComponent<PlayerAttacks>().specialBlocking) //Heals the closest ally, this will not heal myself.
        {
            targets = GameObject.FindGameObjectsWithTag("Player");//Finds all players tagged as "player".
            float closestTargetDist = 0.0f;
            foreach (var possibleTarget in targets)//Finds the closest target by looping though all players and seeing who is closest.
            {
                if (possibleTarget.gameObject.activeSelf && possibleTarget.gameObject != gameObject)
                {
                    float dist = Vector2.Distance(possibleTarget.transform.position, gameObject.transform.position);
                    if (dist < closestTargetDist)
                    {
                        closestTargetDist = dist;
                        target = possibleTarget;
                    }
                    else if (closestTargetDist == 0.0f)
                    {
                        closestTargetDist = dist;
                        target = possibleTarget;
                    }
                }
            }
            target.GetComponent<PlayerHealth>().Heal(damage / 2); //Heals the other player for half the damage taken.
            //Debug.Log("Healed " + target.name + " for " + (damage / 2));
        }
        //if invulnerable don't take damage
        if (invulnerable)
        {
            damage = 0;
        }

        damage = damage * (1 - totalDamageModifier); //calculates total damage taken.
        health -= damage;//take damage

        if (hitStun != 0)//causes hit stun to disable player actions
        {
            //[TODO] Maybe include movement script.
            playerAttacks.enabled = false;
            Invoke("HitStun", hitStun);
        }
        playerUI.GetComponent<PlayerUI>().SetHealthUI();//Update the player's UI hp slider.
        if (health <= 0)//Kills the player
        {
            //trigger death animation

            // [TODO] explore getting a smaller list of only enemies targeting this player
            //enemiesList = GameObject.FindGameObjectsWithTag("Enemy");
            //foreach (var enemy in enemiesList)
            //{
            //    if (enemy.GetComponent<Enemy>().target == gameObject)
            //    {
            //        enemy.GetComponent<Enemy>().target = null;
            //    }
            //}
            PlayerDied();
        }
    }
}
