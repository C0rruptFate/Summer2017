using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [Tooltip("Attach the pause text located on the UI!")]
    public Text pauseText;

    [HideInInspector]
    public GameObject[] players;

    [HideInInspector]
    public int totalPlayers;

    private GameObject levelManager;

    // Use this for initialization
    void Awake () {

        levelManager = GameObject.Find("Level Manager");
        levelManager.GetComponent<LevelManager>().SpawnPlayers();
        players = GameObject.FindGameObjectsWithTag("Player");

        pauseText.enabled = false;
        if (pauseText == null)
        {
            Debug.Log("Attach 'Pause Text' from the UI");
        }

        for (int i = 0; i < players.Length; i++)
        {
            RaisePlayerCount();
            //Debug.Log("Total Player Count " + totalPlayers);
        }
    }
	
	// Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            Pause();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void Pause() //Currently this can be paused/unpaused by anyone
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            //Display pause text
            pauseText.enabled = true;
        }
        else
        {
            Time.timeScale = 1;
            //Disable Pause Text
            pauseText.enabled = false;
        }
    }
    public void LowerPlayerCount() //Called when a player dies
    {
        totalPlayers--;
        if (totalPlayers == 0)
        {
            Debug.Log("All Players dead");
            Invoke("GameOver", 5);
        }
    }

    public void RaisePlayerCount() //Called when a player is revived
    {
        totalPlayers++;
    }

    void GameOver() //Called when all players are dead at the same time
    {
        levelManager.GetComponent<LevelManager>().LoadLevel("GameOver");
    }
}
