using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour {

    //Look at https://docs.unity3d.com/ScriptReference/EditorGUILayout.Foldout.html to see about collapsable menues for the inspector

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
    [Tooltip("How long it will stop the enemy from moving or attacking.")]
    public float meleeKnockBack = 500f;
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

    [Header("Special Settings")]
    #region
    [Tooltip("Attach a gameObject/effect that will play when my special is active.")]
    public GameObject specialActiveEffect;
    [HideInInspector]//Do I currently have my special button held down?
    public bool specialActive = false;
    [Tooltip("Attach a gameObject of what my melee attack will be.")]
    public GameObject specialMeleeAttackObject;
    [Tooltip("How much mana it costs to use my Special Melee Attack.")]
    public int specialMeleeManaCost;

    [Tooltip("Attach a gameObject of what my melee attack will be.")]
    public GameObject specialRangedAttackObject;
    [Tooltip("How much mana it costs to use my Special Ranged Attack.")]
    public int specialRangedManaCost;

    [Tooltip("Attach a gameObject of what my Special Defend will be.")]
    public GameObject specialDefendObject;
    [Tooltip("How much mana it costs to use my Special Defend.")]
    public int specialDefendManaCost;
    #endregion

    //[Header("Wisp Settings")]
    #region
    [HideInInspector]//The transform that I will be calling the wisp to.
    public Transform myWispTargetLocation;
    [HideInInspector]//The Wisp that I will be telling where to go.
    public GameObject wisp;
    [HideInInspector]//Am I currently calling the Wisp?
    public bool callingWisp = false;
    [HideInInspector]//How long I have been holding down the button to attach the wisp to me.
    public int callingWispTime = 0;
    #endregion

    //Private Attack extras
    #region
    [HideInInspector]//element of this player
    public Element element; 
    [HideInInspector]//Who I am parented to, normally empty.
    public GameObject playerParent;
    [HideInInspector]//Where my ground projectiles will be shot from.
    public Transform groundGun;
    [HideInInspector]//Where my second ground projectile will be shot from if I shot a second.
    public Transform groundGunTwo;
    [HideInInspector]//Where my air projectiles will be shot from.
    public Transform airGun;
    [HideInInspector]//Where my second air projectile will be shot from if I shot a second.
    public Transform airGunTwo;
    [HideInInspector]//Where my ground melee attack will be when I melee attack on the ground.
    public Transform groundMeleeGun;
    [HideInInspector]//Where my second ground melee attack will be when I melee attack on the ground.
    public Transform groundMeleeGunTwo;
    [HideInInspector]//Where my air melee attacks wil be when I melee in the air.
    public Transform airMeleeGun;
    [HideInInspector]//Where my second air melee attacks wil be when I melee in the air.
    public Transform airMeleeGunTwo;
    [HideInInspector]//Empty object that holds all of my attacks.
    public GameObject playerWeaponParent;
    [HideInInspector]//Sets up when I can next shoot a projectile.
    public float projectileNextFire = 0.0f;
    [HideInInspector]//Sets up when I can next melee attack.
    public float meleeNextFire = 0.0f;
    [HideInInspector]//Sets up when I can next block.
    public float blockNextFire = 0.0f;
    [HideInInspector]//Will always be below me.
    public Transform jumpMeleeGun;

    //Scripts and player setup
    [HideInInspector]//What player # is controlling me.
    public int playerNumber = 1;
    [HideInInspector]//My HP script
    public PlayerHealth playerHealth;
    [HideInInspector]//My Movement script
    public PlayerMovement playerMovement;


    #endregion

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
        {//Put melee attacks on the ground here.
            case true://Checks if the projectile is lobbed or not

                //[CHECK] I DON'T THINK THIS IS BEING USED. AS PROJECTILES ARN'T EVER NAMED THIS.

                if (groundProjectile.name == "Player Projectile Lobbed")
                {//If it is lobbed then check what direction I am facing and create the object, then apply impulse force and toss it at the angle set in the inspector.
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
                        //Does the same thing for the secondary grounded projectile if one is set.
                        newGroundProjectile = Instantiate(groundProjectile, groundGunTwo.position, groundGunTwo.rotation);
                        newGroundProjectile.transform.parent = playerWeaponParent.transform;
                        newGroundProjectile.GetComponent<PlayerProjectile>().player = gameObject;
                        SetBasicRangedAttackStats(newGroundProjectile);
                    }

                }
                //Set up a aerial gun position object on each player.
                break;
            default:
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
            newBlockEffect.transform.parent = gameObject.transform;
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
    public virtual void SpecialMeleeAttack()
    {
        //Spends the mana to use your special melee attack.
        playerHealth.SpendMana(specialMeleeManaCost);

        //[TODO] Set up special melee attack for each character.
    }

    //Special Ranged Attacks, these will be different for each character.
    public virtual void SpecialRangedAttack()
    {
        //Spends the mana to use your special ranged attack.
        playerHealth.SpendMana(specialRangedManaCost);

        //[TODO] Set up the special ranged attack for each character.

    }

    //Special Defend, these will be different for each character.
    public virtual void SpecialPlayerDefend()
    {
        //Spends the mana to use your special ranged attack.
        playerHealth.SpendMana(specialDefendManaCost);

        //[TODO] Set up the special Defend for each character.

    }

    public virtual void SetBasicMeleeAttackStats(GameObject melee) //Sets the stats for the melee object when it is created.
    {
        melee.GetComponent<PlayerMelee>().meleeHitBoxLife = meleeHitBoxLife;
        melee.GetComponent<PlayerMelee>().meleeDamage = meleeDamage;
        melee.GetComponent<PlayerMelee>().stunLockOut = meleeHitStun;
        melee.GetComponent<PlayerMelee>().knockBack = meleeKnockBack;
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
}
