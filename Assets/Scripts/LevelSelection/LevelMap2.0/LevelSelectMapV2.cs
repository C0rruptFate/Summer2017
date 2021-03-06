﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.UI;

public class LevelSelectMapV2 : MonoBehaviour {

    //Map Movement
    public GameObject wisp;
    public GameObject target;
    public float moveWait = 0.1f;
    private float nextMoveWait;

    //Objects
    public GameObject levelManager;
    public GameObject[] bossLevels;

    //Controls
    private Player input_manager1;
    private Player input_manager2;
    private Player input_manager3;
    private Player input_manager4;
    //public int player_id;

    //UI
    public Text levelName;
    public Image levelNameImage;
    public Color fire;//FF450080
    public Color earth;// 6D5D4980
    public Color air;// FDD02380
    public Color water;//2389DA80
    public Color dark; //9C31F180
    public Text statsText;

    private string characterSelectionScreen = "Main Menu";
    //private PlayerDictionary playerDictionary; Might need to reenable

    // Use this for initialization
    void Start () {
        wisp = GameObject.Find("Wisp Cursor");
        levelManager = GameObject.Find("Level Manager");
        //playerDictionary = levelManager.GetComponent<PlayerDictionary>();
        input_manager1 = ReInput.players.GetPlayer(0);
        input_manager2 = ReInput.players.GetPlayer(1);
        input_manager3 = ReInput.players.GetPlayer(2);
        input_manager4 = ReInput.players.GetPlayer(3);
        //Tell the wisp to target the highest value target it can find. Maybe it finds it target from the level manager string or something that tracks what it just beat.
        if (levelManager.GetComponent<LevelManager>().playableLevelIndex != "")
        {
            target = GameObject.Find(levelManager.GetComponent<LevelManager>().playableLevelIndex);
        }
        else
        {
            target = GameObject.Find("zoneALevel1");
        }
        if (target.name.Contains("Boss"))
        {
            wisp.transform.position = new Vector2(target.transform.position.x, target.transform.position.y + 7.5f);
            Debug.Log("On a Boss level");
        }
        else
        {
            wisp.transform.position = new Vector2(target.transform.position.x, target.transform.position.y + 5f);
        }
        
        LevelName();

        foreach(GameObject bossLevel in bossLevels)
        {
            bossLevel.GetComponent<BossLevelSelector>().playerDictionary = levelManager.GetComponent<PlayerDictionary>();
            bossLevel.GetComponent<BossLevelSelector>().CheckBossLevelBeaten();
        }


    }
	
	// Update is called once per frame
	void Update () {
		
        //Look at the next level
        if ((input_manager1.GetAxis("move_horizontal") == 1 || input_manager2.GetAxis("move_horizontal") == 1 || input_manager3.GetAxis("move_horizontal") == 1|| input_manager4.GetAxis("move_horizontal") == 1) && Time.time >= nextMoveWait && target.GetComponent<IsUnlocked>().nextLevel != null && target.GetComponent<IsUnlocked>().nextLevel.GetComponent<IsUnlocked>().isUnlocked == true)
        {
                FindNextLevel();
        }//Go back to the last level we were looking at
        else if ((input_manager1.GetAxis("move_horizontal") == -1 || input_manager2.GetAxis("move_horizontal") == -1 || input_manager3.GetAxis("move_horizontal") == -1 || input_manager4.GetAxis("move_horizontal") == -1) && Time.time >= nextMoveWait && target.GetComponent<IsUnlocked>().previousLevel != null && target.GetComponent<IsUnlocked>().previousLevel.GetComponent<IsUnlocked>().isUnlocked == true)

        {
            FindPreviousLevel();
        }

        //Start the level
        if (input_manager1.GetButtonDown("Jump") || input_manager2.GetButtonDown("Jump") || input_manager3.GetButtonDown("Jump") || input_manager4.GetButtonDown("Jump"))
        {
            if (target.GetComponent<LevelSelector>() != null)
            {
                levelManager.GetComponent<LevelManager>().LoadLevel(target.GetComponent<LevelSelector>().sceneName, target.GetComponent<LevelSelector>().levelIDName, target.GetComponent<LevelSelector>().zone, false);
            }
            else if(target.GetComponent<BossLevelSelector>() != null)
            {
                levelManager.GetComponent<LevelManager>().LoadLevel(target.GetComponent<BossLevelSelector>().sceneName, target.GetComponent<BossLevelSelector>().levelIDName, target.GetComponent<BossLevelSelector>().bossZone, true);
            }
            else
            {
                Debug.LogError("Target is missing it's level Selector or boss level selector");
            }
        }//Go back to the character select screen.
        else if (input_manager1.GetButtonDown("Ranged") || input_manager2.GetButtonDown("Ranged") || input_manager3.GetButtonDown("Ranged") || input_manager4.GetButtonDown("Ranged"))
        {
            Debug.Log("Went back to Character Select");
            levelManager.GetComponent<LevelManager>().LoadScene(characterSelectionScreen);
        }

    }

