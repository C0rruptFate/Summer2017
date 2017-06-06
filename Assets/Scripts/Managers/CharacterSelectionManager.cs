using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionManager : MonoBehaviour {

    //[HideInInspector]
    //public int playerCount;
    [HideInInspector]//counts how many players are ready.
    public int playersReady;
    [HideInInspector]//Starts the countdown to begin the level.
    public bool readyToStart = false;
    [HideInInspector]//How many seconds it takes to start the game.
    public int countDown;

    [Tooltip("The countdown text when all players are locked in.")]
    public Text readyText;
    [Tooltip("The name of the level to load, this might end up as a level selection screen.")]
    public string levelToLoad;
    [Tooltip("How long until the level is loaded. This will be the countdown time.")]
    public int loadLevelWaitTime;
    private GameObject levelManager;//The level manager so that the this can pass the characters through to them.
    private IEnumerator coroutine;//The count down that resets when a player deselects or joins the lobby.


    // Use this for initialization
    void Start () {
        //Sets everything up so that the players can select their characters.
        Constants.playerCount = 0;
        levelManager = GameObject.Find("Level Manager");
        readyText.text = "";
        countDown = loadLevelWaitTime;
        coroutine = CountDown();
	}
	
	// Update is called once per frame
	void Update () {
		//if all active players have selected their character then start the count down.
        if(Constants.playerCount >= 1 && Constants.playerCount == playersReady)
        {
            if (!readyToStart)
            {
                readyToStart = true;
                StartCoroutine(coroutine);
            }
            readyText.text = "Starting in " + countDown;
        }
        else
        {
            StopCoroutine(coroutine);
            readyToStart = false;
            readyText.text = "";
            countDown = loadLevelWaitTime;
        }
	}

    void CharactersSelect()//Sends the selected player elements to the level manager so that it knows what to spawn.
    {
        if (Constants.playerCount >= 1 && Constants.playerCount == playersReady)
        {
            levelManager.GetComponent<LevelManager>().player1Element = GameObject.Find("Player 1 Cursor").GetComponent<Cursor>().element;
            levelManager.GetComponent<LevelManager>().player2Element = GameObject.Find("Player 2 Cursor").GetComponent<Cursor>().element;
            levelManager.GetComponent<LevelManager>().player3Element = GameObject.Find("Player 3 Cursor").GetComponent<Cursor>().element;
            levelManager.GetComponent<LevelManager>().player4Element = GameObject.Find("Player 4 Cursor").GetComponent<Cursor>().element;
            //Debug.Log("About to load level");
            levelManager.GetComponent<LevelManager>().LoadLevel(levelToLoad);
        }
    }

    private IEnumerator CountDown()//The Count down used when all active players have locked in their characters.
    {
        while(countDown > 0)
        {
            countDown--;
            yield return new WaitForSeconds(1.0f);
            if (countDown == 0)
            {
                CharactersSelect();
                yield return new WaitForSeconds(1.0f);
                levelManager.GetComponent<LevelManager>().SpawnPlayers();
            }
        }
    }
}
