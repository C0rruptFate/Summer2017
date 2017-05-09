//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class UIHealth : MonoBehaviour {

//    public string playerNumber;
//    public string healthUIName;
//    public Image fillImage;
//    public Image playerUIPanel;
//    public Color fullHealthColor = Color.green;
//    public Color halfHealthColor = Color.yellow;
//    public Color lowHealthColor = Color.red;
//    public Color noHealthColor = Color.black;

//    private Transform healthUI;
//    private Slider hpSlider;
//    private float maxHealth;
//    private float minHealth;
//    private float currentHealth;
//    private float startingHealth;
//    private GameObject player;
//    private PlayerHealth playerHealth;

//    //Dealing with special
//    private Transform specialUI;
//    private Slider specialSlider;

//	// Use this for initialization
//	void Start () {

//        //Find our UI elements
//        healthUI = gameObject.transform.GetChild(1);
//        specialUI = gameObject.transform.GetChild(2);

//        hpSlider = healthUI.GetComponent<Slider>();
//        specialSlider = specialUI.GetComponent<Slider>();

//        if (hpSlider == null || specialSlider == null)
//        {
//            Debug.Log("Missing a Slider");
//        }

//        //Find our player
//        player = GameObject.Find(playerNumber);
//        if (player == null)
//        {
//            Destroy(gameObject);
//            return;
//        }

//        //Find player HP
//        playerHealth = player.GetComponent<PlayerHealth>();
//        startingHealth = playerHealth.health;
//        hpSlider.maxValue = startingHealth;
//        playerHealth.uiHealth = healthUI;
//        fillImage.color = Color.green;
//        SetHealthUI();

//        //Find player Special

//        //Make the player's panal match their color
//        if(playerHealth.element == Element.Fire)
//        {
//            playerUIPanel.color = Color.red;
//        }

//	}

//    //Call this everytime the player takes damage or is healed. Using 'uiHealth.GetComponent<UIHealth>().SetHealthUI();' on the player
//    public void SetHealthUI()
//    {
//        currentHealth = playerHealth.health;
//        hpSlider.value = currentHealth;

//        if(currentHealth <= (startingHealth / 2) && currentHealth >= (startingHealth /3))
//        {
//            fillImage.color = halfHealthColor;
//        }else if(currentHealth <= (startingHealth / 3))
//        {
//            fillImage.color = lowHealthColor;
//        }else if (currentHealth <= 0)
//        {
//            fillImage.color = noHealthColor;
//        }
//    }
//}
