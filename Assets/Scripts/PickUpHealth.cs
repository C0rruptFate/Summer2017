using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHealth : MonoBehaviour {

    [Tooltip("How much does this heal for?")]
    public float heal;
    [Tooltip("How much mana does this give?")]
    public float mana;

    [Tooltip("What element am I? This should be changed by the enemy that drops it.")]
    public Element myElement;
    [Tooltip("How much is my my heal/mana multiplied by if my element matches the player?")]
    public float sameElementBonus = 2;

    private Component playerScript;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerScript = other.GetComponent<PlayerHealth>();
            if (other.GetComponent<PlayerHealth>().element == myElement)
            {
                heal = heal * sameElementBonus;
                mana = mana * sameElementBonus;
            }
            playerScript.GetComponent<PlayerHealth>().Heal(heal);
            playerScript.GetComponent<PlayerHealth>().GainMana(mana);
            Destroy(gameObject);
        }
    }
}
