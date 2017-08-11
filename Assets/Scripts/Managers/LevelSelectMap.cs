using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.UI;

public class LevelSelectMap : MonoBehaviour {

    public GameObject[] levelList;
    public GameObject pathEffect;
    public GameObject unlockEffect;
    public float effectDuration = 3;
    [SerializeField]
    private int arrayPosition = 0;
    public int player_id;

    public Color fire;//FF4500AA FF4500FF
    public Color earth;//6D5D49AA 6D5D49FF
    public Color air;//9C31F1AA FDD02380
    public Color water;//A5F2F3AA 2389DAFF 
    public Color dark; //9C31F1AA

    public float speed;

    //[HideInInspector]
    public GameObject wisp;

    public Text levelName;
    public Image levelNameImage;

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
        //levelName = GameObject.Find("Level Name Text").GetComponent<Text>();
        //levelNameImage = GameObject.Find("Level Name Text").GetComponent<Image>();

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
        LevelName();
    }
	
	// Update is called once per frame
	void Update () {

        //Move to the next level up
        if (input_manager.GetAxis("move_horizontal") == 1 && Time.time >= nextMoveWait && levelList[arrayPosition + 1].GetComponent<LevelLoader>().unlocked && arrayPosition < levelList.Length && !levelList[arrayPosition + 1].GetComponent<LevelLoader>().isBonusLevel)
        {
            Debug.Log("List Length " + levelList.Length);
            FindNextLevel();
        }
        else if (input_manager.GetAxis("move_horizontal") == -1 && Time.time >= nextMoveWait && (arrayPosition - 1 >= 0 && !levelList[arrayPosition + 1].GetComponent<LevelLoader>().isBonusLevel))
        {
            FindPreviousLevel();
        }
        else if (target.GetComponent<LevelLoader>().bonusLevel != null && input_manager.GetAxis("move_vertical") == 1 && Time.time >= nextMoveWait)
        {
            TargetBonusLevel();
        }
        else if (target.GetComponent<LevelLoader>().isBonusLevel && input_manager.GetAxis("move_vertical") == -1 && Time.time >= nextMoveWait)
        {
            TargetPreviousLevel();
        }

        if (input_manager.GetButtonDown("Jump"))
        {
            //Uncomment if using the older level select model.
            //levelManager.GetComponent<LevelManager>().playableLevelIndex = arrayPosition;
            //levelManager.GetComponent<LevelManager>().playingBonusLevel = target.GetComponent<LevelLoader>().isBonusLevel;
            //levelManager.GetComponent<LevelManager>().LoadLevel(target.GetComponent<LevelLoader>().sceneName, target.GetComponent<LevelLoader>().lev);
        }

        if (input_manager.GetButtonDown("Ranged"))
        {
            Debug.Log("Went back to Character Select");
            //levelManager.GetComponent<LevelManager>().LoadLevel(levelManager.GetComponent<LevelManager>().previousScene);
        }
    }

    void FindNextLevel()
    {
        arrayPosition++;
        target = levelList[arrayPosition];
        wisp.transform.position = new Vector2(target.transform.position.x, target.transform.position.y + 5f);
        LevelName();
        //wisp.transform.position = Vector2.MoveTowards(wisp.transform.position, levelList[arrayPosition].transform.position,speed);
        nextMoveWait = Time.time + moveWait;
    }

    void FindPreviousLevel()
    {
        arrayPosition--;
        target = levelList[arrayPosition];
        wisp.transform.position = new Vector2(target.transform.position.x, target.transform.position.y + 5f);
        LevelName();
        //wisp.transform.position = Vector2.MoveTowards(wisp.transform.position, levelList[arrayPosition].transform.position, speed);
        nextMoveWait = Time.time + moveWait;
    }

    void TargetBonusLevel()
    {
        target = target.GetComponent<LevelLoader>().bonusLevel;
        wisp.transform.position = new Vector2(target.transform.position.x, target.transform.position.y + 5f);
        //arrayPosition = arrayPosition - 1;
        LevelName();
        nextMoveWait = Time.time + moveWait;
    }

    void TargetPreviousLevel()
    {
        target = target.GetComponent<LevelLoader>().previousLevel;
        wisp.transform.position = new Vector2(target.transform.position.x, target.transform.position.y + 5f);
        LevelName();
        nextMoveWait = Time.time + moveWait;
    }

    void LevelName()
    {
        levelName.text = target.GetComponent<LevelLoader>().sceneName.ToString();

        switch(target.GetComponent<LevelLoader>().primeElement)
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
