using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePlayerSpecials : MonoBehaviour {

    [SerializeField]
    private GameObject GM;

    [SerializeField]
    private float waitTime = 5;
    private float removeTextTime;

    private bool taughtSpecials;

	// Use this for initialization
	void Start () {

        foreach (GameObject player in GM.GetComponent<GameController>().players)
        {
            player.GetComponent<PlayerAttacks>().learnedSpecial = false;
        }
        
	}
	
	// Update is called once per frame
	void Update () {
		
        if (taughtSpecials && removeTextTime < Time.time && GM.GetComponent<GameController>().microQuestText.text == "Press RT or RB along with attack or defend to preform a special.")
        {
            GM.GetComponent<GameController>().microQuestText.text = "";
        }

	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wisp"))
        {
            foreach (GameObject player in GM.GetComponent<GameController>().players)
            {
                player.GetComponent<PlayerAttacks>().learnedSpecial = true;
            }
            GM.GetComponent<GameController>().microQuestText.text = "Press RT or RB along with attack or defend to preform a special.";
            removeTextTime = Time.time + waitTime;
            taughtSpecials = true;
        }
    }
}
