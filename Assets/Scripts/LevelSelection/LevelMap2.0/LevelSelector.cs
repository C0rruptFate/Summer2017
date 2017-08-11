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


    public GameObject previousLevel;
    public GameObject nextLevel;
    public GameObject myBossLevel;

    public bool isUnlocked;
    public bool isBeaten;
    public int enemiesSlain;

    private GameObject levelManager;

    private PlayerDictionary playerDictionary;

    // Use this for initialization
    void Start () {
        levelManager = GameObject.Find("Level Manager");
        playerDictionary = levelManager.GetComponent<PlayerDictionary>();
        levelIDName = gameObject.name;

        /////////////////////
        //Debug.Log("index count: " + playerDictionary.levelBeat.Count + " level ID Name: " + levelIDName + " level ID bool value: " + playerDictionary.CheckLevelBeat(levelIDName));

        if (playerDictionary.levelBeat.ContainsKey(levelIDName))
        {
            //Debug.Log("level ID name " + levelIDName);
            isBeaten = playerDictionary.CheckLevelBeat(levelIDName);
        }
        else
        {
            Debug.Log("Couldn't find level ID name " + levelIDName);
        }


        //isBeaten = playerDictionary.levelBeat[levelIDName];
        //////////////////////
        //Debug.Log("Enemy Kills: " + playerDictionary.CheckLevelKills(levelIDName));
        enemiesSlain = playerDictionary.CheckLevelKills(levelIDName);

        // If I am not a boss level check to see if my previous level has been beaten
        CheckUnlock();
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

    void CheckUnlock()
    {
        if (previousLevel == null || previousLevel.GetComponent<IsUnlocked>().isBeaten)
        {
            isUnlocked = true;
        }
        if (playerDictionary.CheckZoneClearCount(zone) >= myBossLevel.GetComponent<BossLevelSelector>().requiredLevelBeatCount && !myBossLevel.GetComponent<BossLevelSelector>().bossUnlocked)
        {
            myBossLevel.GetComponent<BossLevelSelector>().bossUnlocked = true;
        }
    }
}