    void FindNextLevel()
    {
        if (target.GetComponent<LevelSelector>() != null)
        {
            target = target.GetComponent<LevelSelector>().nextLevel;
        }
        else if(target.GetComponent<BossLevelSelector>() != null)
        {
            target = target.GetComponent<BossLevelSelector>().nextLevel;
        }
        else
        {
            Debug.LogError("Target is missing it's level Selector or boss level selector");
        }

        if (target.name.Contains("Boss"))
        {
            wisp.transform.position = new Vector2(target.transform.position.x, target.transform.position.y + 7.5f);
            Debug.Log("On a Boss level");
        }
        else
        {
            wisp.transform.position = new Vector2(target.transform.position.x, target.transform.position.y + 5f);
        }
        LevelName();
        nextMoveWait = Time.time + moveWait;
    }

    void FindPreviousLevel()
    {
        if (target.GetComponent<LevelSelector>() != null)
        {
            target = target.GetComponent<LevelSelector>().previousLevel;
        }
        else if (target.GetComponent<BossLevelSelector>() != null)
        {
            target = target.GetComponent<BossLevelSelector>().previousLevel;
        }
        else
        {
            Debug.LogError("Target is missing it's level Selector or boss level selector");
        }

        if (target.name.Contains("Boss"))
        {
            wisp.transform.position = new Vector2(target.transform.position.x, target.transform.position.y + 7.5f);
            Debug.Log("On a Boss level");
        }
        else
        {
            wisp.transform.position = new Vector2(target.transform.position.x, target.transform.position.y + 5f);
        }
        LevelName();
        nextMoveWait = Time.time + moveWait;
    }

    void LevelName()
    {
        if (target.GetComponent<LevelSelector>() != null)
        {
            levelName.text = target.GetComponent<LevelSelector>().sceneName.ToString();
            statsText.text = "Primary Element: " + target.GetComponent<LevelSelector>().primeElement + "\nHighest Kills: " + target.GetComponent<LevelSelector>().killCount;
            switch (target.GetComponent<LevelSelector>().primeElement)
            {
                case Element.Fire:
                    levelNameImage.color = fire;
                    break;
                case Element.Earth:
                    levelNameImage.color = earth;
                    break;
                case Element.Air:
                    levelNameImage.color = air;
                    break;
                case Element.Water:
                    levelNameImage.color = water;
                    break;
                case Element.None:
                    levelNameImage.color = dark;
                    break;
            }
        }
        else if (target.GetComponent<BossLevelSelector>() != null)
        {
            levelName.text = target.GetComponent<BossLevelSelector>().sceneName.ToString();

            if (target.GetComponent<BossLevelSelector>().bossBeaten)
            {
                statsText.text = target.GetComponent<BossLevelSelector>().bossName + " Has been defeated \nthis area has been cleansed!";
            }
            else
            {
                statsText.text = target.GetComponent<BossLevelSelector>().bossName + " is ready to be challenged.";
            }

            switch (target.GetComponent<BossLevelSelector>().primeElement)
            {
                case Element.Fire:
                    levelNameImage.color = fire;
                    break;
                case Element.Earth:
                    levelNameImage.color = earth;
                    break;
                case Element.Air:
                    levelNameImage.color = air;
                    break;
                case Element.Water:
                    levelNameImage.color = water;
                    break;
                case Element.None:
                    levelNameImage.color = dark;
                    break;
            }
        }


    }
}
