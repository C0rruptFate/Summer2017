using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Tooltip("Sets the element, player number, and her attacks.")]
    public int playerNumber = 0;
    public Element element; //element of this player
    [Tooltip("How much Health do I start with? 'This is also max health.'")]
    public float startingHealth = 100f;
    //[HideInInspector]//The player's current HP
    public float health = 200f;

    [Tooltip("How much is the damage I take multiplied by if it counters my element? 'Make this above 1.0f'")]
    public float counterDamageModifier = 1.5f;
    [Tooltip("How much is the damage I take multiplied by if it counters my element? 'Make this below 1.0f'")]
    public float counterResistanceModifier = 0.75f; 

    //This player's UI component
    [HideInInspector]//The UI component associated with this player, used to update when gaining/losing hp/mana
    public GameObject playerUI;
    //Player Script
    [HideInInspector]//The attached player's attacks and actions.
    public PlayerAttacks playerAttacks;
    [HideInInspector]//The attached player's movement script.
    public PlayerMovement playerMovement;

    private GameObject[] enemiesList; //[TODO] Used to reset enemy targets when a player dies
    private GameObject gameManager; //Used to tell the game when a player dies at 0 level ends.

    //Special
    [HideInInspector]
    public Vector3 playerDefaultSize;

    [HideInInspector]
    public bool allowedToInputAttacks = true;

    [HideInInspector]
    public bool invulnerable = false;

    // Use this for initialization
    protected virtual void Start()
    {
        //Setting up the player's movement and actions scripts
        GetComponent<PlayerMovement>().playerHealth = GetComponent<PlayerHealth>();
        GetComponent<PlayerAttacks>().playerHealth = GetComponent<PlayerHealth>();
        GetComponent<PlayerAttacks>().element = element;
        playerDefaultSize = transform.localScale;
        //playerAttacks.playerNumber = playerNumber;
        //playerMovement.playerNumber = playerNumber;

        health = startingHealth;


        gameManager = GameObject.Find("Game Manager");
    }

    //Reenables the players ability to attack after hitstun
    protected virtual void HitStun()
    {
        playerAttacks.enabled = true;
    }

    //reduces the player hp, finds out what hit the player so it can't hit them again for a short amount of time, and how long I am stunned for.
    public virtual void TakeDamage(GameObject whatHitMe, float damage, float hitStun)
    {
        //damage reduction.
        float totalDamageModifier = 0;

        //if I am hit by a projectile or melee enemy
        if (whatHitMe.CompareTag("Projectile"))
        {
            if (whatHitMe.GetComponent<Projectiles>().element == Constants.whatCountersMe(element) && whatHitMe.GetComponent<Projectiles>().hurtsPlayers)
            {
                totalDamageModifier = totalDamageModifier - counterDamageModifier;
            }
            else if (whatHitMe.GetComponent<Projectiles>().element == Constants.whatICounter(element) && whatHitMe.GetComponent<Projectiles>().hurtsPlayers)
            {
                totalDamageModifier = totalDamageModifier + counterResistanceModifier;
            }
        }
        else if (whatHitMe.GetComponent<EnemyHealth>() != null)
        {//if I am hit by an enemy
            if (whatHitMe.GetComponent<EnemyHealth>().element == Constants.whatCountersMe(element))
            {
                totalDamageModifier = totalDamageModifier - counterDamageModifier;
            }
            else if (whatHitMe.GetComponent<EnemyHealth>().element == Constants.whatICounter(element))
            {
                totalDamageModifier = totalDamageModifier + counterResistanceModifier;
            }
        }
        else if (whatHitMe.GetComponent<Hazard>() != null)
        {//Damaged by a hazard
            if (whatHitMe.GetComponent<Hazard>().element == Constants.whatCountersMe(element))
            {
                totalDamageModifier = totalDamageModifier - counterDamageModifier;
            }
            else if (whatHitMe.GetComponent<Hazard>().element == Constants.whatICounter(element))
            {
                totalDamageModifier = totalDamageModifier + counterResistanceModifier;
            }
        }
        else
        {
            Debug.LogError("Something tried to damage me that I don't have listed!");
        }

        if (gameObject.GetComponent<PlayerAttacks>().blocking)//If I am blocking take reduced damage and no hitstun.
        {
            totalDamageModifier = totalDamageModifier + gameObject.GetComponent<PlayerAttacks>().blockingResistanceModifier;
            hitStun = 0;
        }
        //if invulnerable don't take damage
        if(invulnerable)
        {
            damage = 0;
        }

        damage = damage * (1 - totalDamageModifier); //calculates total damage taken.
        health -= damage;//take damage
        
        if (hitStun  != 0)//causes hit stun to disable player actions
        {
            //[TODO] Maybe include movement script.
            playerAttacks.enabled = false;
            Invoke("HitStun", hitStun);
        }
        playerUI.GetComponent<PlayerUI>().SetHealthUI();//Update the player's UI hp slider.
        if (health <= 0)//Kills the player
        {
            //trigger death animation

            // [TODO] explore getting a smaller list of only enemies targeting this player
            //enemiesList = GameObject.FindGameObjectsWithTag("Enemy");
            //foreach (var enemy in enemiesList)
            //{
            //    if (enemy.GetComponent<Enemy>().target == gameObject)
            //    {
            //        enemy.GetComponent<Enemy>().target = null;
            //    }
            //}
            PlayerDied();
        }
    }

    //Heals the player by the amount given.
    public virtual void Heal(float healGain)
    {
        health += healGain;
        if(health >= startingHealth)
        {
            health = startingHealth;
        }
        //Update the player's UI HP slider
        playerUI.GetComponent<PlayerUI>().SetHealthUI();
    }

    //Used to spend mana on specials
    //public void SpendMana(float manaCost)
    //{
    //    if(mana >= manaCost)
    //    {
    //        Debug.Log("Spend mana");
    //        mana -= manaCost;
    //        playerUI.GetComponent<PlayerUI>().SetManaUI();
    //    }
    //    else
    //    {
    //        Debug.Log("Spend mana but didn't have enough");
    //        //[TODO] Set up feedback for when you don't have enough mana to use an ability
    //    }
    //}

    //kills the player by disabling them
    public virtual void PlayerDied()
    {
        //Destroy(playerScript.GetComponent<PlayerController>().newGroundMelee);
        //Destroy(playerScript.GetComponent<PlayerController>().newAirMelee);
        gameManager.GetComponent<GameController>().LowerPlayerCount();
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}