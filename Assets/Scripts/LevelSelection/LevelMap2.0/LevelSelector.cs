using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour {

    public string sceneName;
    public bool isUnlocked;
    public bool isBeaten;
    public bool bossLevel;
    public int enemiesSlain;
    public int beatTokensGathered;
    public int beatTokensRequired;

    public GameObject previousLevel;
    public GameObject nextLevel;

    public string levelName;

    private GameObject levelManager;

	// Use this for initialization
	void Start () {
        levelManager = GameObject.Find("Level Manager");
        isBeaten = levelManager.GetComponent<PlayerData>().levelData[levelName];
        enemiesSlain = levelManager.GetComponent<PlayerData>().levelKills[enemiesSlain];

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
        if (!bossLevel && (previousLevel.GetComponent<LevelSelector>().isBeaten || previousLevel == null))
        {
            isUnlocked = true;
        }
        else if (bossLevel && beatTokensGathered >= beatTokensRequired)
        {
            isUnlocked = true;
        }
    }
}
