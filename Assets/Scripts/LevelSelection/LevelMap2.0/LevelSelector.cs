using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour {

    [Tooltip("Level to load in the scene manager 'use flavorful text' ")]
    public string sceneName;
    [Tooltip("my level ID name 'ex. zoneALevel1' ")]
    public string levelIDName;
    [Tooltip("what Zone this is a part of 'A, B, C, D' ")]
    public string zone;
    [Tooltip("The prime element for this level.")]
    public Element primeElement;
    public bool isLevelOne = false;
    public int killCount;

    public GameObject previousLevel;
    public GameObject nextLevel;
    public GameObject myBossLevel;

    public bool isUnlocked;
    public bool isBeaten;
    public int enemiesSlain;

    private GameObject levelManager;

    public PlayerDictionary playerDictionary;

    // Use this for initialization
    void Start () {
        levelManager = GameObject.Find("Level Manager");
        playerDictionary = levelManager.GetComponent<PlayerDictionary>();
        levelIDName = gameObject.name;
        zone = myBossLevel.GetComponent<BossLevelSelector>().bossZone;

        /////////////////////
        //Debug.Log("index count: " + playerDictionary.levelBeat.Count + " level ID Name: " + levelIDName + " level ID bool value: " + playerDictionary.CheckLevelBeat(levelIDName));

        if (playerDictionary.levelBeat.ContainsKey(levelIDName))
        {
            //Debug.Log("level ID name " + levelIDName);
            isBeaten = playerDictionary.CheckLevelBeat(levelIDName);
            GetComponent<IsUnlocked>().isBeaten = isBeaten;
            killCount = playerDictionary.CheckLevelKills(levelIDName);
        }
        else
        {
            Debug.Log("Couldn't find level ID name " + levelIDName);
        }


        //isBeaten = playerDictionary.levelBeat[levelIDName];
        //////////////////////
        //Debug.Log("Enemy Kills: " + playerDictionary.CheckLevelKills(levelIDName));
        enemiesSlain = playerDictionary.CheckLevelKills(levelIDName);

        // Check to see if my previous level has been beaten
        if (isLevelOne)
        {
            CheckUnlock();
        }
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void UpdateEnemiesSlain(int newEmemiesSlain)
    {
        if (newEmemiesSlain > enemiesSlain)
        {
            enemiesSlain = newEmemiesSlain;
        }
    }

    public void CheckUnlock()
    {
        if (previousLevel == null || previousLevel.GetComponent<IsUnlocked>().isUnlocked)
        {
            isUnlocked = true;
            GetComponent<IsUnlocked>().isUnlocked = isUnlocked;
            if (nextLevel.GetComponent<LevelSelector>() != null)
            {
                nextLevel.GetComponent<LevelSelector>().CheckUnlock();
            }
            
        }

        if (gameObject.name == "zoneALevel3")
        {
            Debug.Log(playerDictionary.CheckZoneClearCount(zone) +" " + zone);
        }

        if (playerDictionary.CheckZoneClearCount(zone) >= myBossLevel.GetComponent<BossLevelSelector>().requiredLevelBeatCount && !myBossLevel.GetComponent<BossLevelSelector>().bossUnlocked)
        {
            myBossLevel.GetComponent<BossLevelSelector>().UnlockBossLevel();
        }
    }
}
