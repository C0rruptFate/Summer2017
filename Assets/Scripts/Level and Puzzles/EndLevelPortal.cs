using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelPortal : MonoBehaviour {

    [HideInInspector]
    public int playersOnPortal;

    [HideInInspector]
    public GameObject gameManager;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("Game Manager");
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.GetComponent<PlayerHealth>() != null || other.transform.GetComponent<EnemyHealth>() != null)
        {
            playersOnPortal++;
            gameManager.GetComponent<GameController>().playersOnPortal = playersOnPortal;
            gameManager.GetComponent<GameController>().LevelProgression();
            if (playersOnPortal >= gameManager.GetComponent<GameController>().totalPlayerCount)
            {
                gameManager.GetComponent<GameController>().BeatLevel();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.GetComponent<PlayerHealth>() != null || other.transform.GetComponent<EnemyHealth>() != null)
        {
            playersOnPortal--;
            gameManager.GetComponent<GameController>().playersOnPortal = playersOnPortal;
        }
    }
}
