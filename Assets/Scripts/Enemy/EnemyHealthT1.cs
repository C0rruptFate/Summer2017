using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthT1 : EnemyHealth {

    // Use this for initialization
    public override void Start()
    {
        myEnemyScript = gameObject.GetComponent<Enemy>();
        //Debug.Log("myEnemyScript: " + myEnemyScript);
        enemyHPUI.maxValue = health;
        //whatCantHitMe = gameObject;

        canvas = GameObject.Find("Canvas");

        if (!canvas)
        {
            canvas = new GameObject("Canvas");
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        //if(beenHit == true)
        //{
        //    enemyHPUI.transform.position = new Vector3(transform.position.x, transform.position.y - offSetUI, transform.position.z);
        //}
    }

    public override void TakeDamage(GameObject whatHitMe, float damage, float hitStun)
    {
        //[TODO] Pull the element of what hit me, if it is what counters me it deals bonus damage if I counter it then deal less damage.
        if (whatHitMe.CompareTag("Projectile"))
        {
            if (whatHitMe.GetComponent<PlayerProjectile>().myElement == Constants.whatCountersMe(element))
            {
                damage = damage * counterDamageModifier;
            }
            else if (whatHitMe.GetComponent<PlayerProjectile>().myElement == Constants.whatICounter(element))
            {
                damage = damage * counterResistanceModifier;
            }
        }
        else
        {
            if (whatHitMe.GetComponent<PlayerMelee>().myElement == Constants.whatCountersMe(element))
            {
                damage = damage * counterDamageModifier;
            }
            else if (whatHitMe.GetComponent<PlayerMelee>().myElement == Constants.whatICounter(element))
            {
                damage = damage * counterResistanceModifier;
            }
        }
        if (whatHitMe != whatCantHitMe)
        {
            health -= damage;
            if (enemyHPUIObject.activeSelf == false)
            {
                enemyHPUIObject.SetActive(true);
            }
            UpDateEnemyUI();
            //enemyHPUI.value = health;
            //Hit stun
            myEnemyScript.enabled = false;
            Invoke("HitStun", hitStun);
            whatCantHitMe = whatHitMe;
            if (health <= 0)
            {
                //Spawns a random drop from drops
                int whatToSpawn = Random.Range(0, drops.Length);
                GameObject pickUp = Instantiate(drops[whatToSpawn], transform.position, Quaternion.identity);
                if(drops == null)
                {
                    Debug.LogError("Set up drops");
                }

                if (drops[whatToSpawn].GetComponent<PickUpHealth>() != null)
                {
                    pickUp.GetComponent<PickUpHealth>().pickUpElement = element;
                }
                //trigger death animation

                DestroyObject();
            }
        }
    }

    public override void DestroyObject()
    {
        Destroy(gameObject);
    }

    public override void HitStun()
    {
        myEnemyScript.enabled = true;
        whatCantHitMe = gameObject;
    }

    public override void UpDateEnemyUI()
    {
        enemyHPUI.value = health;
    }
}
