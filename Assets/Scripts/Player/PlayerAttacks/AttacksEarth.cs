using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttacksEarth : PlayerAttacks {
    [Header("Character Specific Settings")]
    [Tooltip("How many times can this hit something.")]
    public int maxHitCount = 2;
    [Tooltip("What force is applied if I use a lobbed projectile. 'This is only applied if a lobbed projectile is used'.")]
    public Vector2 specialLobbedForce = new Vector2(15, 15);
    public float forceMagnitude = 150;

    [Tooltip("Speed objects will be pulled in at.")]
    public float pullSpeed = 10;
    [Tooltip("How Long does my Melee pull in last.")]
    public float effectDuration = 5;

    [Tooltip("How Long does my Special Defend last.")]
    public float specialDefendDestroyWait = 4f;

    [Tooltip("How fast does my Special Defend Move.")]
    public float specialDefendMoveSpeed = 50f;

    [Tooltip("How many Hits can my Special Defend Take.")]
    public int specialDefendMaxHits = 5;

    // Use this for initialization
    public override void Start()
    {
        //Plugs myself into my PlayerHealth and Player movement scripts.
        // [TODO] CHANGE THESE GETCOMPONENTS FOR EACH ELEMENTAL ATTACK SCRIPT.
        GetComponent<PlayerHealth>().playerAttacks = GetComponent<AttacksEarth>();
        GetComponent<PlayerMovement>().playerAttacks = GetComponent<AttacksEarth>();
        base.Start();
    }

    protected override void SpecialMeleeAttack()
    {
        base.SpecialMeleeAttack();

        //[TODO] Set up special melee attack for each character.
    }

    public override void SpecialPlayerDefend()
    {//Might need to change spawn from location.
        GameObject specialDefender = Instantiate(specialDefendObject, new Vector3(groundGun.position.x, transform.position.y - 1f, transform.position.z), transform.rotation);
        //specialDefender.GetComponent<DestroyBlocking>().player = gameObject;
        specialDefender.transform.parent = playerWeaponParent.transform;
        SetSpecialDefendStats(specialDefender);
    }

    public override void SetSpecialMeleeAttackStats(GameObject melee)
    {
        base.SetSpecialMeleeAttackStats(melee);
        melee.GetComponent<PlayerMeleeEarthSpecial>().effectDuration = effectDuration;
        melee.GetComponent<PlayerMeleeEarthSpecial>().pullSpeed = pullSpeed;

    }

    public override void SetBasicRangedAttackStats(GameObject projectile)
    {
        base.SetBasicRangedAttackStats(projectile);
        projectile.GetComponent<PlayerProjectileEarthBasic>().maxHitCount = maxHitCount;
    }

    public override void SetSpecialRangedAttackStats(GameObject specialProjectile)
    {
        //specialProjectile.GetComponent<PlayerProjectileEarthSpecial>().enablePullWaitTime = throwWaitTime;
        specialProjectile.GetComponent<PlayerProjectileEarthSpecial>().forceMagnitude = forceMagnitude;
        specialProjectile.GetComponent<PlayerProjectile>().projectileDamage = specialRangedDamage;
        specialProjectile.GetComponent<PlayerProjectile>().projectileMaxDuration = specialProjectileMaxDuration;
        specialProjectile.GetComponent<PlayerProjectile>().lobbedForce = specialLobbedForce;
        specialProjectile.GetComponent<PlayerProjectileEarthSpecial>().throwWaitTime = throwWaitTime;
    }

    public override void SetSpecialDefendStats(GameObject defend)
    {
        defend.GetComponent<EarthSpecialDefend>().player = gameObject;
        defend.GetComponent<EarthSpecialDefend>().destroyWait = specialDefendDestroyWait;
        defend.GetComponent<EarthSpecialDefend>().moveSpeed = specialDefendMoveSpeed;
        defend.GetComponent<EarthSpecialDefend>().maxHits = specialDefendMaxHits;
        defend.GetComponent<EarthSpecialDefend>().myGun = groundMeleeGun;
    }
}
