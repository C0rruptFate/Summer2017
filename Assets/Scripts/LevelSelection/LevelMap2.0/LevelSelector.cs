using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour {

    public bool isUnlocked;
    public bool isBeaten;
    public int enemiesSlain;

    public GameObject previousLevel;
    public GameObject nextLevel;

    public string levelName;

	// Use this for initialization
	void Start () {
		
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
