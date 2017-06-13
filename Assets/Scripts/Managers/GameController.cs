using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [Tooltip("Attach the pause text located on the CameraRig>UI>Pause Text!")]
    public Text pauseText;//Text displayed when a player pauses the game.

    [HideInInspector]//list of all players that joined the level.
    public GameObject[] players;

    //[HideInInspector]//Total count of all players that joined the level.
    public int totalPlayerCount;

    //[HideInInspector]//Total count of all living players.
    public int alivePlayerCount;

    private GameObject levelManager;//Object of the level manager so that we can load levels.

    // Use this for initialization
    void Awake () {
        //Sets gameobject and components.
        levelManager = GameObject.Find("Level Manager");
        levelManager.GetComponent<LevelManager>().SpawnPlayers();
        players = GameObject.FindGameObjectsWithTag("Player");
        totalPlayerCount = players.Length;
        //Counts how many players joined the game.
        alivePlayerCount = totalPlayerCount;
        //for (int i = 0; i < totalPlayerCount; i++)
        //{
        //    RaisePlayerCount();
        //    //Debug.Log("Total Player Count " + totalPlayers);
        //}

        //Makes sure the pause text isn't enabled.
        pauseText.enabled = false;

        //if pause text isn't set up lets the designers know.
        if (pauseText == null)
        {
            Debug.Log("Attach 'Pause Text' from the UI");
        }
    }
	
	// Update is called once per frame
    void Update()
    {
        //Allows for a global pause [TODO] set up per player pause.
        if (Input.GetButtonDown("Pause"))
        {
            Pause();
        }

        //Quits the game, [TODO] remove redunency of levelmanager and game manager and put in a confermation when quit is clicked. Attach this to pause.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void Pause() //Currently this can be paused/unpaused by anyone, need to set up per player pause.
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
        alivePlayerCount--;
        if (alivePlayerCount <= 0)
        {
            Debug.Log("All Players dead");
            Invoke("GameOver", 3);//[TODO] Set up game over time and effect then change this # to match that length.
        }
    }

    public void RaisePlayerCount() //Called when a player is revived.
    {
        alivePlayerCount++;
    }

    public void GameOver() //Called when all players are dead at the same time
    {
        levelManager.GetComponent<LevelManager>().LoadLevel("GameOver");
    }

    public void BeatLevel()
    {
        //[TODO] change to a beat level screen or the next level.
        levelManager.GetComponent<LevelManager>().LoadLevel("GameOver");
    }
}
