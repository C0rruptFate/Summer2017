using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthTurret : EnemyHealth {

    public override void TakeDamage(GameObject whatHitMe, float damage, float hitStun)
    {
        //turrets don't take hitstun.
        hitStun = 0;
        float totalDamageModifier = 0;
        //Checks the element of what hit me and causes me to take extra damage or reduced damage.
        if (whatHitMe.GetComponent<Projectiles>() != null)
        {
            if (whatHitMe.GetComponent<Projectiles>().element == Constants.whatCountersMe(element))
            {
                totalDamageModifier = totalDamageModifier - counterDamageModifier;
            }
            else if (whatHitMe.GetComponent<Projectiles>().element == Constants.whatICounter(element))
            {
                totalDamageModifier = totalDamageModifier + counterResistanceModifier;
            }
        }
        else if (whatHitMe.GetComponent<PlayerMelee>() != null)
        {
            if (whatHitMe.GetComponent<PlayerMelee>().myElement == Constants.whatCountersMe(element))
            {
                totalDamageModifier = totalDamageModifier - counterDamageModifier;
            }
            else if (whatHitMe.GetComponent<PlayerMelee>().myElement == Constants.whatICounter(element))
            {
                totalDamageModifier = totalDamageModifier + counterResistanceModifier;
            }
        }
        else if (whatHitMe.GetComponent<Hazard>() != null)
        {
            if (whatHitMe.GetComponent<Hazard>().element == Constants.whatCountersMe(element))
            {
                totalDamageModifier = totalDamageModifier - counterDamageModifier;
            }
            else if (whatHitMe.GetComponent<Hazard>().element == Constants.whatICounter(element))
            {
                totalDamageModifier = totalDamageModifier + counterResistanceModifier;
            }
        }
        else
        {
            Debug.LogError("Something tried to damage " + gameObject.name + " that I don't have listed!");
        }

        // [TODO] Enemies don't block (yet)
        //if (gameObject.GetComponent<PlayerAttacks>().blocking)//If I am blocking take reduced damage and no hitstun.
        //{
        //    totalDamageModifier = totalDamageModifier + gameObject.GetComponent<PlayerAttacks>().blockingResistanceModifier;
        //    Debug.Log("blocking mod: " + gameObject.GetComponent<PlayerAttacks>().blockingResistanceModifier);
        //    Debug.Log("total Damage Mod round 3 after blocking: " + totalDamageModifier);
        //    Debug.Log("damage round 3 after blocking: " + damage);
        //    hitStun = 0;
        //}

        //if invulnerable don't take damage
        if (invulnerable)
        {
            damage = 0;
        }

        damage = damage * (1 - totalDamageModifier); //calculates total damage taken.


        if (whatHitMe != whatCantHitMe)//Makes it so that this enemy can't be hit by the same attack right away (largely for melee attacks that last long).
        {
            health -= damage;//Takes damage.
            if (enemyHPUISliderObject.activeSelf == false)//Enables the hp bar for this enemy when they get hit.
            {
                enemyHPUISliderObject.SetActive(true);
            }
            UpDateEnemyUI();//Caues the enemy hp to update when they get hit.
            //Debug.Log("I took Damage: " + damage);
            //Hit stun
            myEnemyScript.enabled = false;
            Invoke("HitStun", hitStun);//calls hit stun as long as the attack that hit me says it should last for.
            whatCantHitMe = whatHitMe;//Makes it so that I can't be hit by the same object again.
            if (health <= 0)//If I am out of HP destroy gameobject.
            {
                //Spawns a random drop from drops
                int whatToSpawn = Random.Range(0, drops.Length); //Decides what to drop when the enemy is killed.
                GameObject pickUp = Instantiate(drops[whatToSpawn], transform.position, Quaternion.identity);//drops the pick up that is choosen at random.
                if (drops == null)
                {
                    Debug.LogError("Set up pickups");//If the pick up isn't set up play this message for the designer.
                }

                if (drops[whatToSpawn].GetComponent<PickUpHealth>() != null)//Sets the pick up element to my element.
                {
                    pickUp.GetComponent<PickUpHealth>().pickUpElement = element;
                }

                //calls destroy enemy
                Destroy(GetComponent<EnemyTurret>().shootPoint);
                DestroyObject();
            }
        }
    }
}
