using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionManager : MonoBehaviour {

    //[HideInInspector]
    //public int playerCount;
    [HideInInspector]
    public int playersReady;
    [HideInInspector]
    public bool readyToStart = false;
    [HideInInspector]
    public int countDown;


    public Text readyText;
    public string levelToLoad;
    public int loadLevelWaitTime;
    private GameObject levelManager;
    private IEnumerator coroutine;


    // Use this for initialization
    void Start () {
        Constants.playerCount = 0;
        levelManager = GameObject.Find("Level Manager");
        readyText.text = "";
        countDown = loadLevelWaitTime;
        coroutine = CountDown();
	}
	
	// Update is called once per frame
	void Update () {
		
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

        if(readyToStart)
        {
        }
	}

    void CharactersSelect()
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

    private IEnumerator CountDown()
    {
        while(countDown > 0)
        {
            countDown--;
            yield return new WaitForSeconds(1.0f);
            if (countDown == 0)
            {
                CharactersSelect();
            }
        }
    }
}
