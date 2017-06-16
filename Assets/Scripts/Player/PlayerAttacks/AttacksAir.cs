﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttacksAir : PlayerAttacks {

    [Tooltip("The radius of the shock orb trigger checker.")]
    public float shockOrbRadius;

    // Use this for initialization
    public override void Start()
    {
        //Plugs myself into my PlayerHealth and Player movement scripts.
        // [TODO] CHANGE THESE GETCOMPONENTS FOR EACH ELEMENTAL ATTACK SCRIPT.
        GetComponent<PlayerHealth>().playerAttacks = GetComponent<AttacksAir>();
        GetComponent<PlayerMovement>().playerAttacks = GetComponent<AttacksAir>();
        base.Start();
    }

    protected override void SpecialMeleeAttack()
    {
        base.SpecialMeleeAttack();

        //[TODO] Set up special melee attack for each character.
    }

    public override void SpecialPlayerDefend()
    {
        //Spends the mana to use your special ranged attack.
        //playerHealth.SpendMana(specialDefendManaCost);

        //[TODO] Set up the special Defend for each character.

    }

    public override void SetSpecialMeleeAttackStats(GameObject melee)
    {
        base.SetSpecialMeleeAttackStats(melee);
    }

    public override void SetSpecialRangedAttackStats(GameObject projectile)
    {
        projectile.GetComponent<PlayerProjectile>().projectileSpeed = specialProjectileSpeed;
        projectile.GetComponent<PlayerProjectile>().projectileDamage = specialRangedDamage;
        projectile.GetComponent<PlayerProjectile>().projectileHitStun = specialRangedHitStun;
        projectile.GetComponent<PlayerProjectile>().projectileMaxDuration = specialProjectileMaxDuration;
        projectile.GetComponent<PlayerProjectile>().projectileBreakChance = specialProjectileBreakChance;
        projectile.GetComponent<PlayerProjectile>().usesConstantForceProjectile = usesConstantForceProjectile;
        projectile.GetComponent<PlayerProjectile>().breaksHittingWall = specialBreaksHittingWall;
        projectile.GetComponent<PlayerProjectile>().throwWaitTime = throwWaitTime;
    }
}
