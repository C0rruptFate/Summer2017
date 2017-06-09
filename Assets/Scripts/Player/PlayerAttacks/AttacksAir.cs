using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttacksAir : PlayerAttacks {

    // Use this for initialization
    void Start()
    {

        //Plugs myself into my PlayerHealth and Player movement scripts.
        // [TODO] CHANGE THESE GETCOMPONENTS FOR EACH ELEMENTAL ATTACK SCRIPT.
        GetComponent<PlayerHealth>().playerAttacks = GetComponent<AttacksAir>();
        GetComponent<PlayerMovement>().playerAttacks = GetComponent<AttacksAir>();
        //Sets my player # so I know what controller to look at.
        playerNumber = playerHealth.playerNumber;
        //Sets up my rigid body.
        rb = GetComponent<Rigidbody2D>();
        //Finds the wisp target location object, changes it's name, and removes it as a child.
        myWispTargetLocation = transform.Find("Wisp Target Location");
        myWispTargetLocation.name = gameObject.name + "Wisp Target Location";
        myWispTargetLocation.parent = null;
        //Find the wisp object
        wisp = GameObject.Find("Wisp");
        if (wisp == null)
        {//If the wisp can't be found it will inform the designer.
            Debug.LogError("Can't find the Wisp, it might not be added to the scene.");
        }

        //Set's up the player's weapon parent object
        playerWeaponParent = GameObject.Find("Player Attacks");
        if (!playerWeaponParent)//If it can't find the weapon parent it will create one (the first player on each level should create this automatically).
        {
            playerWeaponParent = new GameObject("Player Attacks");
        }

        //Melee attack Setup
        #region
        //Sets up the grounded melee attack for your first weapon
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
            //If the this object is missing it will inform the designer.
            Debug.LogError("Can't find the PRIMARY GROUND MELEE gun for " + gameObject);
        }

        //Sets up the grounded melee attack for your second weapon if you have one.
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
            case AttackFromLocation.Empty://Second could be left empty.
            default:
                groundMeleeGunTwo = null;
                break;
        }
        if (groundMeleeGunTwo == null)
        {
            //If the this object is missing it will inform the designer.
            Debug.Log("Can't find the SECONDARY GROUND MELEE gun for " + gameObject);
        }
        //Sets up the aerial melee attack for your first weapon.
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
            //If the this object is missing it will inform the designer.
            Debug.LogError("Can't find the PRIMARY AIR MELEE gun for " + gameObject);
        }
        //Sets up the aerial melee attack for your secondary weapon.
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
            case AttackFromLocation.Empty://Second could be left empty.
            default:
                airMeleeGunTwo = null;
                break;
        }
        if (airMeleeGunTwo == null)
        {
            //If the this object is missing it will inform the designer.
            Debug.Log("Can't find the SECONDARY AIR MELEE gun for player " + gameObject);
        }
        //Jump attack Setup this will always be the gun below you.
        jumpMeleeGun = transform.Find("Below Gun");
        #endregion

        //Ranged attack Setup
        #region 
        //Sets up the grounded projectile for your first weapon
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
            //If the this object is missing it will inform the designer.
            Debug.LogError("No GROUND PROJECTILE selected on PRIMARY gun for " + gameObject);
        }
        //Sets up the grounded projectile for your secondary weapon.
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
            //If the this object is missing it will inform the designer.
            Debug.Log("No GROUND PROJECTILE selected on SECONDARY gun for " + gameObject);
        }

        //Sets up the aerial projectile for your primary weapon.
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
            //If the this object is missing it will inform the designer.
            Debug.LogError("No air projectile selected on PRIMARY gun for " + gameObject);
        }
        //Sets up the aerial projectile for your secondary weapon.
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
            //If the this object is missing it will inform the designer.
            Debug.Log("No air projectile selected on SECONDARY gun for " + gameObject);
        }
        #endregion
    }

    // Update is called once per frame
    void Update()
    {

        //Calls Wisp and ticks up how long it has been held down. When greater than 20 the Wisp will attach to you.
        //Debug.Log("Callwisp" + Input.GetAxisRaw("CallWisp" + playerNumber));
        if (Input.GetAxisRaw("CallWisp" + playerNumber) > 0.25f)
        {
            if (callingWispTime < 20)
            {
                callingWispTime++;
            }
            CallWisp();
            callingWisp = true;
        }//Stops calling the Wisp when the button isn't held down.
        else if (Input.GetAxisRaw("CallWisp" + playerNumber) <= 0.25f)
        {
            callingWispTime = 0;
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
        }
        if (Input.GetAxisRaw("Special" + playerNumber) != 1 && specialActive)//Turns off the special when the button/trigger is released.
        {
            //Turn off special
            //Debug.Log("SpeciaL has been DEACTIVATED");
            specialActive = false;
        }

        //Melee attacks
        //[TODO ALSO REQUIRE MANA TO BE >=SPECIAL MELEE MANA COST]
        if (specialActive && playerHealth.mana >= specialMeleeManaCost && Input.GetButtonDown("Melee" + playerNumber) && Time.time > meleeNextFire)//Special Melee Attack
        {
            //Debug.Log("Melee Special is active.");
            SpecialMeleeAttack();
        }
        else if (Input.GetButtonDown("Melee" + playerNumber) && Time.time > meleeNextFire)//Melee Attack
        {
            MeleeAttack();
        }

        //Ranged Attacks
        //[TODO ALSO REQUIRE MANA TO BE >=SPECIAL MELEE MANA COST]
        if (specialActive && playerHealth.mana >= specialRangedManaCost && Input.GetButtonDown("Ranged" + playerNumber) && Time.time > projectileNextFire)//Special Ranged Attack
        {
            //Debug.Log("Ranged Special is active.");
            SpecialRangedAttack();
        }
        else if (Input.GetButtonDown("Ranged" + playerNumber) && Time.time > projectileNextFire)//Ranged Attack
        {
            RangedAttack();
        }

        //Defend
        //[TODO ALSO REQUIRE MANA TO BE >=SPECIAL MELEE MANA COST]
        if (specialActive && playerHealth.mana >= specialDefendManaCost && Input.GetButton("Defend" + playerNumber) && Time.time >= blockNextFire)//Special Block
        {
            Debug.Log("Defend Special is active.");
            if (!blocking)
            {
                blocking = true;
                SpecialPlayerDefend();
            }
        }
        else if (Input.GetButton("Defend" + playerNumber) && Time.time >= blockNextFire)//Block
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
    }

    protected override void SpecialMeleeAttack()
    {
        base.SpecialMeleeAttack();

        //[TODO] Set up special melee attack for each character.
    }

    protected override void SpecialRangedAttack()
    {
        base.SpecialRangedAttack();

        //[TODO] Set up the special ranged attack for each character.

    }

    public override void SpecialPlayerDefend()
    {
        //Spends the mana to use your special ranged attack.
        playerHealth.SpendMana(specialDefendManaCost);

        //[TODO] Set up the special Defend for each character.

    }
}
