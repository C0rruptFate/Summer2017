using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    public virtual void PlayerAttackType()
    {

    }
}

public class AttacksFire : PlayerAttacks {

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
    [HideInInspector]
    public GameObject newGroundMelee;
    [HideInInspector]
    public GameObject newAirMelee;
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
    public Vector3 lobbedForce = new Vector3(7, 7, 0);

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

    [Header("Block Settings")]
    #region
    public float blockFireRate = 0.5f;
    public float blockingResistanceModifier = 15f;
    [Tooltip("Attach a gameObject that will be spawned when you are blocking.")]
    public GameObject blockEffect; //attach a game object that will be spawned when you ar blocking.
    #endregion

    //Private Attack extras
    #region
    [HideInInspector]public Element element; //element of this player
    private GameObject playerParent;
    private Transform groundGun;
    private Transform groundGunTwo;
    private Transform airGun;
    private Transform airGunTwo;
    private Transform groundMeleeGun;
    private Transform groundMeleeGunTwo;
    private Transform airMeleeGun;
    private Transform airMeleeGunTwo;
    private GameObject playerWeaponParent;
    private float projectileNextFire = 0.0f;
    private float meleeNextFire = 0.0f;
    private float blockNextFire = 0.0f;
    private Transform jumpMeleeGun;

    //Jump Attack
    private Rigidbody2D rb;
    private float arialJumpForce;
    //private Component playerMovement;
    #endregion

    // Use this for initialization
    void Start () {
        //GetComponent<PlayerHealth>().attackScript = this.ToString();
        GetComponent<PlayerHealth>().playerAttacks = GetComponent<AttacksFire>();
        GetComponent<PlayerMovement>().playerAttacks = GetComponent<AttacksFire>();
        rb = GetComponent<Rigidbody2D>();
        //playerMovement = GetComponent<PlayerMovement>();
        //Change for each element attack type.
        //GetComponent<PlayerMovement>().attackScript = GetComponent<AttacksFire>();

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
                groundMeleeGun = transform.Find("Horizontal Gun");
                break;
            case AttackFromLocation.Empty:
            default:
                groundMeleeGun = null;
                break;
        }
        if (groundMeleeGunTwo == null)
        {
            Debug.LogError("Can't find the SECONDARY GROUND MELEE gun for " + gameObject);
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
            Debug.LogError("Can't find the SECONDARY AIR MELEE gun for player " + gameObject);
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
            Debug.LogError("No air projectile selected on SECONDARY gun for " + gameObject);
        }

        //projectileParent = GameObject.Find("Projectiles");

