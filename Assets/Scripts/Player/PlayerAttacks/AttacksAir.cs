using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttacksAir : PlayerAttacks {
    [Header("Character Specific Settings")]

    [Tooltip("The number of times my basic projectile can hit a wall before vanishing.")]
    public int wallHitCount = 3;
    [Tooltip("The radius of the shock orb trigger checker.")]
    public float shockOrbRadius;

    [Tooltip("How long does this debuff last?")]
    public float airEffectDuration = 3f;

    public Transform reflectorPoint;

    [HideInInspector]
    public float fullSpecialDefendCooldown;
    public float reducedSpecialDefendCooldown;

    [HideInInspector]
    public bool reflectedSomething = false;

    // Use this for initialization
    public override void Start()
    {
        //Plugs myself into my PlayerHealth and Player movement scripts.
        // [TODO] CHANGE THESE GETCOMPONENTS FOR EACH ELEMENTAL ATTACK SCRIPT.
        GetComponent<PlayerHealth>().playerAttacks = GetComponent<AttacksAir>();
        GetComponent<PlayerMovement>().playerAttacks = GetComponent<AttacksAir>();
        base.Start();
        //Removes the reflector from myself and puts it on the weapon Parent.
        reflectorPoint.parent = playerWeaponParent.transform;
        fullSpecialDefendCooldown = specialDefendCooldown;
    }

    public override void Update()
    {
        //Calls Wisp and ticks up how long it has been held down. When greater than 20 the Wisp will attach to you.
        //Debug.Log("Callwisp" + Input.GetAxisRaw("CallWisp" + playerNumber));
        if (Input.GetAxisRaw("CallWisp" + playerNumber) > 0.25f)
        {
            CallWisp();
            callingWisp = true;
        }//Stops calling the Wisp when the button isn't held down.
        else if (Input.GetAxisRaw("CallWisp" + playerNumber) <= 0.25f)
        {
            callingWisp = false;
        }

        //Activate Special
        if (Input.GetAxisRaw("Special" + playerNumber) == 1)//Enables the special attack to be used by the melee, ranged, and defend attacks.
        {
            //print("Special Trigger pressed" + Input.GetAxis("Special" + playerNumber));

            //[TODO]Play special particle effect
            //turn special is active to true
            //Debug.Log("Special is active.");
            specialActive = true;
            if (transform.Find("specialActiveEffect") == null)
            {
                GameObject newSpecialActiveEffect = Instantiate(specialActiveEffect, transform.position, transform.rotation);
                newSpecialActiveEffect.name = "specialActiveEffect";
                newSpecialActiveEffect.transform.parent = transform;
            }
        }
        if (Input.GetAxisRaw("Special" + playerNumber) != 1 && specialActive)//Turns off the special when the button/trigger is released.
        {
            //Turn off special
            //Debug.Log("SpeciaL has been DEACTIVATED");
            specialActive = false;
            if (transform.Find("specialActiveEffect") != null)
            {
                Destroy(transform.Find("specialActiveEffect").gameObject);
            }
        }

        //Melee attacks
        //[TODO ALSO REQUIRE MANA TO BE >=SPECIAL MELEE MANA COST]
        if (specialActive && currentSpecialMeleeCooldown == 0 && Input.GetButtonDown("Melee" + playerNumber) && Time.time > meleeNextFire && playerHealth.allowedToInputAttacks)//Special Melee Attack
        {
            //Debug.Log("Melee Special is active.");
            SpecialMeleeAttack();
        }
        else if (Input.GetButtonDown("Melee" + playerNumber) && Time.time > meleeNextFire && playerHealth.allowedToInputAttacks)//Melee Attack
        {
            MeleeAttack();
        }

        //Ranged Attacks
        //[TODO ALSO REQUIRE MANA TO BE >=SPECIAL MELEE MANA COST]
        if (specialActive && currentSpecialRangedCooldown <= 0 && Input.GetButtonDown("Ranged" + playerNumber) && Time.time > projectileNextFire && playerHealth.allowedToInputAttacks)//Special Ranged Attack
        {
            //Debug.Log("Ranged Special is active.");
            SpecialRangedAttack();
        }
        else if (Input.GetButtonDown("Ranged" + playerNumber) && Time.time > projectileNextFire && playerHealth.allowedToInputAttacks)//Ranged Attack
        {
            RangedAttack();
        }

        //Defend
        if (specialActive && currentSpecialDefendCooldown == 0 && Input.GetButton("Defend" + playerNumber) && playerHealth.allowedToInputAttacks)//Special Block
        {
            if (!blocking)
            {
                //Debug.Log("Defend Special is active.");
                blocking = true;
                specialBlocking = true;
                SpecialPlayerDefend();
            }
        }
        else if (blocking && specialBlocking)//Causes me to release the block.
        {
            blocking = false;
            specialBlocking = false;
            blockNextFire = Time.time + blockFireRate;
            if (reflectedSomething)
            {
                specialDefendCooldown = reducedSpecialDefendCooldown;
            }
            else
            {
                specialDefendCooldown = fullSpecialDefendCooldown;
            }
            currentSpecialDefendCooldown = specialDefendCooldown;

            //PlayerDefend();
        }
        else if (Input.GetButton("Defend" + playerNumber) && Time.time >= blockNextFire && playerHealth.allowedToInputAttacks)//Block
        {
            if (!blocking)//If I am not already blocking start blocking
            {
                blocking = true;
                PlayerDefend();//Creates the block effect and all that goes with that.
            }
        }
        else if (blocking)//Causes me to release the block.
        {
            blocking = false;
            blockNextFire = Time.time + blockFireRate;
            PlayerDefend();
        }

        //Cooldowns
        //Melee Cooldown
        if (currentSpecialMeleeCooldown == specialMeleeCooldown)
        {
            StartCoroutine(meleeCooldownCoroutine);
        }
        else if (currentSpecialMeleeCooldown <= 0)
        {
            StopCoroutine(meleeCooldownCoroutine);
        }
        //Ranged Cooldown
        if (currentSpecialRangedCooldown == specialRangedCooldown)
        {
            StartCoroutine(rangedCooldownCoroutine);
        }
        else if (currentSpecialRangedCooldown <= 0)
        {
            StopCoroutine(rangedCooldownCoroutine);
        }
        //Defend Cooldown
        if (currentSpecialDefendCooldown == specialDefendCooldown)
        {
            StartCoroutine(defendCooldownCoroutine);
        }
        else if (currentSpecialDefendCooldown <= 0)
        {
            StopCoroutine(defendCooldownCoroutine);
        }
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

    public override void SetBasicRangedAttackStats(GameObject projectile)
    {
        base.SetBasicRangedAttackStats(projectile);
        projectile.GetComponent<PlayerProjectileAirBasic>().wallHitCount = wallHitCount;
    }

    public override void SpecialPlayerDefend()
    {
        GameObject specialDefender = Instantiate(specialDefendObject, transform.position, transform.rotation);
        specialDefender.GetComponent<DestroyBlocking>().player = gameObject;
        specialDefender.transform.parent = playerWeaponParent.transform;
        specialDefender.GetComponent<Reflector>().myCharacter = gameObject;
        SetSpecialDefendStats(specialDefender);

    }

    public override void SetSpecialMeleeAttackStats(GameObject melee)
    {
        base.SetSpecialMeleeAttackStats(melee);
        melee.GetComponent<PlayerMeleeAirSpecial>().airEffectDuration = airEffectDuration;
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

    public override void SetSpecialDefendStats(GameObject defend)
    {
        base.SetSpecialDefendStats(defend);
        defend.GetComponent<Reflector>().hurtsPlayers = false;
        defend.GetComponent<Reflector>().reflectPoint = reflectorPoint;
    }
}
