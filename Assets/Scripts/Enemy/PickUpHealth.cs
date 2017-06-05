using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHealth : MonoBehaviour {

    [Tooltip("How much does this heal for?")]
    public float heal;
    [Tooltip("How much mana does this give?")]
    public float mana;

    [Tooltip("What element am I? This should be changed by the enemy that drops it.")]
    public Element pickUpElement;
    [Tooltip("How much is my my heal/mana multiplied by if my element matches the player?")]
    public float sameElementBonus = 2;

    private PlayerHealth playerHealth;//player health script for the player that picked me up

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        //if touching a player grant them health/mana.
        if (other.gameObject.tag == "Player")
        {
            playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if (other.gameObject.GetComponent<PlayerHealth>().element == pickUpElement)
            {
                heal = heal * sameElementBonus;
                mana = mana * sameElementBonus;
            }
            playerHealth.GetComponent<PlayerHealth>().Heal(heal);
            playerHealth.GetComponent<PlayerHealth>().GainMana(mana);
            Destroy(gameObject);
        }
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        playerHealth = other.GetComponent<PlayerHealth>();
    //        if (other.GetComponent<PlayerHealth>().element == pickUpElement)
    //        {
    //            heal = heal * sameElementBonus;
    //            mana = mana * sameElementBonus;
    //        }
    //        playerHealth.GetComponent<PlayerHealth>().Heal(heal);
    //        playerHealth.GetComponent<PlayerHealth>().GainMana(mana);
    //        Destroy(gameObject);
    //    }
    //}
}
