using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsUnlocked : MonoBehaviour {

    public bool isUnlocked;
    public bool isBeaten;
    public GameObject previousLevel;
    public GameObject nextLevel;

	// Use this for initialization
	void Start () {

        //Finds the previous and next levels
        if (GetComponent<LevelSelector>() != null)
        {
            previousLevel = GetComponent<LevelSelector>().previousLevel;
            nextLevel = GetComponent<LevelSelector>().nextLevel;
            isUnlocked = GetComponent<LevelSelector>().isUnlocked;
            isBeaten = GetComponent<LevelSelector>().isBeaten;
        }
        else
        {
            previousLevel = GetComponent<BossLevelSelector>().previousLevel;
            nextLevel = GetComponent<BossLevelSelector>().nextLevel;
            isUnlocked = GetComponent<BossLevelSelector>().bossUnlocked;
            isBeaten = GetComponent<BossLevelSelector>().bossBeaten;
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
