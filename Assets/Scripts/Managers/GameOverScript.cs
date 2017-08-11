using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour {

    private GameObject levelManager;
    private LevelManager levelManagerScript;
    
	// Use this for initialization
	void Start () {
        levelManager = GameObject.Find("Level Manager");
        levelManagerScript = levelManager.GetComponent<LevelManager>();
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Submit"))
        {
            levelManagerScript.LoadLevel(levelManagerScript.previousScene, levelManagerScript.playableLevelIndex);
        }

        if (Input.GetButtonDown("Cancel"))
        {
            levelManagerScript.LoadScene("MainMenu");
        }
    }
}
