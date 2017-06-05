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

    //Dealing with mana
    public Image manaFillImage;
    
    private Transform manaUI;
    private Slider manaSlider;
    private float currentMana;
    private float maxMana;

    //Other
    private PlayerHealth playerHealth; //player HP script
    private GameObject player; //the player themself
    private PlayerHealth playerResources; //the player's mana

    // Use this for initialization
    void Start()
    {
        //Find our UI elements
        healthUI = gameObject.transform.GetChild(1);
        manaUI = gameObject.transform.GetChild(2);
        //finds the sliders of those elements.
        hpSlider = healthUI.GetComponent<Slider>();
        manaSlider = manaUI.GetComponent<Slider>();

        if (hpSlider == null || manaSlider == null)
        {
            //Lets the designer know if those can't be found, this shouldn't ever show.
            Debug.Log("Missing a Slider");
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
        playerResources = player.GetComponent<PlayerHealth>();
        startingHealth = playerResources.health;
        playerResources.playerUI = gameObject;
        maxHealth = startingHealth;
        hpSlider.maxValue = maxHealth;
        hpFillImage.color = fullHealthColor;
        

        //Find player mana
        maxMana = playerResources.maxMana;
        manaSlider.maxValue = maxMana;


        //Set the UI
        SetHealthUI();
        SetManaUI();

        //Make the player's panal match their color
        playerHealth = player.GetComponent<PlayerHealth>();
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
        currentHealth = playerResources.health;
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
        currentMana = playerResources.mana;
        manaSlider.value = currentMana;
        //Debug.Log("Current mana: " + currentMana);
        if (currentMana == 0)
        {
            manaFillImage.enabled = false;
        }
        else
        {
            manaFillImage.enabled = true;
        }

    }
}