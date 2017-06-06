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

    [Header("Block Settings")]
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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void MeleeAttack()
    {

    }

    public virtual void RangedAttack()
    {

    }

    public virtual void PlayerDefend()
    {

    }

    public virtual void JumpAttack()
    {

    }

    public virtual void SpecialMeleeAttack()
    {

    }

    public virtual void SpecialRangedAttack()
    {

    }

    public virtual void SpecialPlayerDefend()
    {

    }

    public virtual void CallWisp()
    {

    }
}
