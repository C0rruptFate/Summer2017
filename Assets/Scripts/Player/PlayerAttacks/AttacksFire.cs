using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttacksFire : PlayerAttacks {
    [Header("Character Specific Settings")]
    [Tooltip("Max combo points for this character")]
    public int maxComboPoints;

    [Tooltip("Ranged Special explosion radius")]
    public int rangedExplosionStartingRadius = 3;
    [HideInInspector]
    public int currentComboPoints;
    public float comboPointFallRateDelay = 10f;
    public float comboPointFallRate = 2f;
    public bool comboPointCountDown = false;

    public float shrinkSpeed = 50;
    private bool shrinking;
    private bool growing;
    private bool specialMeleeMovement;
    public float specialMeleeMovementSpeed = 20f;
    public float speicalMeleeMovementDuration = 2f;
    private Vector3 targetShrinkScale = new Vector3(1f, 1f, 1f);

    public bool comboPointAlreadyCounting = true;
    [HideInInspector]
    public float currentTimeDelaySet;

    //private bool allowedToInputAttacks = true;



    // Use this for initialization
    public override void Start()
    {
        //Plugs myself into my PlayerHealth and Player movement scripts.
        // [TODO] CHANGE THESE GETCOMPONENTS FOR EACH ELEMENTAL ATTACK SCRIPT.
        GetComponent<PlayerHealth>().playerAttacks = GetComponent<AttacksFire>();
        GetComponent<PlayerMovement>().playerAttacks = GetComponent<AttacksFire>();
        base.Start();
    }

    public override void Update()
    {
        base.Update();

        //Start Fire stuff
        if (shrinking)
        {
            //Debug.Log("Shrinking");
            transform.localScale -= Vector3.one * Time.deltaTime * shrinkSpeed;
            if (transform.localScale.x < targetShrinkScale.x && transform.localScale.y < targetShrinkScale.y)
            {
                transform.localScale = targetShrinkScale;
                shrinking = false;
            }
                
        }
        if(growing)
        {
            //Debug.Log("Growing");
            transform.localScale += Vector3.one * Time.deltaTime * shrinkSpeed;
            if(transform.localScale.x > playerHealth.playerDefaultSize.x)
            {
                transform.localScale = playerHealth.playerDefaultSize;
                growing = false;
            }     
        }

        if ((Time.time >= comboPointFallRateDelay + currentTimeDelaySet) && !comboPointAlreadyCounting)
        {
            StartCoroutine("ComboPointFallOffWait");
            comboPointAlreadyCounting = true;
        }
    }

    public void FixedUpdate()
    {
        if (specialMeleeMovement)
        {
            if (playerMovement.facingRight)
            {
                //Debug.Log("Go to the right");
                rb.MovePosition(transform.position + Vector3.right * specialMeleeMovementSpeed * Time.fixedDeltaTime); 
            }
            else
            {
                //Debug.Log("Go to the left");
                rb.MovePosition(transform.position + Vector3.left * specialMeleeMovementSpeed * Time.fixedDeltaTime);
            }
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
                    Invoke("SecondaryMeleeAttack", meleeHitBoxLife - 0.1f);
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
        currentSpecialMeleeCooldown = specialMeleeCooldown - currentComboPoints;
        StartCoroutine(meleeCooldownCoroutine);
        SpendComboPoints();
        meleeNextFire = Time.time + meleeFireRate; //Decides when preform another melee attack.
        //Checks if I am grounded. Creates the melee object at my gun location, parents it to the weapons gameobject, and sets the weapon's location to the player's gun.
        switch (gameObject.GetComponent<PlayerMovement>().grounded)
        {
            case true:
                //Put melee attacks on the ground here.
                newGroundMelee = Instantiate(specialMeleeAttackObject, transform.position, transform.rotation);
                newGroundMelee.transform.parent = playerWeaponParent.transform;
                newGroundMelee.GetComponent<PlayerMelee>().player = gameObject;
                newGroundMelee.GetComponent<PlayerMelee>().myGun = transform;
                SetSpecialMeleeAttackStats(newGroundMelee);
                if (groundMeleeGunTwo != null && useMeleeSecondaryGunSpecial)//Does the same for the 2nd grounded melee attack, if I have one.
                {
                    newGroundMelee = Instantiate(specialMeleeAttackObject, transform.position, transform.rotation);
                    newGroundMelee.transform.parent = playerWeaponParent.transform;
                    newGroundMelee.GetComponent<PlayerMelee>().player = gameObject;
                    newGroundMelee.GetComponent<PlayerMelee>().myGun = transform;
                    SetSpecialMeleeAttackStats(newGroundMelee);
                }
                break;
            default://If I am not grounded. Creates the melee object at my gun location, parents it to the weapons gameobject, and sets the weapon's location to the player's gun.
                //Do Air melee attack stuff here. 
                newAirMelee = Instantiate(specialMeleeAttackObject, transform.position, transform.rotation);
                newAirMelee.transform.parent = playerWeaponParent.transform;
                newAirMelee.GetComponent<PlayerMelee>().player = gameObject;
                newAirMelee.GetComponent<PlayerMelee>().myGun = transform;
                SetSpecialMeleeAttackStats(newAirMelee);
                if (airMeleeGunTwo != null && useMeleeSecondaryGunSpecial) //Does the same for the 2nd aerial, if I have one.
                {
                    newAirMelee = Instantiate(specialMeleeAttackObject, transform.position, transform.rotation);
                    newAirMelee.transform.parent = playerWeaponParent.transform;
                    newAirMelee.GetComponent<PlayerMelee>().player = gameObject;
                    newAirMelee.GetComponent<PlayerMelee>().myGun = transform;
                    SetSpecialMeleeAttackStats(newAirMelee);
                }
                break;
        }

        //Removes gravity so the player goes a single direction.
        rb.gravityScale = 0;

        //Makes it so the player doesn't collide with enemies (his attack does) but can still be blocked
        gameObject.layer = 17;

        //Become invincible
        playerHealth.invulnerable = true;

        //Stops other attacks from being input
        playerHealth.allowedToInputAttacks = false;

        //Start shrinking
        shrinking = true;
       
        //Start Moving
        specialMeleeMovement = true;
        
        //Grow when reaching target
        Invoke("SpecialMeleeAttackPart2", speicalMeleeMovementDuration);
        
        //[TODO] Set up special melee attack for each character.
    }

    public void SpecialMeleeAttackPart2()
    {
        rb.gravityScale = 1;
        gameObject.layer = 8;
        growing = true;
        specialMeleeMovement = false;
        playerHealth.invulnerable = false;
        playerHealth.allowedToInputAttacks = true;
    }

    protected override void SpecialRangedAttack()
    {
        base.SpecialRangedAttack();
        //Causes the radius of the explosion to scale with the # of combo points you have.
        //Debug.Log("Dump all combo points " + currentComboPoints + " Radius: " + specialRangedAttackObject.GetComponent<PlayerProjectileFireSpecial>().explosionObject.GetComponent<CircleCollider2D>().radius);
        //Resets combo point count.
        SpendComboPoints();
    }

    public override void SpecialPlayerDefend()
    {
        base.SpecialPlayerDefend();
        SpendComboPoints();

    }

    public override void SetSpecialMeleeAttackStats(GameObject melee) //Sets the stats for the melee object when it is created.
    {
        melee.GetComponent<PlayerMelee>().meleeHitBoxLife = speicalMeleeMovementDuration;
        melee.GetComponent<PlayerMelee>().meleeDamage = specialMeleeDamage;
        melee.GetComponent<PlayerMelee>().stunLockOut = meleeHitStun;
        melee.GetComponent<PlayerMelee>().knockBack = meleeKnockBack;
        melee.GetComponent<PlayerMelee>().effectParticle = meleeEffectParticle;
    }

    public override void SetSpecialRangedAttackStats(GameObject projectile)
    {
        base.SetSpecialRangedAttackStats(projectile);
        specialRangedAttackObject.GetComponent<PlayerProjectileFireSpecial>().explosionObject.GetComponent<CircleCollider2D>().radius = rangedExplosionStartingRadius + currentComboPoints;
    }

    public override void SetSpecialDefendStats(GameObject defend)
    {
        base.SetSpecialDefendStats(defend);
        defend.GetComponent<CircleCollider2D>().radius = rangedExplosionStartingRadius + currentComboPoints;
        defend.GetComponent<FireProjectileExplosion>().shooter = gameObject;
    }

    void SpendComboPoints()
    {
        currentComboPoints = 0;
        //Informs the UI and tells it to update.
        comboPointAlreadyCounting = false;
        GetComponent<PlayerHealth>().playerUI.GetComponent<PlayerUI>().UpdateComboPointUI();
    }

    public IEnumerator ComboPointFallOffWait()
    {
        //yield return new WaitForSeconds(comboPointFallRateDelay);
        while (currentComboPoints >= 1 && (comboPointFallRateDelay + currentTimeDelaySet < Time.time))
        {
            yield return new WaitForSeconds(comboPointFallRate);
            if (currentComboPoints >= 1 && (comboPointFallRateDelay + currentTimeDelaySet < Time.time))
            {
                currentComboPoints--;
                GetComponent<PlayerHealth>().playerUI.GetComponent<PlayerUI>().UpdateComboPointUI(); 
            }
            if (currentComboPoints <= 0)
            {
                comboPointCountDown = false;
                comboPointAlreadyCounting = false;
            }
        }
    }
}
