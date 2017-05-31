using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour {

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
    [HideInInspector]
    public GameObject newGroundMelee;
    [HideInInspector]
    public GameObject newAirMelee;

    //Jump Attack
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
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
    public float blockFireRate = 0.5f;
    public float blockingResistanceModifier = 15f;
    [HideInInspector]
    public bool blocking = false;
    [Tooltip("Attach a gameObject that will be spawned when you are blocking.")]
    public GameObject blockEffect; //attach a game object that will be spawned when you ar blocking.
    #endregion

    //[Header("Special Settings")]
    #region
    [HideInInspector]
    public bool specialActive = false;
    #endregion

    //[Header("Wisp Settings")]
    #region
    [HideInInspector]
    public Transform myWispTargetLocation;
    [HideInInspector]
    public GameObject wisp;
    #endregion

    //Private Attack extras
    #region
    [HideInInspector]
    public Element element; //element of this player
    [HideInInspector]
    public GameObject playerParent;
    [HideInInspector]
    public Transform groundGun;
    [HideInInspector]
    public Transform groundGunTwo;
    [HideInInspector]
    public Transform airGun;
    [HideInInspector]
    public Transform airGunTwo;
    [HideInInspector]
    public Transform groundMeleeGun;
    [HideInInspector]
    public Transform groundMeleeGunTwo;
    [HideInInspector]
    public Transform airMeleeGun;
    [HideInInspector]
    public Transform airMeleeGunTwo;
    [HideInInspector]
    public GameObject playerWeaponParent;
    [HideInInspector]
    public float projectileNextFire = 0.0f;
    [HideInInspector]
    public float meleeNextFire = 0.0f;
    [HideInInspector]
    public float blockNextFire = 0.0f;
    [HideInInspector]
    public Transform jumpMeleeGun;
    //Scripts and player setup
    [HideInInspector]
    public int playerNumber = 1;
    [HideInInspector]
    public PlayerHealth playerHealth;
    [HideInInspector]
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

    public virtual void SpecialMelee()
    {

    }

    public virtual void SpecialRanged()
    {

    }

    public virtual void CallWisp()
    {

    }
}
