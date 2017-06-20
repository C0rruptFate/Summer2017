﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttacksEarth : PlayerAttacks {
    [Header("Character Specific Settings")]
    [Tooltip("What force is applied if I use a lobbed projectile. 'This is only applied if a lobbed projectile is used'.")]
    public Vector2 specialLobbedForce = new Vector2(15, 15);
    public float forceMagnitude = 150;

    [Tooltip("Speed objects will be pulled in at.")]
    public float pullSpeed = 10;
    [Tooltip("How Long does this last.")]
    public float effectDuration = 5;

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
    {
        //Spends the mana to use your special ranged attack.
        //playerHealth.SpendMana(specialDefendManaCost);

        //[TODO] Set up the special Defend for each character.

    }

    public override void SetSpecialMeleeAttackStats(GameObject melee)
    {
        base.SetSpecialMeleeAttackStats(melee);
        melee.GetComponent<PlayerMeleeEarthSpecial>().effectDuration = effectDuration;
        melee.GetComponent<PlayerMeleeEarthSpecial>().pullSpeed = pullSpeed;

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
}
