using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Element element; //element of this player
    [Tooltip("How much Health do I start with? 'This is also max health.'")]
    public float startingHealth = 100f;
    [HideInInspector]
    public float health = 100f;

    [Tooltip("What is my max mana?")]
    public float maxMana = 100f;
    [Tooltip("How much mana do I start with?")]
    public float startingMana =100f;
    [HideInInspector]
    public float mana;
    [Tooltip("How much is the damage I take multiplied by if it counters my element? 'Make this above 1.0f'")]
    public float counterDamageModifier = 1.5f;
    [Tooltip("How much is the damage I take multiplied by if it counters my element? 'Make this below 1.0f'")]
    public float counterResistanceModifier = 0.75f; 

    //This player's UI component
    [HideInInspector]
    public GameObject playerUI;

    private GameObject[] enemiesList;
    private Behaviour playerScript;
    private GameObject gameManager;
    private bool blocking;
    private float blockingResistanceModifier;

    //Special



    // Use this for initialization
    void Start()
    {
        health = startingHealth;
        mana = startingMana;

        if(gameObject.GetComponent<PlayerController>() != null)
        {
            playerScript = gameObject.GetComponent<PlayerController>();
            element = gameObject.GetComponent<PlayerController>().element;
            blocking = gameObject.GetComponent<PlayerController>().blocking;
            blockingResistanceModifier = gameObject.GetComponent<PlayerController>().blockingResistanceModifier;
        }

        gameManager = GameObject.Find("Game Manager");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void HitStun()
    {
        playerScript.enabled = true;
    }

    public void TakeDamage(GameObject whatHitMe, float damage, float hitStun)
    {
        float totalDamageModifier = 0;
        if (whatHitMe.CompareTag("Projectile"))
        {
            if (whatHitMe.GetComponent<EnemyProjectile>().element == Constants.whatCountersMe(element))
            {
                totalDamageModifier = totalDamageModifier - counterDamageModifier;
            }
            else if (whatHitMe.GetComponent<EnemyProjectile>().element == Constants.whatICounter(element))
            {
                totalDamageModifier = totalDamageModifier + counterResistanceModifier;
            }
        }
        else
        {
            if (whatHitMe.GetComponent<Enemy>().element == Constants.whatCountersMe(element))
            {
                totalDamageModifier = totalDamageModifier - counterDamageModifier;
            }
            else if (whatHitMe.GetComponent<Enemy>().element == Constants.whatICounter(element))
            {
                totalDamageModifier = totalDamageModifier + counterResistanceModifier;
            }
        }
        if (blocking) // [TODO] this comes from the player controler script
        {
            totalDamageModifier = totalDamageModifier + blockingResistanceModifier; // [TODO] set this up at the top
            hitStun = 0;
        }
        damage = damage * (1 - totalDamageModifier);
        health -= damage;
        //[TODO] if hitStun == 0 disable below
        if (hitStun  != 0)
        {
            playerScript.enabled = false;
            Invoke("HitStun", hitStun);
        }
        playerUI.GetComponent<PlayerUI>().SetHealthUI();
        if (health <= 0)
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

    public void Heal(float healGain)
    {
        health += healGain;
        if(health >= startingHealth)
        {
            health = startingHealth;
        }
        playerUI.GetComponent<PlayerUI>().SetHealthUI();
    }

    public void GainMana(float manaGain)
    {
        mana += manaGain;
        if (mana >= maxMana)
        {
            mana = maxMana;
        }
        playerUI.GetComponent<PlayerUI>().SetManaUI();
    }

    public void SpendMana(float manaCost)
    {
        mana -= manaCost;
        playerUI.GetComponent<PlayerUI>().SetManaUI();
    }

    public void PlayerDied()
    {
        //Destroy(playerScript.GetComponent<PlayerController>().newGroundMelee);
        //Destroy(playerScript.GetComponent<PlayerController>().newAirMelee);
        gameManager.GetComponent<GameController>().LowerPlayerCount();
        Destroy(gameObject);
    }
}