using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour {

    //Look at https://docs.unity3d.com/ScriptReference/EditorGUILayout.Foldout.html to see about collapsable menues for the inspector also the player inspector script 

    [Header("Melee Attack Settings")]
    #region

    [Tooltip("Drag in this character's melee prefab")]
    public GameObject meleeObject;
    [Tooltip("How much time must take place between melee attacks")]
    public float meleeFireRate = 0.5f;
    [Tooltip("Where does my melee attack come from when I am on the ground?")]
    public AttackFromLocation groundMeleeType;
    [Tooltip("Do I have a secondary ground melee attack location? Where does my melee attack come from when I am on the ground?")]
    public AttackFromLocation groundMeleeTwoType;
    [Tooltip("Where does my melee attack come from when I am in the air?")]
    public AttackFromLocation airMeleeType;
    [Tooltip("Do I have a secondary air melee attack location? Where does my melee attack come from when I am in the air?")]
    public AttackFromLocation airMeleeTwoType;

    [Header("Melee Weapon Settings")]

    [Tooltip("How Long the hitbox stays out. 'Keep very small'")]
    public float meleeHitBoxLife = 0.1f;
    [Tooltip("How much damge this object deals.")]
    public float meleeDamage = 10f;
    [Tooltip("How long it will stop the enemy from moving or attacking.")]
    public float meleeHitStun = 0.5f;
    [Tooltip("knockback force.")]
    public float meleeKnockBack = 500f;
    [Tooltip("Attach the effect you want to play when I hit an enemy with my basic melee attack.")]
    public GameObject meleeEffectParticle;
    [HideInInspector]//The ground Melee attack that is created when the player does a melee attack on the ground.
    public GameObject newGroundMelee;
    [HideInInspector]//The air melee attack that is created when the player does a melee attack in the air.
    public GameObject newAirMelee;

    //Jump Attack
    [HideInInspector]//My rigidbody
    public Rigidbody2D rb;
    [HideInInspector]//The jump force of my jump attack, this is pulled from my playermovement script and maches the airal jump.
    public float arialJumpForce;
    #endregion

    [Header("Ranged Attack Settings")]
    #region
    [Tooltip("Drag in this character's grounded projectile prefab")]
    public GameObject groundProjectile;
    [Tooltip("Drag in this character's arial projectile prefab")]
    public GameObject airProjectile;
    [Tooltip("How much time must take place between ranged attacks")]
    public float projectileFireRate = 0.5f;
    [Tooltip("Where does my ground projectile come from?")]
    public AttackFromLocation groundProjectileType;
    [Tooltip("Do I have a second GROUND projectile location. Where does my second ground projectile come from?")]
    public AttackFromLocation groundProjectileTwoType;
    [Tooltip("Where does my air projectile come from?")]
    public AttackFromLocation airProjectileType;
    [Tooltip("Do I have a second AIR projectile location. Where does my second air projectile come from?")]
    public AttackFromLocation airProjectileTwoType;
    [Tooltip("What force is applied if I use a lobbed projectile. 'This is only applied if a lobbed projectile is used'.")]
    public Vector2 lobbedForce = new Vector2(15, 15);
    [Tooltip("How long before the lobbed projectile is fired.")]
    public float throwWaitTime;

    [Header("Projectile Settings")]
    [Tooltip("How fast the projectiles you shoot move?")]
    public float projectileSpeed = 10;
    [Tooltip("How much damage does my projectile do?")]
    public float projectileDamage = 5;
    [Tooltip("How long does my projectile stun for if it hits an enemey?")]
    public float projectileHitStun = 0.25f;
    [Tooltip("How long does my projectile last before vanishing?")]
    public float projectileMaxDuration = 10f;
    [Tooltip("What is the chance that this will break when hitting another object?")]
    public float projectileBreakChance = 50f;
    [Tooltip("Does this projectile fly forward constantly?")]
    public bool usesConstantForceProjectile = true;
    [Tooltip("Does this break right as it hits a wall? 'Use for standard projectiles, lobbed that roll want this off.'")]
    public bool breaksHittingWall = true;
    #endregion

    [Header("Defend Settings")]
    #region
    [Tooltip("How often I need to wait between blocks.")]
    public float blockFireRate = 0.5f;
    [Tooltip("What percentage of damage does blocking prevent.")]
    public float blockingResistanceModifier = 15f;
    [HideInInspector]
    public bool blocking = false;//Am I currently blocking.
    [Tooltip("Attach a gameObject that will be spawned when you are blocking.")]
    public GameObject blockEffect; //attach a game object/effect that will be spawned when I am blocking.
    #endregion

    [Header("Special Attack Settings")]
    #region
    [Tooltip("Attach a gameObject/effect that will play when my special is active.")]
    public GameObject specialActiveEffect;
    [HideInInspector]//Do I currently have my special button held down?
    public bool specialActive = false;

    [Header("Special Melee Attack Settings")]
    [Tooltip("Attach a gameObject of what my melee attack will be.")]
    public GameObject specialMeleeAttackObject;
    [Tooltip("Damage done by my special Melee attack.")]
    public float specialMeleeDamage;
    [Tooltip("Cooldown for Special Melee Attacks.")]
    public float specialMeleeCooldown;
    [Tooltip("Check if you want the special melee to fire from both the primary and secondary guns.")]
    public bool useMeleeSecondaryGunSpecial = false;
    [HideInInspector]//Current cool down of this ability.
    public float currentSpecialMeleeCooldown;

    [Header("Special Ranged Attack Settings")]
    [Tooltip("Attach a gameObject of what my melee attack will be.")]
    public GameObject specialRangedAttackObject;
    [Tooltip("Damage done by my special ranged attack.")]
    public float specialRangedDamage;
    [Tooltip("Hitstun duration of my special ranged attack.")]
    public float specialRangedHitStun;
    [Tooltip("Speed of my special projectile.")]
    public float specialProjectileSpeed;
    [Tooltip("Max Duration of my special projectile.")]
    public float specialProjectileMaxDuration;
    [Tooltip("Break chance of my special projectile.")]
    public float specialProjectileBreakChance;
    [Tooltip("Does this break right as it hits a wall? 'Use for standard projectiles, lobbed that roll want this off.'")]
    public bool specialBreaksHittingWall = true;
    [Tooltip("Check if you want the special range to fire from both the primary and secondary guns.")]
    public bool useRangedSecondaryGunSpecial = false;
    [Tooltip("Cooldown for Special Ranged Attacks.")]
    public float specialRangedCooldown;
    [HideInInspector]//Current cool down of this ability.
    public float currentSpecialRangedCooldown;

    [Header("Special Defend Settings")]
    [Tooltip("Attach a gameObject of what my Special Defend will be.")]
    public GameObject specialDefendObject;
    [Tooltip("Cooldown for Special Defend Attacks.")]
    public float specialDefendCooldown;
    [HideInInspector]//Current cool down of this ability.
    public float currentSpecialDefendCooldown;

    [HideInInspector]
    public bool specialBlocking;
    #endregion

    //Wisp Settings 
    #region
    [HideInInspector]//The transform that I will be calling the wisp to.
    public Transform myWispTargetLocation;
    [HideInInspector]//The Wisp that I will be telling where to go.
    public GameObject wisp;
    [HideInInspector]//Am I currently calling the Wisp?
    public bool callingWisp = false;
    #endregion

    //Private Attack extras
    #region
    [HideInInspector]//element of this player
    public Element element; 
    [HideInInspector]//Who I am parented to, normally empty.
    protected GameObject playerParent;
    [HideInInspector]//Where my ground projectiles will be shot from.
    protected Transform groundGun;
    [HideInInspector]//Where my second ground projectile will be shot from if I shot a second.
    protected Transform groundGunTwo;
    [HideInInspector]//Where my air projectiles will be shot from.
    protected Transform airGun;
    [HideInInspector]//Where my second air projectile will be shot from if I shot a second.
    protected Transform airGunTwo;
    [HideInInspector]//Where my ground melee attack will be when I melee attack on the ground.
    protected Transform groundMeleeGun;
    [HideInInspector]//Where my second ground melee attack will be when I melee attack on the ground.
    protected Transform groundMeleeGunTwo;
    [HideInInspector]//Where my air melee attacks wil be when I melee in the air.
    protected Transform airMeleeGun;
    [HideInInspector]//Where my second air melee attacks wil be when I melee in the air.
    protected Transform airMeleeGunTwo;
    [HideInInspector]//Empty object that holds all of my attacks.
    public GameObject playerWeaponParent;
    [HideInInspector]//Sets up when I can next shoot a projectile.
    protected float projectileNextFire = 0.0f;
    [HideInInspector]//Sets up when I can next melee attack.
    protected float meleeNextFire = 0.0f;
    [HideInInspector]//Sets up when I can next block.
    public float blockNextFire = 0.0f;
    [HideInInspector]//Will always be below me.
    protected Transform jumpMeleeGun;

    //Scripts and player setup
    [HideInInspector]//What player # is controlling me.
    public int playerNumber = 1;
    [HideInInspector]//My HP script
    public PlayerHealth playerHealth;
    [HideInInspector]//My Movement script
    public PlayerMovement playerMovement;

    protected IEnumerator meleeCooldownCoroutine;
    protected IEnumerator rangedCooldownCoroutine;
    protected IEnumerator defendCooldownCoroutine;
    #endregion

    //Animation
    protected Animator anim;

    public virtual void Start()
    {
        meleeCooldownCoroutine = MeleeCooldown();
        rangedCooldownCoroutine = RangedCooldown();
        defendCooldownCoroutine = DefendCooldown();

        //Sets my player # so I know what controller to look at.
        playerNumber = playerHealth.playerNumber;
        //Debug.Log("element " + element + "player Number " + playerNumber);
        //Sets up my rigid body and animator
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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

    public virtual void Update()
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
        }else if (currentSpecialRangedCooldown <= 0)
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

    public virtual void CallWisp()
    {
        //Moves target location to the player
        myWispTargetLocation.position = gameObject.transform.position;

        //Tells the Wisp to move to the targeted location
        wisp.GetComponent<Wisp>().targetLocation = myWispTargetLocation;
        wisp.GetComponent<Wisp>().moving = true;
        //callingWisp = false;
    }

    //Player Basic Melee attacks
    public virtual void MeleeAttack()
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
                    newAirMelee = Instantiate(meleeObject, airMeleeGunTwo.position, airMeleeGunTwo.rotation);
                    newAirMelee.transform.parent = playerWeaponParent.transform;
                    newAirMelee.GetComponent<PlayerMelee>().player = gameObject;
                    newAirMelee.GetComponent<PlayerMelee>().myGun = airMeleeGunTwo;
                    SetBasicMeleeAttackStats(newAirMelee);
                }
                break;
        }

        //If I am blocking attacking will pull me out of it.
        if (blocking == true)
        {
            blocking = false;
            blockNextFire = Time.time + blockFireRate;
        }
    }

    //Player Basic Ranged Attacks
    public virtual void RangedAttack()
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

        if (blocking == true)//Pulls me out of blocking when I shot if I was blocking.
        {
            blocking = false;
            blockNextFire = Time.time + blockFireRate;
        }
    }

    //The Player Basic Defense
    public virtual void PlayerDefend()
    {
        //creates a empty blocking game object so that I can destroy it when not blocking anymore.
        GameObject newBlockEffect = null;
        //Puts the player into a defensive stance that reduces incoming damage by X amount. (Do we apply that before or after the elemental resist?)
        if (blocking)
        {
            newBlockEffect = Instantiate(blockEffect, transform.position, transform.rotation);
            newBlockEffect.GetComponent<DestroyBlocking>().player = gameObject;
            newBlockEffect.transform.parent = playerWeaponParent.transform;
            //newBlockEffect.transform.parent = gameObject.transform;
        }
    }

    //When the Player jumps off an enemy
    public virtual void JumpAttack()
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

    //Special Melee Attacks, these will be different for each character.
    protected virtual void SpecialMeleeAttack()
    {
        currentSpecialMeleeCooldown = specialMeleeCooldown;
        meleeNextFire = Time.time + meleeFireRate; //Decides when preform another melee attack.
        //Checks if I am grounded. Creates the melee object at my gun location, parents it to the weapons gameobject, and sets the weapon's location to the player's gun.
        switch (gameObject.GetComponent<PlayerMovement>().grounded)
        {
            case true:
                //Put melee attacks on the ground here.
                newGroundMelee = Instantiate(specialMeleeAttackObject, groundMeleeGun.position, groundMeleeGun.rotation);
                newGroundMelee.transform.parent = playerWeaponParent.transform;
                newGroundMelee.GetComponent<PlayerMelee>().player = gameObject;
                newGroundMelee.GetComponent<PlayerMelee>().myGun = groundMeleeGun;
                SetSpecialMeleeAttackStats(newGroundMelee);
                if (groundMeleeGunTwo != null && useMeleeSecondaryGunSpecial)//Does the same for the 2nd grounded melee attack, if I have one.
                {
                    newGroundMelee = Instantiate(specialMeleeAttackObject, groundMeleeGunTwo.position, groundMeleeGunTwo.rotation);
                    newGroundMelee.transform.parent = playerWeaponParent.transform;
                    newGroundMelee.GetComponent<PlayerMelee>().player = gameObject;
                    newGroundMelee.GetComponent<PlayerMelee>().myGun = groundMeleeGunTwo;
                    SetSpecialMeleeAttackStats(newGroundMelee);
                }
                break;
            default://If I am not grounded. Creates the melee object at my gun location, parents it to the weapons gameobject, and sets the weapon's location to the player's gun.
                //Do Air melee attack stuff here. 
                newAirMelee = Instantiate(specialMeleeAttackObject, airMeleeGun.position, airMeleeGun.rotation);
                newAirMelee.transform.parent = playerWeaponParent.transform;
                newAirMelee.GetComponent<PlayerMelee>().player = gameObject;
                newAirMelee.GetComponent<PlayerMelee>().myGun = airMeleeGun;
                SetSpecialMeleeAttackStats(newAirMelee);
                if (airMeleeGunTwo != null && useMeleeSecondaryGunSpecial) //Does the same for the 2nd aerial, if I have one.
                {
                    newAirMelee = Instantiate(specialMeleeAttackObject, airMeleeGunTwo.position, airMeleeGunTwo.rotation);
                    newAirMelee.transform.parent = playerWeaponParent.transform;
                    newAirMelee.GetComponent<PlayerMelee>().player = gameObject;
                    newAirMelee.GetComponent<PlayerMelee>().myGun = airMeleeGunTwo;
                    SetSpecialMeleeAttackStats(newAirMelee);
                }
                break;
        }
    }

    //Special Ranged Attacks, these will be different for each character.
    protected virtual void SpecialRangedAttack()
    {
        //Spends the mana to use your special ranged attack.
        //playerHealth.SpendMana(specialRangedManaCost);

        currentSpecialRangedCooldown = specialRangedCooldown; //Sets the cooldown to 0
        //StartCoroutine(rangedCooldownCoroutine);
        projectileNextFire = Time.time + projectileFireRate; //Decides when preform another ranged attack.
        //Shoots the projectile, put the projectile movement code on that object.
        //Checks if I am grounded. Creates the ranged object at my gun location, parents it to the weapons gameobject, and sets the weapon's location to the player's gun.
        switch (gameObject.GetComponent<PlayerMovement>().grounded)
        {//Put ranged attacks on the ground here.
            case true://Checks if grounded or not/

                //Set up a gun position object on each player.
                GameObject newGroundProjectile = Instantiate(specialRangedAttackObject, groundGun.position, groundGun.rotation);
                newGroundProjectile.transform.parent = playerWeaponParent.transform;
                newGroundProjectile.GetComponent<PlayerProjectile>().player = gameObject;
                SetSpecialRangedAttackStats(newGroundProjectile);
                if (groundGunTwo != null && useRangedSecondaryGunSpecial)
                {
                    //Does the same thing for the secondary grounded projectile if one is set.
                    newGroundProjectile = Instantiate(specialRangedAttackObject, groundGunTwo.position, groundGunTwo.rotation);
                    newGroundProjectile.transform.parent = playerWeaponParent.transform;
                    newGroundProjectile.GetComponent<PlayerProjectile>().player = gameObject;
                    SetSpecialRangedAttackStats(newGroundProjectile);
                }
                break;
            default://Set up a aerial gun position object on each player.
                GameObject newAirProjectile = Instantiate(specialRangedAttackObject, airGun.position, airGun.rotation);
                newAirProjectile.transform.parent = playerWeaponParent.transform;
                newAirProjectile.GetComponent<PlayerProjectile>().player = gameObject;
                SetSpecialRangedAttackStats(newAirProjectile);
                if (airGunTwo != null && useRangedSecondaryGunSpecial)
                {//Does the same thing for the secondary if one is set.
                    newAirProjectile = Instantiate(specialRangedAttackObject, airGunTwo.position, airGunTwo.rotation);
                    newAirProjectile.transform.parent = playerWeaponParent.transform;
                    newAirProjectile.GetComponent<PlayerProjectile>().player = gameObject;
                    SetSpecialRangedAttackStats(newAirProjectile);
                }
                break;
        }

        if (blocking == true)//Pulls me out of blocking when I shot if I was blocking.
        {
            blocking = false;
            blockNextFire = Time.time + blockFireRate;
        }
    }

    //Special Defend, these will be different for each character.
    public virtual void SpecialPlayerDefend()
    {
        GameObject specialDefender = Instantiate(specialDefendObject, transform.position, transform.rotation);
        //specialDefender.GetComponent<DestroyBlocking>().player = gameObject;
        specialDefender.transform.parent = playerWeaponParent.transform;
        SetSpecialDefendStats(specialDefender);

    }

    public virtual void SetBasicMeleeAttackStats(GameObject melee) //Sets the stats for the melee object when it is created.
    {
        melee.GetComponent<PlayerMelee>().meleeHitBoxLife = meleeHitBoxLife;
        melee.GetComponent<PlayerMelee>().meleeDamage = meleeDamage;
        melee.GetComponent<PlayerMelee>().stunLockOut = meleeHitStun;
        melee.GetComponent<PlayerMelee>().knockBack = meleeKnockBack;
        melee.GetComponent<PlayerMelee>().effectParticle = meleeEffectParticle;
    }

    public virtual void SetBasicRangedAttackStats(GameObject projectile)//Sets the stats for the projectile object when it is created.
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

    public virtual void SetSpecialMeleeAttackStats(GameObject melee) //Sets the stats for the melee object when it is created.
    {
        melee.GetComponent<PlayerMelee>().meleeHitBoxLife = meleeHitBoxLife;
        melee.GetComponent<PlayerMelee>().meleeDamage = specialMeleeDamage;
        melee.GetComponent<PlayerMelee>().stunLockOut = meleeHitStun;
        melee.GetComponent<PlayerMelee>().knockBack = meleeKnockBack;
    }

    public virtual void SetSpecialRangedAttackStats(GameObject projectile)//Sets the stats for the projectile object when it is created.
    {
        projectile.GetComponent<PlayerProjectile>().projectileSpeed = specialProjectileSpeed;
        projectile.GetComponent<PlayerProjectile>().projectileDamage = specialRangedDamage;
        projectile.GetComponent<PlayerProjectile>().projectileHitStun = specialRangedHitStun;
        projectile.GetComponent<PlayerProjectile>().projectileMaxDuration = specialProjectileMaxDuration;
        projectile.GetComponent<PlayerProjectile>().projectileBreakChance = specialProjectileBreakChance;
        projectile.GetComponent<PlayerProjectile>().usesConstantForceProjectile = usesConstantForceProjectile;
        projectile.GetComponent<PlayerProjectile>().lobbedForce = lobbedForce;
        projectile.GetComponent<PlayerProjectile>().breaksHittingWall = specialBreaksHittingWall;
        projectile.GetComponent<PlayerProjectile>().throwWaitTime = throwWaitTime;
    }

    public virtual void SetSpecialDefendStats(GameObject defend)
    {
        //Special Defend life time.
        //Special Defend damage reduction.

    }

    public virtual IEnumerator MeleeCooldown()
    {
        while (currentSpecialMeleeCooldown > 0)
        {
            currentSpecialMeleeCooldown = currentSpecialMeleeCooldown - 0.5f;
            yield return new WaitForSeconds(0.5f);
            if (currentSpecialMeleeCooldown == 0)
            {
                StopCoroutine(meleeCooldownCoroutine);
            }
        }
    }

    public virtual IEnumerator RangedCooldown()
    {
        while (currentSpecialRangedCooldown > 0)
        {
            currentSpecialRangedCooldown = currentSpecialRangedCooldown -0.5f;
            yield return new WaitForSeconds(0.5f);
            if (currentSpecialRangedCooldown == 0)
            {
                StopCoroutine(rangedCooldownCoroutine);
            }
        }
    }

    public virtual IEnumerator DefendCooldown()
    {
        while (currentSpecialDefendCooldown > 0)
        {
            currentSpecialDefendCooldown = currentSpecialDefendCooldown - 0.5f;
            yield return new WaitForSeconds(0.5f);
            if (currentSpecialDefendCooldown == 0)
            {
                StopCoroutine(defendCooldownCoroutine);
            }
        }
    }
}
