using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeEarthSpecial : PlayerMelee
{
    public GameObject earthSpecialPullInEffect;
    [HideInInspector]
    public float pullSpeed;
    [HideInInspector]
    public float effectDuration;

    public override void Start()
    {
        base.Start();
        //Debug.Log("Special Melee Attack Used");
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
                GameObject newearthSpecialPullInEffect = Instantiate(earthSpecialPullInEffect, other.transform.position, other.transform.rotation);
                newearthSpecialPullInEffect.GetComponent<EarthPullInEffect>().pullSpeed = pullSpeed;
                newearthSpecialPullInEffect.GetComponent<EarthPullInEffect>().effectDuration = effectDuration;
                newearthSpecialPullInEffect.transform.parent = other.transform;
                //Debug.Log("Pull in Effect" + newearthSpecialPullInEffect.name);
                health.TakeDamage(gameObject, meleeDamage, stunLockOut);
                //Uncomment if you only want it to hit a single guy, we can add a bool for hitting multipule guys if we want.
                //Destroy(gameObject);
            }
        }
    }
}
