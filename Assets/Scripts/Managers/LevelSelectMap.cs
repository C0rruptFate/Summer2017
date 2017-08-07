using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class LevelSelectMap : MonoBehaviour {

    public GameObject[] levelList;
    public GameObject pathEffect;
    public GameObject unlockEffect;
    public float effectDuration = 3;
    [SerializeField]
    private int arrayPosition = 0;
    public int player_id;

    public float speed;

    //[HideInInspector]
    public GameObject wisp;

    private Player input_manager;

    [HideInInspector]//Use to find what direction the player is trying to move
    public float horizontalDir;

    [HideInInspector]//Use to find what direction the player is trying to move
    public float verticalDir;

    public GameObject target;

    public float moveWait;
    private float nextMoveWait;

    private GameObject levelManager;

    private int levelIndex;

    void Awake()
    {

    }

    // Use this for initialization
    void Start()
    {
        wisp = GameObject.Find("Wisp Cursor");
        levelManager = GameObject.Find("Level Manager");
        levelIndex = levelManager.GetComponent<LevelManager>().levelIndex;
        int i = 0;
        while (i <= levelIndex)
        {
            if (!levelList[i].GetComponent<LevelLoader>().unlocked)
            {
                //[TODO] play unlock animation (ask kyle because the object does get destroyed)
                if (i != 0)
                {
                    GameObject myPathEffect = Instantiate(pathEffect, levelList[i - 1].transform.position, pathEffect.transform.rotation);
                    myPathEffect.GetComponent<LevelMapPathEffect>().point2 = levelList[i - 1].transform;
                    myPathEffect.GetComponent<LevelMapPathEffect>().point1 = levelList[i].transform;
                }
                if (i == levelIndex)
                {
                    GameObject myUnlockEffect = Instantiate(unlockEffect, new Vector2(levelList[i].transform.position.x, levelList[i].transform.position.y + 1.5f), unlockEffect.transform.rotation);
                    myUnlockEffect.GetComponent<DestroySelfRightAway>().waitToDestroyTime = effectDuration;
                }

                levelList[i].GetComponent<LevelLoader>().UnlockTorch();
            }
            i++;
        }
        input_manager = ReInput.players.GetPlayer(player_id);
        target = levelList[levelIndex];
        arrayPosition = levelIndex;
        wisp.transform.position = new Vector2(target.transform.position.x, target.transform.position.y + 5f);
        levelManager.GetComponent<LevelManager>().levelSelectMap = gameObject;
    }
	
	// Update is called once per frame
	void Update () {

        //Move to the next level up
        if (input_manager.GetAxis("move_horizontal") == 1 && Time.time >= nextMoveWait && levelList[arrayPosition + 1].GetComponent<LevelLoader>().unlocked && arrayPosition < levelList.Length)
        {
            FindNextLevel();
        }
        else if (input_manager.GetAxis("move_horizontal") == -1 && Time.time >= nextMoveWait && (arrayPosition - 1 >= 0))
        {
            FindPreviousLevel();
        }

        if (input_manager.GetButtonDown("Jump"))
        {
            //[TODO]Load the targeted level
            levelManager.GetComponent<LevelManager>().playableLevelIndex = arrayPosition;
            levelManager.GetComponent<LevelManager>().LoadLevel(target.GetComponent<LevelLoader>().sceneName);
        }
    }

    void FindNextLevel()
    {
        arrayPosition++;
        target = levelList[arrayPosition];
        wisp.transform.position = new Vector2(target.transform.position.x, target.transform.position.y + 5f);
        //wisp.transform.position = Vector2.MoveTowards(wisp.transform.position, levelList[arrayPosition].transform.position,speed);
        nextMoveWait = Time.time + moveWait;
    }

    void FindPreviousLevel()
    {
        arrayPosition--;
        target = levelList[arrayPosition];
        wisp.transform.position = new Vector2(target.transform.position.x, target.transform.position.y + 5f);
        //wisp.transform.position = Vector2.MoveTowards(wisp.transform.position, levelList[arrayPosition].transform.position, speed);
        nextMoveWait = Time.time + moveWait;
    }
}
