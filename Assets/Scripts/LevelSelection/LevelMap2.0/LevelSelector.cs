using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour {

    public string sceneName;
    public bool isUnlocked;
    public bool isBeaten;
    public int enemiesSlain;

    public GameObject previousLevel;
    public GameObject nextLevel;

    public string levelName;

    private GameObject levelManager;

	// Use this for initialization
	void Start () {
        levelManager = GameObject.Find("Level Manager");
        isBeaten = levelManager.GetComponent<PlayerData>().levelData[levelName];
        enemiesSlain = levelManager.GetComponent<PlayerData>().levelKills[enemiesSlain];

        if (previousLevel.GetComponent<LevelSelector>().isBeaten || previousLevel == null)
        {
            isUnlocked = true;
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
}
