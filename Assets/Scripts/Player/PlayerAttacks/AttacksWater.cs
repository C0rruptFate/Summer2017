using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttacksWater : PlayerAttacks {
    [Header("Character Specific Settings")]
    [Tooltip("How much does this heal for when your hp is ABOVE the threshold. (multiply by damage done.)")]
    public float baseHealReduction = 0.5f;

    [Tooltip("What percentage of HP do you need to be below to get the bonus healing.")]
    public float healMultiplierThreshold = 0.25f;
    [Tooltip("How much does this heal for when your hp is Below the threshold. (multiply by damage done.)")]
    public float healMultiplier = 1.5f;

    [Tooltip("Attach the wet effect here")]
    public GameObject wetEffect;
    [Tooltip("How long does this buff last for.")]
    public float wetEffectDuration;

    private bool rangedMultiShot = false;

    public float specialDefendMaxRange;
    public float specialDefendHealAmount;

    // Use this for initialization
    public override void Start()
    {
        //Plugs myself into my PlayerHealth and Player movement scripts.
        // [TODO] CHANGE THESE GETCOMPONENTS FOR EACH ELEMENTAL ATTACK SCRIPT.
        GetComponent<PlayerHealth>().playerAttacks = GetComponent<AttacksWater>();
        GetComponent<PlayerMovement>().playerAttacks = GetComponent<AttacksWater>();
        base.Start();
    }

    public override void MeleeAttack()
    {
        base.MeleeAttack();

    }

    public override void RangedAttack()
    {
        base.RangedAttack();

        if (!rangedMultiShot)
        {
            rangedMultiShot = true;
            
            Invoke("MultiShot", 0.25f);
            Invoke("MultiShot", 0.50f);

        }
        else
        {
            rangedMultiShot = false;
        }
        
    }

    public void MultiShot()
    {
        projectileNextFire = Time.time + projectileFireRate; //Decides when preform another ranged attack.
        //Shoots the projectile, put the projectile movement code on that object.
        //Checks if I am grounded. Creates the ranged object at my gun location, parents it to the weapons gameobject, and sets the weapon's location to the player's gun.
        switch (gameObject.GetComponent<PlayerMovement>().grounded)
        {//Put ranged attacks on the ground here.
            case true://Checks if grounded or not/

                //Set up a gun position object on each player.
                GameObject newGroundProjectile = Instantiate(groundProjectile, groundGun.position, groundGun.rotation);
                newGroundProjectile.transform.parent = playerWeaponParent.transform;
                newGroundProjectile.GetComponent<PlayerProjectile>().player = gameObject;
                SetBasicRangedAttackStats(newGroundProjectile);
                if (groundGunTwo != null)
                {
                    //Does the same thing for the secondary grounded projectile if one is set.
                    newGroundProjectile = Instantiate(groundProjectile, groundGunTwo.position, groundGunTwo.rotation);
                    newGroundProjectile.transform.parent = playerWeaponParent.transform;
                    newGroundProjectile.GetComponent<PlayerProjectile>().player = gameObject;
                    SetBasicRangedAttackStats(newGroundProjectile);
                }
                break;
            default://Set up a aerial gun position object on each player.
                GameObject newAirProjectile = Instantiate(airProjectile, airGun.position, airGun.rotation);
                newAirProjectile.transform.parent = playerWeaponParent.transform;
                newAirProjectile.GetComponent<PlayerProjectile>().player = gameObject;
                SetBasicRangedAttackStats(newAirProjectile);
                if (airGunTwo != null)
                {//Does the same thing for the secondary if one is set.
                    newAirProjectile = Instantiate(airProjectile, airGunTwo.position, airGunTwo.rotation);
                    newAirProjectile.transform.parent = playerWeaponParent.transform;
                    newAirProjectile.GetComponent<PlayerProjectile>().player = gameObject;
                    SetBasicRangedAttackStats(newAirProjectile);
                }
                break;
        }
    }//Same as base.RangedAttack, but needs to be here so it can be invoked.

    protected override void SpecialMeleeAttack()
    {
        base.SpecialMeleeAttack();

        //[TODO] Set up special melee attack for each character.
    }

    public override void SpecialPlayerDefend()
    {
        GameObject specialDefender = Instantiate(specialDefendObject, transform.position, transform.rotation);
        specialDefender.GetComponent<DestroyBlocking>().player = gameObject;
        specialDefender.transform.parent = playerWeaponParent.transform;
        SetSpecialDefendStats(specialDefender);
    }

    public override void SetBasicMeleeAttackStats(GameObject melee) //Sets the stats for the melee object when it is created.
    {
        base.SetBasicMeleeAttackStats(melee);
        melee.GetComponent<PlayerMeleeWaterBasic>().wetEffect = wetEffect;
        melee.GetComponent<PlayerMeleeWaterBasic>().wetEffectDuration = wetEffectDuration;
    }

    public override void SetSpecialRangedAttackStats(GameObject projectile)
    {
        base.SetSpecialRangedAttackStats(projectile);
        projectile.GetComponent<PlayerProjectileWaterSpecial>().baseHealReduction = baseHealReduction;
        projectile.GetComponent<PlayerProjectileWaterSpecial>().healMultiplierThreshold = healMultiplierThreshold;
        projectile.GetComponent<PlayerProjectileWaterSpecial>().healMultiplier = healMultiplier;
    }

    public override void SetSpecialDefendStats(GameObject defend)
    {
        base.SetSpecialDefendStats(defend);
        //defend.GetComponent
        //Range to nearest player
        //Healing amount.
    }
}
