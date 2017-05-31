using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttacksFire : PlayerAttacks {

    // Use this for initialization
    void Start () {
        // [TODO] CHANGE THESE GETCOMPONENTS FOR EACH ELEMENTAL ATTACK SCRIPT.
        GetComponent<PlayerHealth>().playerAttacks = GetComponent<AttacksFire>();
        GetComponent<PlayerMovement>().playerAttacks = GetComponent<AttacksFire>();
        playerNumber = playerHealth.playerNumber;
        rb = GetComponent<Rigidbody2D>();
        //Finds the wisp target location object and changes it's name
        myWispTargetLocation = transform.Find("Wisp Target Location");
        myWispTargetLocation.name = gameObject.name + "Wisp Target Location";
        myWispTargetLocation.parent = null;
        //Find the wisp object
        wisp = GameObject.Find("Wisp");

        //Set's up the player's weapon parent object
        playerWeaponParent = GameObject.Find("Player Attacks");

        if (!playerWeaponParent)
        {
            playerWeaponParent = new GameObject("Player Attacks");
        }

        //Melee attack Setup
        #region
        switch (groundMeleeType)
        {
            case AttackFromLocation.Overhead:
                groundMeleeGun = transform.Find("Overhead Gun");
                break;
            case AttackFromLocation.DownAngled:
                groundMeleeGun = transform.Find("Down Angled Gun");
                break;
            case AttackFromLocation.Below:
                groundMeleeGun = transform.Find("Below Gun");
                break;
            case AttackFromLocation.Behind:
                groundMeleeGun = transform.Find("Behind Gun");
                break;
            case AttackFromLocation.Self:
                groundMeleeGun = transform;//Targets self rather than a gun set up.
                break;
            case AttackFromLocation.Empty: //Shouldn't happen
                groundMeleeGun = null;
                break;
            case AttackFromLocation.Horizontal:
            default:
                groundMeleeGun = transform.Find("Horizontal Gun");
                break;
        }
        if (groundMeleeGun == null)
        {
            Debug.LogError("Can't find the PRIMARY GROUND MELEE gun for " + gameObject);
        }

        switch (groundMeleeTwoType)
        {
            case AttackFromLocation.Overhead:
                groundMeleeGunTwo = transform.Find("Overhead Gun");
                break;
            case AttackFromLocation.DownAngled:
                groundMeleeGunTwo = transform.Find("Down Angled Gun");
                break;
            case AttackFromLocation.Below:
                groundMeleeGunTwo = transform.Find("Below Gun");
                break;
            case AttackFromLocation.Behind:
                groundMeleeGunTwo = transform.Find("Behind Gun");
                break;
            case AttackFromLocation.Self:
                groundMeleeGunTwo = transform;//Targets self rather than a gun set up.
                break;
            case AttackFromLocation.Horizontal:
                groundMeleeGunTwo = transform.Find("Horizontal Gun");
                break;
            case AttackFromLocation.Empty:
            default:
                groundMeleeGunTwo = null;
                break;
        }
        if (groundMeleeGunTwo == null)
        {
            Debug.Log("Can't find the SECONDARY GROUND MELEE gun for " + gameObject);
        }

        switch (airMeleeType)
        {
            case AttackFromLocation.Overhead:
                airMeleeGun = transform.Find("Overhead Gun");
                break;
            case AttackFromLocation.DownAngled:
                airMeleeGun = transform.Find("Down Angled Gun");
                break;
            case AttackFromLocation.Below:
                airMeleeGun = transform.Find("Below Gun");
                break;
            case AttackFromLocation.Behind:
                airMeleeGun = transform.Find("Behind Gun");
                break;
            case AttackFromLocation.Self:
                airMeleeGun = transform;//Targets self rather than a gun set up.
                break;
            case AttackFromLocation.Empty:
                airMeleeGun = null;
                break;
            case AttackFromLocation.Horizontal:
            default:
                airMeleeGun = transform.Find("Horizontal Gun");
                break;
        }
        if (airMeleeGun == null)
        {
            Debug.LogError("Can't find the PRIMARY AIR MELEE gun for " + gameObject);
        }

        switch (airMeleeTwoType)
        {
            case AttackFromLocation.Overhead:
                airMeleeGunTwo = transform.Find("Overhead Gun");
                break;
            case AttackFromLocation.DownAngled:
                airMeleeGunTwo = transform.Find("Down Angled Gun");
                break;
            case AttackFromLocation.Below:
                airMeleeGunTwo = transform.Find("Below Gun");
                break;
            case AttackFromLocation.Behind:
                airMeleeGunTwo = transform.Find("Behind Gun");
                break;
            case AttackFromLocation.Self:
                airMeleeGunTwo = transform;//Targets self rather than a gun set up.
                break;
            case AttackFromLocation.Horizontal:
                airMeleeGunTwo = transform.Find("Horizontal Gun");
                break;
            case AttackFromLocation.Empty:
            default:
                airMeleeGunTwo = null;
                break;
        }
        if (airMeleeGunTwo == null)
        {
            Debug.Log("Can't find the SECONDARY AIR MELEE gun for player " + gameObject);
        }
        //Jump attack Setup
        jumpMeleeGun = transform.Find("Below Gun");
        #endregion

        //Ranged attack Setup
        #region 
        switch (groundProjectileType)
        {
            case AttackFromLocation.Overhead:
                groundGun = transform.Find("Overhead Gun");
                break;
            case AttackFromLocation.DownAngled:
                groundGun = transform.Find("Down Angled Gun");
                break;
            case AttackFromLocation.Below:
                groundGun = transform.Find("Below Gun");
                break;
            case AttackFromLocation.Behind:
                groundGun = transform.Find("Behind Gun");
                break;
            case AttackFromLocation.Self:
                groundGun = transform;//Targets self rather than a gun set up.
                break;
            case AttackFromLocation.Empty: //No ground projectile selected on main gun.
                groundGun = null;
                break;
            case AttackFromLocation.Horizontal:
            default:
                groundGun = transform.Find("Horizontal Gun");
                break;
        }
        if (groundGun == null)
        {
            Debug.LogError("No GROUND PROJECTILE selected on PRIMARY gun for " + gameObject);
        }
        //Secondary gun slot
        switch (groundProjectileTwoType)
        {
            case AttackFromLocation.Overhead:
                groundGunTwo = transform.Find("Overhead Gun");
                break;
            case AttackFromLocation.DownAngled:
                groundGunTwo = transform.Find("Down Angled Gun");
                break;
            case AttackFromLocation.Below:
                groundGunTwo = transform.Find("Below Gun");
                break;
            case AttackFromLocation.Behind:
                groundGunTwo = transform.Find("Behind Gun");
                break;
            case AttackFromLocation.Self:
                groundGunTwo = transform;//Targets self rather than a gun set up.
                break;
            case AttackFromLocation.Horizontal: //No ground projectile selected on main gun.
                groundGunTwo = transform.Find("Horizontal Gun");
                break;
            case AttackFromLocation.Empty:
            default:
                groundGunTwo = null;
                break;
        }
        if (groundGunTwo == null)
        {
            Debug.Log("No GROUND PROJECTILE selected on SECONDARY gun for " + gameObject);
        }

        switch (airProjectileType)
        {
            case AttackFromLocation.Overhead:
                airGun = transform.Find("Overhead Gun");
                break;
            case AttackFromLocation.DownAngled:
                airGun = transform.Find("Down Angled Gun");
                break;
            case AttackFromLocation.Below:
                airGun = transform.Find("Below Gun");
                break;
            case AttackFromLocation.Behind:
                airGun = transform.Find("Behind Gun");
                break;
            case AttackFromLocation.Self:
                airGun = transform;//Targets self rather than a gun set up.
                break;
            case AttackFromLocation.Horizontal:
            default:
                airGun = transform.Find("Horizontal Gun");
                break;
        }
        if (airGun == null)
        {
            Debug.LogError("No air projectile selected on PRIMARY gun for " + gameObject);
        }
        //Secondary projectile Type
        switch (airProjectileTwoType)
        {
            case AttackFromLocation.Overhead:
                airGunTwo = transform.Find("Overhead Gun");
                break;
            case AttackFromLocation.DownAngled:
                airGunTwo = transform.Find("Down Angled Gun");
                break;
            case AttackFromLocation.Below:
                airGunTwo = transform.Find("Below Gun");
                break;
            case AttackFromLocation.Behind:
                airGunTwo = transform.Find("Behind Gun");
                break;
            case AttackFromLocation.Self:
                airGunTwo = transform;//Targets self rather than a gun set up.
                break;
            case AttackFromLocation.Horizontal:
                airGunTwo = transform.Find("Horizontal Gun");
                break;
            case AttackFromLocation.Empty:
            default:
                airGunTwo = null;
                break;
        }
        if (airGunTwo == null)
        {
            Debug.Log("No air projectile selected on SECONDARY gun for " + gameObject);
        }
        #endregion
    }

    // Update is called once per frame
    void Update () {

        //Call Wisp

        if (Input.GetAxisRaw("CallWisp" + playerNumber) >= .25f)
        {
            if (!callingWisp)
            {
                CallWisp();
                callingWisp = true;
            }
        }
        if (Input.GetAxisRaw("CallWisp" + playerNumber) < 0.25f)
        {
            callingWisp = false;
        }

        //Special Atack
        if (Input.GetAxisRaw("Special" + playerNumber) == 1)
        {
            //print("Special Trigger pressed" + Input.GetAxis("Special" + playerNumber));

            //[TODO]Play special particle effect
            //turn special is active to true
            specialActive = true;
        }
        if (Input.GetAxisRaw("Special" + playerNumber) != 1 && specialActive)
        {
            //Turn off special
            specialActive = false;
        }

        //Melee Attack
            if (Input.GetButtonDown("Melee"+ playerNumber) && Time.time > meleeNextFire)
        {
            MeleeAttack();
        }

        //Ranged Attack
        if (Input.GetButtonDown("Ranged" + playerNumber) && Time.time > projectileNextFire)
        {
            RangedAttack();
        }

        //Defend
        if (Input.GetButton("Defend" + playerNumber) && Time.time >= blockNextFire)
        {
            if(!blocking)
            {
                blocking = true;
                PlayerDefend();
            }
        }
        else if(blocking)
        {
            blocking = false;
            blockNextFire = Time.time + blockFireRate;
            PlayerDefend();
        }
    }

    public override void MeleeAttack()
    {
        meleeNextFire = Time.time + meleeFireRate;
        switch (gameObject.GetComponent<PlayerMovement>().grounded)
        {
            case true:
                //Put melee attacks on the ground here.
                newGroundMelee = Instantiate(meleeObject, groundMeleeGun.position, groundMeleeGun.rotation);
                newGroundMelee.transform.parent = playerWeaponParent.transform;
                newGroundMelee.GetComponent<PlayerMelee>().player = gameObject;
                newGroundMelee.GetComponent<PlayerMelee>().myGun = groundMeleeGun;
                SetBasicMeleeAttackStats(newGroundMelee);
                if (groundMeleeGunTwo != null)
                {
                    newGroundMelee = Instantiate(meleeObject, groundMeleeGunTwo.position, groundMeleeGunTwo.rotation);
                    newGroundMelee.transform.parent = playerWeaponParent.transform;
                    newGroundMelee.GetComponent<PlayerMelee>().player = gameObject;
                    newGroundMelee.GetComponent<PlayerMelee>().myGun = groundMeleeGunTwo;
                    SetBasicMeleeAttackStats(newGroundMelee);
                }
                break;
            default:
                //Do Air melee attack stuff here. 
                newAirMelee = Instantiate(meleeObject, airMeleeGun.position, airMeleeGun.rotation);
                newAirMelee.transform.parent = playerWeaponParent.transform;
                newAirMelee.GetComponent<PlayerMelee>().player = gameObject;
                newAirMelee.GetComponent<PlayerMelee>().myGun = airMeleeGun;
                SetBasicMeleeAttackStats(newAirMelee);
                if (airMeleeGunTwo != null)
                {
                    newAirMelee = Instantiate(meleeObject, airMeleeGunTwo.position, airMeleeGunTwo.rotation);
                    newAirMelee.transform.parent = playerWeaponParent.transform;
                    newAirMelee.GetComponent<PlayerMelee>().player = gameObject;
                    newAirMelee.GetComponent<PlayerMelee>().myGun = airMeleeGunTwo;
                    SetBasicMeleeAttackStats(newAirMelee);
                }
                break;
        }

        if (blocking == true)
        {
            blocking = false;
            blockNextFire = Time.time + blockFireRate;
        }
    }

    public override void RangedAttack()
    {
        projectileNextFire = Time.time + projectileFireRate;
        //Shoots the projectile, put the projectile movement code on that object.
        switch (gameObject.GetComponent<PlayerMovement>().grounded)
        {
            case true:
                if (groundProjectile.name == "Player Projectile Lobbed")
                {
                    GameObject newGroundProjectile = Instantiate(groundProjectile, groundGun.position, groundGun.rotation);
                    newGroundProjectile.transform.parent = playerWeaponParent.transform;
                    if (transform.rotation.y > 0 && (lobbedForce.x > 0))
                    {
                        lobbedForce.x = lobbedForce.x * -1;
                    }
                    else if (transform.rotation.y <= 0 && (lobbedForce.x < 0))
                    {
                        lobbedForce.x = lobbedForce.x * -1;
                    }
                    newGroundProjectile.GetComponent<Rigidbody>().AddForce(lobbedForce, ForceMode.Impulse);
                }
                else
                {
                    //Set up a gun position object on each player.
                    GameObject newGroundProjectile = Instantiate(groundProjectile, groundGun.position, groundGun.rotation);
                    newGroundProjectile.transform.parent = playerWeaponParent.transform;
                    newGroundProjectile.GetComponent<PlayerProjectile>().player = gameObject;
                    SetBasicRangedAttackStats(newGroundProjectile);
                    if (groundGunTwo != null)
                    {
                        newGroundProjectile = Instantiate(groundProjectile, groundGunTwo.position, groundGunTwo.rotation);
                        newGroundProjectile.transform.parent = playerWeaponParent.transform;
                        newGroundProjectile.GetComponent<PlayerProjectile>().player = gameObject;
                        SetBasicRangedAttackStats(newGroundProjectile);
                    }

                }
                //Set up a gun position object on each player.
                break;
            default:
                GameObject newAirProjectile = Instantiate(airProjectile, airGun.position, airGun.rotation);
                newAirProjectile.transform.parent = playerWeaponParent.transform;
                newAirProjectile.GetComponent<PlayerProjectile>().player = gameObject;
                SetBasicRangedAttackStats(newAirProjectile);
                if (airGunTwo != null)
                {
                    newAirProjectile = Instantiate(airProjectile, airGunTwo.position, airGunTwo.rotation);
                    newAirProjectile.transform.parent = playerWeaponParent.transform;
                    newAirProjectile.GetComponent<PlayerProjectile>().player = gameObject;
                    SetBasicRangedAttackStats(newAirProjectile);
                }
                break;
        }

        if (blocking == true)
        {
            blocking = false;
            blockNextFire = Time.time + blockFireRate;
        }
    }

    public override void PlayerDefend()
    {
        GameObject newBlockEffect = null;
        //Puts the player into a defensive stance that reduces incoming damage by X amount. (Do we apply that before or after the elemental resist?)
        if(blocking)
        {
            newBlockEffect = Instantiate(blockEffect, transform.position, transform.rotation);
            newBlockEffect.transform.parent = gameObject.transform;
        }
    }

    public override void JumpAttack()
    {
        //Set Jump attack force at the moment of jump
        rb.velocity = new Vector2(rb.velocity.x, 0.0f);
        // Arial Jump
        Vector2 arialJump = new Vector2();
        arialJump.y = gameObject.GetComponent<PlayerMovement>().arialJumpForce;
        rb.AddForce(arialJump, ForceMode2D.Impulse);
        gameObject.GetComponent<PlayerMovement>().bounceJumpsUsed++;
        //Debug.Log("Bounce Jumps Used: " + gameObject.GetComponent<PlayerMovement>().bounceJumpsUsed);

        //Attacking below as you jump.
        GameObject newJumpMelee = Instantiate(meleeObject, jumpMeleeGun.position, jumpMeleeGun.rotation);
        newJumpMelee.transform.parent = playerWeaponParent.transform;
        newJumpMelee.GetComponent<PlayerMelee>().player = gameObject;
        newJumpMelee.GetComponent<PlayerMelee>().myGun = jumpMeleeGun;
        SetBasicMeleeAttackStats(newJumpMelee);
        //Debug.Log("jump attack: "+ newJumpMelee);
    }

    public override void SpecialMelee()
    {

    }

    public override void SpecialRanged()
    {

    }

    public override void CallWisp()
    {
        //Moves target location to the player
        myWispTargetLocation.position = gameObject.transform.position;

        //Tells the Wisp to move to the targeted location
        wisp.GetComponent<Wisp>().targetLocation = myWispTargetLocation;
        wisp.GetComponent<Wisp>().moving = true;
        //callingWisp = false;
    }

    private void SetBasicMeleeAttackStats(GameObject melee)
    {
        melee.GetComponent<PlayerMelee>().meleeHitBoxLife = meleeHitBoxLife;
        melee.GetComponent<PlayerMelee>().meleeDamage = meleeDamage;
        melee.GetComponent<PlayerMelee>().stunLockOut = meleeHitStun;
        melee.GetComponent<PlayerMelee>().knockBack = meleeKnockBack;
    }

    private void SetBasicRangedAttackStats(GameObject projectile)
    {
        projectile.GetComponent<PlayerProjectile>().projectileSpeed = projectileSpeed;
        projectile.GetComponent<PlayerProjectile>().projectileDamage = projectileDamage;
        projectile.GetComponent<PlayerProjectile>().projectileHitStun = projectileHitStun;
        projectile.GetComponent<PlayerProjectile>().projectileMaxDuration = projectileMaxDuration;
        projectile.GetComponent<PlayerProjectile>().projectileBreakChance = projectileBreakChance;
        projectile.GetComponent<PlayerProjectile>().usesConstantForceProjectile = usesConstantForceProjectile;
        projectile.GetComponent<PlayerProjectile>().lobbedForce = lobbedForce;
        projectile.GetComponent<PlayerProjectile>().breaksHittingWall = breaksHittingWall;
        projectile.GetComponent<PlayerProjectile>().throwWaitTime = throwWaitTime;

    }
}
