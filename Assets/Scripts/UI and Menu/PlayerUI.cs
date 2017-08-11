using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    //Dealing with the UI panel and background color.
    public Image playerUIPanel;
    public Color fireColor;
    public Color iceColor;
    public Color earthColor;
    public Color airColor;

    //Finding the Player
    [HideInInspector]public GameObject[] players;
    public int playerNumber;

    //Dealing with Health
    public Text hpText;
    public Image hpFillImage;
    
    private Color fullHealthColor = Color.green;
    private Color halfHealthColor = Color.yellow;
    private Color lowHealthColor = Color.red;
    private Color noHealthColor = Color.black;

    private Transform healthUI;
    private Slider hpSlider;
    [HideInInspector]
    public float maxHealth;
    [HideInInspector]
    public float currentHealth;
    private float startingHealth;

    //Dealing with Cooldowns
    public Text meleeCooldownText;
    private Transform meleeSpecialUI;
    private Slider meleeSlider;

    //public Image rangedFillImage;
    public Text rangedCooldownText;
    private Transform rangedSpecialUI;
    private Slider rangedSlider;

    //public Image defendFillImage;
    public Text defendCooldownText;
    private Transform defendSpecialUI;
    private Slider defendSlider;

    //Fire Combo point system
    private Transform fireComboSystem;
    private Slider fireComboSlider;
    private AttacksFire fireAttacks;
    public Text fireComboCountText;

    //Other
    private PlayerHealth playerHealth; //player HP script
    private PlayerAttacks playerAttacks;
    private GameObject player; //the player themself
    private Animator anim;
    [HideInInspector]
    public bool healed;

    // Use this for initialization
    void Start()
    {
        //Find our UI elements
        healthUI = gameObject.transform.Find("Health Slider");
        //manaUI = gameObject.transform.GetChild(2);
        //finds the sliders of those elements.
        hpSlider = healthUI.GetComponent<Slider>();
        //Animation Stuff
        anim = GetComponent<Animator>();
        anim.SetBool("UITakeDamage", false);
        anim.SetBool("UIHeal", false);
        //manaSlider = manaUI.GetComponent<Slider>();

        //Sets up the Sliders for player cooldowns
        meleeSpecialUI = gameObject.transform.Find("Melee Cooldown Slider");
        meleeSlider = meleeSpecialUI.GetComponent<Slider>();
        //meleeCooldownText = gameObject.transform.Find("Melee Ready Text");
        rangedSpecialUI = gameObject.transform.Find("Ranged Cooldown Slider");
        rangedSlider = rangedSpecialUI.GetComponent<Slider>();
        defendSpecialUI = gameObject.transform.Find("Defend Cooldown Slider");
        defendSlider = defendSpecialUI.GetComponent<Slider>();

        if (hpSlider == null || meleeSlider == null || rangedSlider == null || defendSlider == null)
        {
            //Lets the designer know if those can't be found, this shouldn't ever show.
            Debug.Log(gameObject.name + " Missing a Slider");
        }

        //Find our player
        players = GameObject.FindGameObjectsWithTag("Player");
        //Finds the player with the player # we are looking for.
        foreach (var possiblePlayer in players)
        {
            if(possiblePlayer.GetComponent<PlayerHealth>().playerNumber == playerNumber)
            {
                player = possiblePlayer;
            }
        }

        //disables/destroys the UI component if the player doesn't exist.
        if (player == null)
        {
            Destroy(gameObject);
            return;
        }

        //Find player HP and it's max/current values.
        playerHealth = player.GetComponent<PlayerHealth>();
        startingHealth = playerHealth.health;
        playerHealth.playerUI = gameObject;
        maxHealth = startingHealth;
        hpSlider.maxValue = maxHealth;
        hpFillImage.color = fullHealthColor;

        //Setting up Cooldown caps.
        playerAttacks = player.GetComponent<PlayerAttacks>();
        meleeSlider.maxValue = playerAttacks.specialMeleeCooldown;
        rangedSlider.maxValue = playerAttacks.specialRangedCooldown;
        defendSlider.maxValue = playerAttacks.specialDefendCooldown;

        //Set the UI
        //SetHealthUI();
        #region
        currentHealth = playerHealth.health;
        hpSlider.value = currentHealth;
        hpText.text = currentHealth.ToString() + " / " + maxHealth;

        //Changes HP bar colors
        if (currentHealth <= 0)
        {
            hpFillImage.color = noHealthColor;
        }
        else if (currentHealth <= (startingHealth / 4))//HP turns red when you are below 25% hp
        {
            hpFillImage.color = lowHealthColor;
        }
        else if (currentHealth <= (startingHealth / 2))//HP turns yellow when you are below 25% hp
        {
            hpFillImage.color = halfHealthColor;
        }
        else if (currentHealth == startingHealth)
        {
            hpFillImage.color = fullHealthColor;
        }
        #endregion

        //Fire Combo meter
        fireComboSystem = gameObject.transform.Find("Fire Combo Counter");
        fireComboSlider = fireComboSystem.GetComponent<Slider>();

        //Make the player's panal match their color

        switch (playerHealth.element)
        {
            case Element.Air:
                playerUIPanel.color = airColor;
                fireComboSystem.gameObject.SetActive(false);
                break;
            case Element.Earth:
                playerUIPanel.color = earthColor;
                fireComboSystem.gameObject.SetActive(false);
                break;
            case Element.Fire:
                playerUIPanel.color = fireColor;
                fireAttacks = player.GetComponent<AttacksFire>();
                fireComboSlider.maxValue = fireAttacks.maxComboPoints;
                UpdateComboPointUI();
                break;
            case Element.Water:
                playerUIPanel.color = iceColor;
                fireComboSystem.gameObject.SetActive(false);
                break;
            default:
                break;
        }

    }

    public void Update()
    {
        meleeSlider.value = playerAttacks.currentSpecialMeleeCooldown;
        if (meleeSlider.value != 0)
        {
            meleeCooldownText.text = meleeSlider.value.ToString();
        } 
        else
        {
            meleeCooldownText.text = "R";
        }

        rangedSlider.value = playerAttacks.currentSpecialRangedCooldown;
        if (rangedSlider.value != 0)
        {
            rangedCooldownText.text = rangedSlider.value.ToString();
        }
        else
        {
            rangedCooldownText.text = "R";
        }

        defendSlider.value = playerAttacks.currentSpecialDefendCooldown;
        if (defendSlider.value != 0)
        {
            defendCooldownText.text = defendSlider.value.ToString();
        }
        else
        {
            defendCooldownText.text = "R";
        }
    }

    //Call this everytime the player takes damage or is healed. Using 'uiHealth.GetComponent<UIHealth>().SetHealthUI();' on the player
    public void SetHealthUI()//Updates the HP UI when the player gains/loses HP.
    {
        if (playerHealth.health < currentHealth) //Testing UI shake Take Damage
        {
            //Debug.Log("Took Damage");
            anim.SetBool("UITakeDamage", true);
        }
        else if (playerHealth.health > currentHealth) //Testing UI shake Take Damage
        {
            //Debug.Log("Healed");
            anim.SetBool("UIHeal", true);
        }

        currentHealth = playerHealth.health;
        hpSlider.value = currentHealth;
        hpText.text = currentHealth.ToString() + " / " + maxHealth;
        
        //Changes HP bar colors
        if (currentHealth <= 0)
        {
            hpFillImage.color = noHealthColor;
        }
        else if (currentHealth <= (startingHealth / 4))//HP turns red when you are below 25% hp
        {
            hpFillImage.color = lowHealthColor;
        }
        else if (currentHealth <= (startingHealth / 2))//HP turns yellow when you are below 25% hp
        {
            hpFillImage.color = halfHealthColor;
        }else if(currentHealth == startingHealth)
        {
            hpFillImage.color = fullHealthColor;
        }
    }

    public void UpdateComboPointUI()
    {
        if (playerHealth.element == Element.Fire)
        {
            fireComboSlider.value = fireAttacks.currentComboPoints;
            fireComboCountText.text = fireComboSlider.value.ToString();
        }
    }

    public void NoLongerTakingDamage()
    {
        anim.SetBool("UITakeDamage", false);
    }

    public void FinishHealUIEffect()
    {
        anim.SetBool("UIHeal", false);
    }
}