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
    public float shrinkSpeed = 50;
    [SerializeField]
    private bool shrinking;
    [SerializeField]
    private bool growing;
    [SerializeField]
    private bool specialMeleeMovement;
    public float specialMeleeMovementSpeed = 20f;
    public float speicalMeleeMovementDuration = 2f;
    private Vector3 targetShrinkScale = new Vector3(1f, 1f, 1f);

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
    //    //Calls Wisp and ticks up how long it has been held down. When greater than 20 the Wisp will attach to you.
    //    //Debug.Log("Callwisp" + Input.GetAxisRaw("CallWisp" + playerNumber));
    //    if (Input.GetAxisRaw("CallWisp" + playerNumber) > 0.25f)
    //    {
    //        CallWisp();
    //        callingWisp = true;
    //    }//Stops calling the Wisp when the button isn't held down.
    //    else if (Input.GetAxisRaw("CallWisp" + playerNumber) <= 0.25f)
    //    {
    //        callingWisp = false;
    //    }

    //    //Activate Special
    //    if (Input.GetAxisRaw("Special" + playerNumber) == 1)//Enables the special attack to be used by the melee, ranged, and defend attacks.
    //    {
    //        //print("Special Trigger pressed" + Input.GetAxis("Special" + playerNumber));

    //        //[TODO]Play special particle effect
    //        //turn special is active to true
    //        //Debug.Log("Special is active.");
    //        specialActive = true;
    //    }
    //    if (Input.GetAxisRaw("Special" + playerNumber) != 1 && specialActive)//Turns off the special when the button/trigger is released.
    //    {
    //        //Turn off special
    //        //Debug.Log("SpeciaL has been DEACTIVATED");
    //        specialActive = false;
    //    }

    //    //Melee attacks
    //    //[TODO ALSO REQUIRE MANA TO BE >=SPECIAL MELEE MANA COST]
    //    if (specialActive && currentSpecialMeleeCooldown == 0 && Input.GetButtonDown("Melee" + playerNumber) && Time.time > meleeNextFire && playerHealth.allowedToInputAttacks)//Special Melee Attack
    //    {
    //        //Debug.Log("Melee Special is active.");
    //        SpecialMeleeAttack();
    //    }
    //    else if (Input.GetButtonDown("Melee" + playerNumber) && Time.time > meleeNextFire && playerHealth.allowedToInputAttacks)//Melee Attack
    //    {
    //        MeleeAttack();
    //    }

    //    //Ranged Attacks
    //    //[TODO ALSO REQUIRE MANA TO BE >=SPECIAL MELEE MANA COST]
    //    if (specialActive && currentSpecialRangedCooldown <= 0 && Input.GetButtonDown("Ranged" + playerNumber) && Time.time > projectileNextFire && playerHealth.allowedToInputAttacks)//Special Ranged Attack
    //    {
    //        //Debug.Log("Ranged Special is active.");
    //        SpecialRangedAttack();
    //    }
    //    else if (Input.GetButtonDown("Ranged" + playerNumber) && Time.time > projectileNextFire && playerHealth.allowedToInputAttacks)//Ranged Attack
    //    {
    //        RangedAttack();
    //    }

    //    //Defend
    //    //[TODO ALSO REQUIRE MANA TO BE >=SPECIAL MELEE MANA COST]
    //    if (specialActive && currentSpecialDefendCooldown == 0 && Input.GetButton("Defend" + playerNumber) && Time.time >= blockNextFire && playerHealth.allowedToInputAttacks)//Special Block
    //    {
    //        Debug.Log("Defend Special is active.");
    //        if (!blocking)
    //        {
    //            blocking = true;
    //            SpecialPlayerDefend();
    //        }
    //    }
    //    else if (Input.GetButton("Defend" + playerNumber) && Time.time >= blockNextFire && playerHealth.allowedToInputAttacks)//Block
    //    {
    //        if (!blocking)//If I am not already blocking start blocking
    //        {
    //            blocking = true;
    //            PlayerDefend();//Creates the block effect and all that goes with that.
    //        }
    //    }
    //    else if (blocking)//Causes me to release the block.
    //    {
    //        blocking = false;
    //        blockNextFire = Time.time + blockFireRate;
    //        PlayerDefend();
    //    }

    //    //Cooldowns
    //    //Melee Cooldown
    //    if (currentSpecialMeleeCooldown == specialMeleeCooldown)
    //    {
    //        StartCoroutine(meleeCooldownCoroutine);
    //    }
    //    else if (currentSpecialMeleeCooldown <= 0)
    //    {
    //        StopCoroutine(meleeCooldownCoroutine);
    //    }
    //    //Ranged Cooldown
    //    if (currentSpecialRangedCooldown == specialRangedCooldown)
    //    {
    //        StartCoroutine(rangedCooldownCoroutine);
    //    }
    //    else if (currentSpecialRangedCooldown <= 0)
    //    {
    //        StopCoroutine(rangedCooldownCoroutine);
    //    }
    //    //Defend Cooldown
    //    if (currentSpecialDefendCooldown == specialDefendCooldown)
    //    {
    //        StartCoroutine(defendCooldownCoroutine);
    //    }
    //    else if (currentSpecialDefendCooldown <= 0)
    //    {
    //        StopCoroutine(defendCooldownCoroutine);
    //    }

        //Start Fire stuff
        if (shrinking)
        {
            Debug.Log("Shrinking");
            transform.localScale -= Vector3.one * Time.deltaTime * shrinkSpeed;
            if (transform.localScale.x < targetShrinkScale.x && transform.localScale.y < targetShrinkScale.y)
            {
                transform.localScale = targetShrinkScale;
                shrinking = false;
            }
                
        }
        if(growing)
        {
            Debug.Log("Growing");
            transform.localScale += Vector3.one * Time.deltaTime * shrinkSpeed;
            if(transform.localScale.x > playerHealth.playerDefaultSize.x)
            {
                transform.localScale = playerHealth.playerDefaultSize;
                growing = false;
            }     
        }

    }

    public void FixedUpdate()
    {
        if (specialMeleeMovement)
        {
            rb.MovePosition(transform.position + Vector3.right * specialMeleeMovementSpeed * Time.fixedDeltaTime);
            //transform.Translate(Vector2.right * specialMeleeMovementSpeed * Time.deltaTime);
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
        //Vector2 targetShrinkSize = new Vector2(1, 1);
        //base.SpecialMeleeAttack();

        rb.gravityScale = 0;

        gameObject.layer = 17;

        //Become invincible
        playerHealth.invulnerable = true;

        //Stops other attacks from being input
        playerHealth.allowedToInputAttacks = false;

        //Start shrinking
        shrinking = true;

        // Create Damage object at our location

        //Start Moving
        specialMeleeMovement = true;
        
        //Grow when reaching target
        Invoke("SpecialMeleeAttackPart2", speicalMeleeMovementDuration);

        currentComboPoints = 0;

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

        specialRangedAttackObject.GetComponent<PlayerProjectileFireSpecial>().explosionObject.GetComponent<CircleCollider2D>().radius = rangedExplosionStartingRadius + currentComboPoints;
        //Debug.Log("Dump all combo points " + currentComboPoints + " Radius: " + specialRangedAttackObject.GetComponent<PlayerProjectileFireSpecial>().explosionObject.GetComponent<CircleCollider2D>().radius);
        //Resets combo point count.
        currentComboPoints = 0;
        //Informs the UI and tells it to update.
        GetComponent<PlayerHealth>().playerUI.GetComponent<PlayerUI>().UpdateComboPointUI();
    }

    public override void SpecialPlayerDefend()
    {
        base.SpecialPlayerDefend();
        //currentComboPoints = 0;
        //[TODO] Set up the special Defend for each character.

    }

    void SpendComboPoints()
    {

    }
}
