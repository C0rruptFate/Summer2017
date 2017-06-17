using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttacksAir : PlayerAttacks {
    [Header("Character Specific Settings")]
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

    public override void MeleeAttack()
    {
        meleeNextFire = Time.time + meleeFireRate; //Decides when preform another melee attack.
        //Checks if I am grounded. Creates the melee object at my gun location, parents it to the weapons gameobject, and sets the weapon's location to the player's gun.
        switch (gameObject.GetComponent<PlayerMovement>().grounded)
        {
            case true:
                //Put melee attacks on the ground here.
                newGroundMelee = Instantiate(meleeObject, groundMeleeGun.position, groundMeleeGun.rotation);
                newGroundMelee.transform.parent = playerWeaponParent.transform;
                newGroundMelee.GetComponent<PlayerMelee>().player = gameObject;
                newGroundMelee.GetComponent<PlayerMelee>().myGun = groundMeleeGun;
                SetBasicMeleeAttackStats(newGroundMelee);
                if (groundMeleeGunTwo != null)//Does the same for the 2nd grounded melee attack, if I have one.
                {
                    newGroundMelee = Instantiate(meleeObject, groundMeleeGunTwo.position, groundMeleeGunTwo.rotation);
                    newGroundMelee.transform.parent = playerWeaponParent.transform;
                    newGroundMelee.GetComponent<PlayerMelee>().player = gameObject;
                    newGroundMelee.GetComponent<PlayerMelee>().myGun = groundMeleeGunTwo;
                    SetBasicMeleeAttackStats(newGroundMelee);
                }
                break;
            default://If I am not grounded. Creates the melee object at my gun location, parents it to the weapons gameobject, and sets the weapon's location to the player's gun.
                //Do Air melee attack stuff here. 
                newAirMelee = Instantiate(meleeObject, airMeleeGun.position, airMeleeGun.rotation);
                newAirMelee.transform.parent = playerWeaponParent.transform;
                newAirMelee.GetComponent<PlayerMelee>().player = gameObject;
                newAirMelee.GetComponent<PlayerMelee>().myGun = airMeleeGun;
                SetBasicMeleeAttackStats(newAirMelee);
                if (airMeleeGunTwo != null) //Does the same for the 2nd aerial, if I have one.
                {
                    Invoke("SecondaryMeleeAttack", meleeHitBoxLife + 0.1f);
                }
                break;
        }
    }

    public void SecondaryMeleeAttack()
    {
        newAirMelee = Instantiate(meleeObject, airMeleeGunTwo.position, airMeleeGunTwo.rotation);
        newAirMelee.transform.parent = playerWeaponParent.transform;
        newAirMelee.GetComponent<PlayerMelee>().player = gameObject;
        newAirMelee.GetComponent<PlayerMelee>().myGun = airMeleeGunTwo;
        SetBasicMeleeAttackStats(newAirMelee);
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