        //if (!projectileParent)
        //{
        //    projectileParent = new GameObject("Projectiles");
        //}
        #endregion
    }

    // Update is called once per frame
    void Update () {
        //Melee Attack
        if (Input.GetButtonDown("Fire1") && Time.time > meleeNextFire)
        {
            //gameObject.GetComponent<PlayerMovement>().blocking = false;
            Debug.Log("blocking is: " + gameObject.GetComponent<PlayerMovement>().blocking);
            MeleeAttack();
        }
        //Ranged Attack
        if (Input.GetButtonDown("Fire2") && Time.time > projectileNextFire)
        {
            //gameObject.GetComponent<PlayerMovement>().blocking = false;
            RangedAttack();
        }

        //Defend
        if (Input.GetButton("Fire3") && Time.time >= blockNextFire)
        {
            if (!gameObject.GetComponent<PlayerMovement>().blocking)
            {
                gameObject.GetComponent<PlayerMovement>().blocking = true;
                PlayerDefend();
            }
        }
        else if (gameObject.GetComponent<PlayerMovement>().blocking)
        {
            gameObject.GetComponent<PlayerMovement>().blocking = false;
            blockNextFire = Time.time + blockFireRate;
            PlayerDefend();
        }
        //Remove Defend when jumping
        //if (Input.GetButtonDown("Jump"))
        //{
        //    if (gameObject.GetComponent<PlayerMovement>().blocking == true)
        //    {
        //        gameObject.GetComponent<PlayerMovement>().blocking = false;
        //        blockNextFire = Time.time + blockFireRate;
        //    }
        //    if(gameObject.GetComponent<PlayerMovement>().enemyBelow || gameObject.GetComponent<PlayerMovement>().playerBelow)
        //    {
        //        JumpAttack();
        //    } 
        //}
    }

    private void MeleeAttack()
    {
        meleeNextFire = Time.time + meleeFireRate;
        switch (gameObject.GetComponent<PlayerMovement>().grounded)
        {
            //Constants.WhatsBelowMe(gameObject)
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
            //case "Enemy":
            //    GameObject newAirMelee = Instantiate(meleeObject, airMeleeGun.position, airMeleeGun.rotation);
            //    newAirMelee.transform.parent = transform;
            //    newAirMelee.GetComponent<PlayerMelee>().player = gameObject;

            //    if (airMeleeGunTwo != null)
            //    {

            //    }
            //    break;
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

        if (gameObject.GetComponent<PlayerMovement>().blocking == true)
        {
            gameObject.GetComponent<PlayerMovement>().blocking = false;
            blockNextFire = Time.time + blockFireRate;
        }
    }

    private void RangedAttack()
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

        if (gameObject.GetComponent<PlayerMovement>().blocking == true)
        {
            gameObject.GetComponent<PlayerMovement>().blocking = false;
            blockNextFire = Time.time + blockFireRate;
        }
    }

    private void PlayerDefend()
    {
        GameObject newBlockEffect = null;
        //Puts the player into a defensive stance that reduces incoming damage by X amount. (Do we apply that before or after the elemental resist?)
        if (gameObject.GetComponent<PlayerMovement>().blocking)
        {
            newBlockEffect = Instantiate(blockEffect, transform.position, transform.rotation);
            newBlockEffect.transform.parent = gameObject.transform;
        }
    }

    public void JumpAttack()
    {
        //Set Jump attack force at the moment of jump
        arialJumpForce = gameObject.GetComponent<PlayerMovement>().arialJumpForce;

        rb.velocity = new Vector2(rb.velocity.x, 0.0f);
        // Arial Jump
        Vector2 arialJump = new Vector2();
        arialJump.y = arialJumpForce;
        rb.AddForce(arialJump, ForceMode2D.Impulse);
        gameObject.GetComponent<PlayerMovement>().bounceJumpsUsed++;
        //Attacking below as you jump.
        GameObject newJumpMelee = Instantiate(meleeObject, jumpMeleeGun.position, jumpMeleeGun.rotation);
        newJumpMelee.transform.parent = playerWeaponParent.transform;
        newJumpMelee.GetComponent<PlayerMelee>().player = gameObject;
        newJumpMelee.GetComponent<PlayerMelee>().myGun = jumpMeleeGun;
        Debug.Log("Jump attack used " + gameObject.GetComponent<PlayerMovement>().bounceJumpsUsed);
    }

    private void SpecialMelee()
    {

    }

    private void SpecialRanged()
    {

    }

    private void SetBasicMeleeAttackStats(GameObject melee)
    {
        melee.GetComponent<PlayerMelee>().meleeHitBoxLife = meleeHitBoxLife;
        melee.GetComponent<PlayerMelee>().meleeDamage = meleeDamage;
        melee.GetComponent<PlayerMelee>().stunLockOut = meleeHitStun;
    }

    private void SetBasicRangedAttackStats(GameObject projectile)
    {
        projectile.GetComponent<PlayerProjectile>().projectileSpeed = projectileSpeed;
        projectile.GetComponent<PlayerProjectile>().projectileDamage = projectileDamage;
        projectile.GetComponent<PlayerProjectile>().projectileHitStun = projectileHitStun;
        projectile.GetComponent<PlayerProjectile>().projectileMaxDuration = projectileMaxDuration;
        projectile.GetComponent<PlayerProjectile>().projectileBreakChance = projectileBreakChance;
        projectile.GetComponent<PlayerProjectile>().usesConstantForceProjectile = usesConstantForceProjectile;
        projectile.GetComponent<PlayerProjectile>().breaksHittingWall = breaksHittingWall;
    }

    private void ConvertStringToType(string inputString)
    {
        return System.Activator.CreateInstance(Types.GetType(inputString));
    }
}
