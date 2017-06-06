using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour {

    private GameObject levelManager;
    private LevelManager levelManagerScript;
    private string previousLevel;


	// Use this for initialization
	void Start () {
        levelManager = GameObject.Find("Level Manager");
        levelManagerScript = levelManager.GetComponent<LevelManager>();
        previousLevel = levelManagerScript.previousLevel;
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Submit"))
        {
            levelManagerScript.LoadLevel(previousLevel);
        }

        if (Input.GetButtonDown("Cancel"))
        {
            levelManagerScript.LoadLevel("MainMenu");
        }
    }
}
