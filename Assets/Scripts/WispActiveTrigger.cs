using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WispActiveTrigger : MonoBehaviour {

    public GameObject gameManager;

    public GameObject wisp;

    public string microQuestText;

	// Use this for initialization
	void Start () {

        //wisp = gameManager.GetComponent<GameController>().wisp;

		if (wisp.GetComponent<WispScript>().wispActive)
        {
            DestroySelf();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
        if (wisp.GetComponent<WispScript>().moving && wisp.GetComponent<WispScript>().wispActive)
        {
            DestroySelf();
        }
        else if(wisp.GetComponent<WispScript>().moving)
        {
            wisp.GetComponent<WispScript>().moving = false;
        }

	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !wisp.GetComponent<WispScript>().wispActive)
        {
            wisp.GetComponent<WispScript>().wispActive = true;
            gameManager.GetComponent<GameController>().microQuestText.text = microQuestText;
        }
    }

    void DestroySelf()
    {
        if (gameManager.GetComponent<GameController>().microQuestText.text == microQuestText)
        {
            gameManager.GetComponent<GameController>().microQuestText.text = "";
        }
        Destroy(gameObject);
    }
}
