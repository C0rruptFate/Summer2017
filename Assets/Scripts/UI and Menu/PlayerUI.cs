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
    public Image hpFillImage;
    
    private Color fullHealthColor = Color.green;
    private Color halfHealthColor = Color.yellow;
    private Color lowHealthColor = Color.red;

    private Transform healthUI;
    private Slider hpSlider;
    private float maxHealth;
    private float currentHealth;
    private float startingHealth;

    //Dealing with Cooldowns
    public Image meleeFillImage;
    public Text meleeCooldownText;
    private Transform meleeSpecialUI;
    private Slider meleeSlider;

    public Image rangedFillImage;
    public Text rangedCooldownText;
    private Transform rangedSpecialUI;
    private Slider rangedSlider;

    public Image defendFillImage;
    public Text defendCooldownText;
    private Transform defendSpecialUI;
    private Slider defendSlider;

    //Dealing with mana
    //public Image manaFillImage;

    //private Transform manaUI;
    //private Slider manaSlider;
    //private float currentMana;
    //private float maxMana;

    //Other
    private PlayerHealth playerHealth; //player HP script
    private PlayerAttacks playerAttacks;
    private GameObject player; //the player themself

    // Use this for initialization
    void Start()
    {
        //Find our UI elements
        healthUI = gameObject.transform.Find("Health Slider");
        //manaUI = gameObject.transform.GetChild(2);
        //finds the sliders of those elements.
        hpSlider = healthUI.GetComponent<Slider>();
        //manaSlider = manaUI.GetComponent<Slider>();

        //Sets up the Sliders for player cooldowns
        meleeSpecialUI = gameObject.transform.Find("Melee Cooldown Slider");
        meleeSlider = healthUI.GetComponent<Slider>();
        rangedSpecialUI = gameObject.transform.Find("Ranged Cooldown Slider");
        rangedSlider = healthUI.GetComponent<Slider>();
        defendSpecialUI = gameObject.transform.Find("Defend Cooldown Slider");
        defendSlider = healthUI.GetComponent<Slider>();

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


        //Find player mana
        //maxMana = playerResources.maxMana;
        //manaSlider.maxValue = maxMana;


        //Set the UI
        SetHealthUI();
        //SetManaUI();

        //Make the player's panal match their color
        
        switch (playerHealth.element)
        {
            case Element.Wind:
                playerUIPanel.color = airColor;
                break;
            case Element.Earth:
                playerUIPanel.color = earthColor;
                break;
            case Element.Fire:
                playerUIPanel.color = fireColor;
                break;
            case Element.Ice:
                playerUIPanel.color = iceColor;
                break;
            default:
                break;
        }
    }

    //Call this everytime the player takes damage or is healed. Using 'uiHealth.GetComponent<UIHealth>().SetHealthUI();' on the player
    public void SetHealthUI()//Updates the HP UI when the player gains/loses HP.
    {
        currentHealth = playerHealth.health;
        hpSlider.value = currentHealth;
        
        //Changes HP bar colors
        if (currentHealth <= 0)
        {
            hpFillImage.enabled = false;
        }
        else if (currentHealth <= (startingHealth / 3))
        {
            hpFillImage.color = lowHealthColor;
        }
        else if (currentHealth <= (startingHealth / 2))
        {
            hpFillImage.color = halfHealthColor;
        }else if(currentHealth == startingHealth)
        {
            hpFillImage.color = fullHealthColor;
        }
    }

    public void SetManaUI() //Sets the mana UI when the player spends/gains mana.
    {
        //currentMana = playerResources.mana;
        //manaSlider.value = currentMana;
        ////Debug.Log("Current mana: " + currentMana);
        //if (currentMana == 0)
        //{
        //    manaFillImage.enabled = false;
        //}
        //else
        //{
        //    manaFillImage.enabled = true;
        //}

        //[TODO] remove this and set up cooldowns
    }
}