using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeEarthBasic : PlayerMelee
{
    public GameObject projectileSpawn;
    public Vector2 lobbedForce = new Vector2(20f,20f);

    public override void OnCollisionEnter2D(Collision2D other)
    {
        //lets me do damage to enemies when touching them.
        if (other.transform.tag == ("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            EnemyHealth health = other.gameObject.GetComponent<EnemyHealth>();
            GameObject meleeAttackEffect = Instantiate(effectParticle, other.transform.position, other.transform.rotation, gameObject.transform.parent);

            if (enemy && health)
            {
                health.TakeDamage(gameObject, meleeDamage, stunLockOut);
                //GameObject Instantiate(projectileSpawn,)
                GameObject newGroundProjectile = Instantiate(projectileSpawn, new Vector2(transform.position.x, transform.position.y + 1f), transform.rotation);
                newGroundProjectile.transform.parent = player.GetComponent<PlayerAttacks>().playerWeaponParent.transform;
                newGroundProjectile.GetComponent<PlayerProjectile>().player = player;
                SetBasicRangedAttackStats(newGroundProjectile);
                //Uncomment if you only want it to hit a single guy, we can add a bool for hitting multipule guys if we want.
                //Destroy(gameObject);
            }
        }
    }

    public virtual void SetBasicRangedAttackStats(GameObject projectile)//Sets the stats for the projectile object when it is created.
    {
        projectile.GetComponent<PlayerProjectile>().projectileSpeed = player.GetComponent<PlayerAttacks>().projectileSpeed;
        projectile.GetComponent<PlayerProjectile>().projectileDamage = player.GetComponent<PlayerAttacks>().projectileDamage;
        projectile.GetComponent<PlayerProjectile>().projectileHitStun = player.GetComponent<PlayerAttacks>().projectileHitStun;
        projectile.GetComponent<PlayerProjectile>().projectileMaxDuration = player.GetComponent<PlayerAttacks>().projectileMaxDuration;
        projectile.GetComponent<PlayerProjectile>().projectileBreakChance = player.GetComponent<PlayerAttacks>().projectileBreakChance;
        projectile.GetComponent<PlayerProjectile>().usesConstantForceProjectile = player.GetComponent<PlayerAttacks>().usesConstantForceProjectile;
        projectile.GetComponent<PlayerProjectile>().lobbedForce = lobbedForce;
        projectile.GetComponent<PlayerProjectile>().breaksHittingWall = player.GetComponent<PlayerAttacks>().breaksHittingWall;
        projectile.GetComponent<PlayerProjectile>().throwWaitTime = 0;
        projectile.GetComponent<PlayerProjectileEarthBasic>().maxHitCount = player.GetComponent<AttacksEarth>().maxHitCount;
    }
}
