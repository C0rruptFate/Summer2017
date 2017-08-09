using System.Collections;
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

    //Controls
    private Player input_manager;
    public int player_id;

    //UI
    public Text levelName;
    public Image levelNameImage;
    public Color fire;//FF4500AA FF4500FF
    public Color earth;//6D5D49AA 6D5D49FF
    public Color air;//9C31F1AA FDD02380
    public Color water;//A5F2F3AA 2389DAFF 
    public Color dark; //9C31F1AA

    // Use this for initialization
    void Start () {
        wisp = GameObject.Find("Wisp Cursor");
        levelManager = GameObject.Find("Level Manager");
        input_manager = ReInput.players.GetPlayer(player_id);
    }
	
	// Update is called once per frame
	void Update () {
		
        //Look at the next level
        if (input_manager.GetAxis("move_horizontal") == 1 && Time.time >= nextMoveWait && target.GetComponent<LevelSelector>().nextLevel != null && target.GetComponent<LevelSelector>().nextLevel.GetComponent<LevelSelector>().isUnlocked)
        {
            FindNextLevel();
        }//Go back to the last level we were looking at
        else if (input_manager.GetAxis("move_horizontal") == -1 && Time.time >= nextMoveWait && target.GetComponent<LevelSelector>().previousLevel != null && target.GetComponent<LevelSelector>().previousLevel.GetComponent<LevelSelector>().isUnlocked)
        {
            FindPreviousLevel();
        }

        //Start the level
        if (input_manager.GetButtonDown("Jump"))
        {
            levelManager.GetComponent<LevelManager>().LoadLevel(target.GetComponent<LevelSelector>().levelName);
        }//Go back to the character select screen.
        else if (input_manager.GetButtonDown("Ranged"))
        {
            Debug.Log("Went back to Character Select");
            levelManager.GetComponent<LevelManager>().LoadLevel(levelManager.GetComponent<LevelManager>().previousLevel);
        }

    }

    void FindNextLevel()
    {
        target = target.GetComponent<LevelSelector>().nextLevel;
        wisp.transform.position = new Vector2(target.transform.position.x, target.transform.position.y + 5f);
        LevelName();
        nextMoveWait = Time.time + moveWait;
    }

    void FindPreviousLevel()
    {
        target = target.GetComponent<LevelSelector>().previousLevel;
        wisp.transform.position = new Vector2(target.transform.position.x, target.transform.position.y + 5f);
        LevelName();
        nextMoveWait = Time.time + moveWait;
    }

    void LevelName()
    {
        levelName.text = target.GetComponent<LevelSelector>().sceneName.ToString();

        switch (target.GetComponent<LevelLoader>().primeElement)
        {
            case Element.Fire:
                levelNameImage.color = fire;
                break;
            case Element.Earth:
                levelNameImage.color = earth;
                break;
            case Element.Wind:
                levelNameImage.color = air;
                break;
            case Element.Ice:
                levelNameImage.color = water;
                break;
            case Element.None:
                levelNameImage.color = dark;
                break;
        }
    }
}
