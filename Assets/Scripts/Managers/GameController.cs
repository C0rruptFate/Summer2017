using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public int levelIndex;
    public string levelIndexName;
    public bool isBonusLevel = false;

    [Tooltip("Attach the pause text located on the CameraRig>UI>Pause Text!")]
    [HideInInspector]
    public Text pauseText;//Text displayed when a player pauses the game.

    //End level requirments.
    //[HideInInspector]
    public BeatLevelCondition beatLevelCondition;
    [HideInInspector]
    public Text questText; //The quest text in the top right corner.
    [HideInInspector]
    public Text microQuestText;
    [HideInInspector] //The boss if one is in this level
    public GameObject boss;
    private int enemyDeathCount;
    [Tooltip("Only used if beat level condition is set to Kill X Enemies.")]
    public int requiredEnemyDeathCount;
    [Tooltip("Only used if the Macro quest is to kill this enemy type.")]
    public EnemyTypes enemyTypes;
    [HideInInspector]
    public int uniqueEnemiesKilled;
    public int requiredUniqueEnemies;
    [HideInInspector]//How many enemies need to be killed to clear the level.
    public int numberOfSwitchesToPush;
    [HideInInspector]
    public int currentNumberOfSwitchesToPush;
    [HideInInspector]//How many torches need to be activated.
    public int numberOfTorchesToActivate;
    [HideInInspector]
    public int currentNumberofTorchesActive;
    [HideInInspector]
    public int playersOnPortal;

    [Tooltip("The first string of text used in the quest text if one is being used.")]
    public string string1;
    [Tooltip("The second string of text used in the quest text if one is being used.")]
    public string string2;

    [HideInInspector]//list of all players that joined the level.
    public GameObject[] players;

    [HideInInspector]//Total count of all players that joined the level.
    public int totalPlayerCount;

    [HideInInspector]//Total count of all living players.
    public int alivePlayerCount;

    private GameObject levelManager;//Object of the level manager so that we can load levels.
    private bool gamePaused = false;
    [HideInInspector]
    public GameObject wisp;

    //Respawn wait time
    public int respawnTime = 30;
    
    private int player1CurrentRespawnCount;
    private int player2CurrentRespawnCount;
    private int player3CurrentRespawnCount;
    private int player4CurrentRespawnCount;

    private GameObject player1ReadyToRespawn;
    private GameObject player2ReadyToRespawn;
    private GameObject player3ReadyToRespawn;
    private GameObject player4ReadyToRespawn;

    // Use this for initialization
    void Awake()
    {
        //Sets gameobject and components.
        levelManager = GameObject.Find("Level Manager");
        levelIndex = levelManager.GetComponent<LevelManager>().playableLevelIndex;
        isBonusLevel = levelManager.GetComponent<LevelManager>().playingBonusLevel;
        levelManager.GetComponent<LevelManager>().SpawnPlayers();
        players = GameObject.FindGameObjectsWithTag("Player");
        totalPlayerCount = players.Length;
        //Counts how many players joined the game.
        alivePlayerCount = totalPlayerCount;
        //for (int i = 0; i < totalPlayerCount; i++)
        //{
        //    RaisePlayerCount();
        //    //Debug.Log("Total Player Count " + totalPlayers);
        //}

        //Makes sure the pause text isn't enabled.
        //pauseText.enabled = false;

        //if pause text isn't set up lets the designers know.
        if (pauseText == null)
        {
            Debug.Log("Attach 'Pause Text' from the UI");
        }
    }

    void Start()
    {
        enemyDeathCount = 0;
        uniqueEnemiesKilled = 0;
        questText = GameObject.Find("Quest Text").GetComponent<Text>();
        microQuestText = GameObject.Find("Micro Quest Text").GetComponent<Text>();
        pauseText = GameObject.Find("Pause Text").GetComponent<Text>();
        microQuestText.text = "";
        switch (beatLevelCondition)
        {
            case BeatLevelCondition.DefeatTheBoss:
                //[TODO] Boss needs to tell the game manager who it is.
                if (boss != null)
                {
                    questText.GetComponent<Text>().text = "Defeat " + boss.name + " to cleanse the corruption in this area.";
                }
                else
                {
                    questText.GetComponent<Text>().text = "Missing the boss prefab place add the boss or change the beat level condition.";
                    Debug.LogError("Missing the boss prefab place add the boss or change the beat level condition.");
                }
                break;
            case BeatLevelCondition.KillXEnemies:
                //Set up kill enemy checker, set up quest text
                questText.GetComponent<Text>().text = "Defeat all enemies. \n" + enemyDeathCount + " / " + requiredEnemyDeathCount + " defeated.";
                break;
            case BeatLevelCondition.KillXOfYEnemies:
                //Set up kill enemy checker, set up quest text
                questText.GetComponent<Text>().text = "Defeat " + requiredUniqueEnemies + " " + enemyTypes + ".\n" + uniqueEnemiesKilled +  " / " + requiredUniqueEnemies + " defeated.";
                break;
            case BeatLevelCondition.ReachThePortal:
                //Set up portal and find the portal and set up quest text
                questText.GetComponent<Text>().text = "Reach the final portal to escape!";
                break;
            case BeatLevelCondition.PushAllSwitches:
                //Set button checkers and quest text
                questText.GetComponent<Text>().text = "Activate all switches to " + string1 + "\n" + currentNumberOfSwitchesToPush + " / " + numberOfSwitchesToPush + " currently active.";
                break;
            case BeatLevelCondition.LightASingleTorch:
                //Find the end level torch and set up quest text
                    questText.GetComponent<Text>().text = "Light the beacon to challenge the corruption directly.";
                break;
            case BeatLevelCondition.LightAllTorches:
                //Find all torches and set up quest text
                questText.GetComponent<Text>().text = "Activate all the beacons to reveal the way forward. \n" + currentNumberofTorchesActive + " / " + numberOfTorchesToActivate + " activated.";
                break;
        }

    }
	
	// Update is called once per frame
    void Update()
    {
        //Allows for a global pause [TODO] set up per player pause.
        if (Input.GetButtonDown("Pause"))
        {
            Pause();
        }

        //Quits the game, [TODO] remove redunency of levelmanager and game manager and put in a confermation when quit is clicked. Attach this to pause.
        if (Input.GetKeyDown(KeyCode.Escape) && gamePaused)
        {
            Application.Quit();
        }

        //Respawn Input
        if (Input.GetButtonDown("Jump1") && player1ReadyToRespawn != null)
        {
            Respawn(player1ReadyToRespawn);
            player1ReadyToRespawn = null;
        }

        if (Input.GetButtonDown("Jump2") && player2ReadyToRespawn != null)
        {
            Respawn(player2ReadyToRespawn);
            player2ReadyToRespawn = null;
        }

        if (Input.GetButtonDown("Jump3") && player3ReadyToRespawn != null)
        {
            Respawn(player3ReadyToRespawn);
            player3ReadyToRespawn = null;
        }

        if (Input.GetButtonDown("Jump4") && player4ReadyToRespawn != null)
        {
            Respawn(player4ReadyToRespawn);
            player4ReadyToRespawn = null;
        }
    }

    public void Pause() //Currently this can be paused/unpaused by anyone, need to set up per player pause.
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            //Display pause text
            gamePaused = true;
            pauseText.enabled = true;
        }
        else
        {
            Time.timeScale = 1;
            //Disable Pause Text
            gamePaused = false;
            pauseText.enabled = false;
        }
    }
    public void LowerPlayerCount(GameObject deadPlayer) //Called when a player dies
    {
        alivePlayerCount--;
        if (alivePlayerCount <= 0)
        {
            Debug.Log("All Players dead");
            Invoke("GameOver", 3);//[TODO] Set up game over time and effect then change this # to match that length.
        }
        else
        {
            foreach (GameObject player in players)
            {
                if (player == deadPlayer)
                {
                    //int playersNumber = player.GetComponent<PlayerHealth>().playerNumber;
                    if (player.GetComponent<PlayerHealth>().playerNumber == 1)
                    {
                        player1CurrentRespawnCount = respawnTime;
                        player.GetComponent<PlayerHealth>().playerUI.GetComponent<PlayerUI>().hpText.text = "Respawn in " + player1CurrentRespawnCount.ToString() + " seconds";
                        StartCoroutine(Player1RepawnCountMethod(deadPlayer));
                    }
                    else if (player.GetComponent<PlayerHealth>().playerNumber == 2)
                    {
                        player2CurrentRespawnCount = respawnTime;
                        player.GetComponent<PlayerHealth>().playerUI.GetComponent<PlayerUI>().hpText.text = "Respawn in " + player2CurrentRespawnCount.ToString() + " seconds";
                        StartCoroutine(Player2RepawnCountMethod(deadPlayer));
                    }
                    else if (player.GetComponent<PlayerHealth>().playerNumber == 3)
                    {
                        player3CurrentRespawnCount = respawnTime;
                        player.GetComponent<PlayerHealth>().playerUI.GetComponent<PlayerUI>().hpText.text = "Respawn in " + player3CurrentRespawnCount.ToString() + " seconds";
                        StartCoroutine(Player3RepawnCountMethod(deadPlayer));
                    }
                    else if (player.GetComponent<PlayerHealth>().playerNumber == 4)
                    {
                        player4CurrentRespawnCount = respawnTime;
                        player.GetComponent<PlayerHealth>().playerUI.GetComponent<PlayerUI>().hpText.text = "Respawn in " + player4CurrentRespawnCount.ToString() + " seconds";
                        StartCoroutine(Player2RepawnCountMethod(deadPlayer));
                    }
                }
            }
        }
    }

    public void RaiseAlivePlayerCount() //Called when a player is revived.
    {
        alivePlayerCount++;
    }

    public void GameOver() //Called when all players are dead at the same time
    {
        levelManager.GetComponent<LevelManager>().LoadLevel("GameOver");
    }

    public void LevelProgression()
    {
        switch(beatLevelCondition)
        {
            case BeatLevelCondition.DefeatTheBoss:
                //[TODO] Boss needs to tell the game manager who it is.
                if (boss != null)
                {
                    questText.GetComponent<Text>().text = "Defeat " + boss.name + " to cleanse the corruption in this area.";
                }
                else
                {
                    questText.GetComponent<Text>().text = "Missing the boss prefab place add the boss or change the beat level condition.";
                    Debug.LogError("Missing the boss prefab place add the boss or change the beat level condition.");
                }
                break;
            case BeatLevelCondition.KillXEnemies:
                //Set up kill enemy checker, set up quest text
                questText.GetComponent<Text>().text = "Defeat all enemies. \n" + enemyDeathCount + " / " + requiredEnemyDeathCount + " defeated.";
                break;
            case BeatLevelCondition.KillXOfYEnemies:
                //Set up kill enemy checker, set up quest text
                questText.GetComponent<Text>().text = "Defeat " + requiredUniqueEnemies + " " + enemyTypes + ".\n" + uniqueEnemiesKilled + " / " + requiredUniqueEnemies + " defeated.";
                break;
            case BeatLevelCondition.ReachThePortal:
                //Set up portal and find the portal and set up quest text
                questText.GetComponent<Text>().text = "Reach the final portal to escape!" + playersOnPortal + " / " + totalPlayerCount + " standing on the portal.";
                break;
            case BeatLevelCondition.PushAllSwitches:
                //Set button checkers and quest text
                questText.GetComponent<Text>().text = "Activate all switches to " + string1 + "\n" + currentNumberOfSwitchesToPush + " / " + numberOfSwitchesToPush + " currently active.";
                break;
            case BeatLevelCondition.LightASingleTorch:
                //Find the end level torch and set up quest text
                questText.GetComponent<Text>().text = "Light the beacon to challenge the corruption directly.";
                break;
            case BeatLevelCondition.LightAllTorches:
                //Find all torches and set up quest text
                questText.GetComponent<Text>().text = "Activate all the beacons to reveal the way forward. \n" + currentNumberofTorchesActive + " / " + numberOfTorchesToActivate + " activated.";
                break;
        }

    }

    public void BeatLevel()
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //if (levelIndex == levelManager.GetComponent<LevelManager>().levelIndex && !isBonusLevel)
        //{
        //    levelManager.GetComponent<LevelManager>().levelIndex++; 
        //}
        ////[TODO] save # to player prefs
        ////levelManager.GetComponent<LevelManager>().currentLevelEnemyDeathCount = enemyDeathCount;
        //levelManager.GetComponent<LevelManager>().LoadLevel("LevelSelectScreen");
        /////////////////////////////////////////////////////////////////////////////////////////////////////

        //Show Kyle
        PlayerData data = levelManager.GetComponent<PlayerData>();

        if (!data.levelData[levelIndexName])
        {
            data.levelData[levelIndexName] = true;
        }
        
        if (enemyDeathCount >= data.levelKills[enemyDeathCount])
        {
            data.levelKills[enemyDeathCount] = enemyDeathCount;
        }
        //Takes us back tot he level select screen with the new data added.
        levelManager.GetComponent<LevelManager>().LoadLevel("LevelSelectScreen");
        //Saves the game
        //levelManager.GetComponent<SaveData>().Save();
    }



public void BeatLevel(int UnlockXLevels)
    {
        //[TODO] tell level manager to unlock the next level
        //[TODO] change to a beat level screen or the next level.
        if (levelIndex == levelManager.GetComponent<LevelManager>().levelIndex)
        {
            levelManager.GetComponent<LevelManager>().levelIndex = levelManager.GetComponent<LevelManager>().levelIndex + UnlockXLevels;
        }
        levelManager.GetComponent<LevelManager>().LoadLevel("LevelSelectScreen");
    }

    public void EnemyDeath(GameObject enemy)
    {
        enemyDeathCount++;

        switch(enemyTypes)
        {
            case EnemyTypes.BasicEnemies:
                if (enemy.GetComponent<EnemyT1>())
                    uniqueEnemiesKilled++;
                break;
            case EnemyTypes.Bombers:
                if (enemy.GetComponent<EnemyBomber>())
                    uniqueEnemiesKilled++;
                break;
            case EnemyTypes.CCWizards:
                if (enemy.GetComponent<EnemyCCWizard>())
                    uniqueEnemiesKilled++;
                break;
            case EnemyTypes.Chargers:
                if (enemy.GetComponent<EnemyCharger>())
                    uniqueEnemiesKilled++;
                break;
            case EnemyTypes.Hives:
                if (enemy.GetComponent<EnemyHive>())
                    uniqueEnemiesKilled++;
                break;
            case EnemyTypes.Shooters:
                if (enemy.GetComponent<EnemyShooter>())
                    uniqueEnemiesKilled++;
                break;
            case EnemyTypes.Turrets:
                if (enemy.GetComponent<EnemyTurret>())
                    uniqueEnemiesKilled++;
                break;
            case EnemyTypes.WispHunters:
                if (enemy.GetComponent<EnemyWispHunter>())
                    uniqueEnemiesKilled++;
                break;
                
        }

        if (beatLevelCondition == BeatLevelCondition.KillXEnemies || beatLevelCondition == BeatLevelCondition.KillXOfYEnemies)
        {
            LevelProgression();
        }

        if (enemyDeathCount >= requiredEnemyDeathCount || uniqueEnemiesKilled >= requiredUniqueEnemies)
        {
            BeatLevel();
        }

    }

    public void Respawn(GameObject playerToRespawn)
    {
        playerToRespawn.transform.position = wisp.transform.position;
        playerToRespawn.GetComponent<PlayerHealth>().Heal(playerToRespawn.GetComponent<PlayerHealth>().maxHealth);
        playerToRespawn.GetComponent<PlayerHealth>().playerUI.GetComponent<PlayerUI>().hpText.text = playerToRespawn.GetComponent<PlayerHealth>().playerUI.GetComponent<PlayerUI>().currentHealth.ToString() + " / " + playerToRespawn.GetComponent<PlayerHealth>().playerUI.GetComponent<PlayerUI>().maxHealth;
        playerToRespawn.GetComponent<PlayerHealth>().playerUI.GetComponent<PlayerUI>().SetHealthUI();
        playerToRespawn.SetActive(true);
        RaiseAlivePlayerCount();
        //StopCoroutine(player1RespawnTimer);
    }

    IEnumerator Player1RepawnCountMethod(GameObject player1)
    {
        while (player1CurrentRespawnCount > 0)
        {
            player1CurrentRespawnCount = player1CurrentRespawnCount - 1;
            yield return new WaitForSeconds(1f);
            if (player1CurrentRespawnCount > 1)
            {
                player1.GetComponent<PlayerHealth>().playerUI.GetComponent<PlayerUI>().hpText.text = "Respawn in " + player1CurrentRespawnCount.ToString() + " seconds";
            }
            else
            {
                player1.GetComponent<PlayerHealth>().playerUI.GetComponent<PlayerUI>().hpText.text = "Respawn in " + player1CurrentRespawnCount.ToString() + " second";
            }
            if (player1CurrentRespawnCount == 0)
            {
                player1.GetComponent<PlayerHealth>().playerUI.GetComponent<PlayerUI>().hpText.text = "Jump to Respawn";
                player1ReadyToRespawn = player1;
                //StopCoroutine(player1RespawnTimer);
                StopCoroutine(Player1RepawnCountMethod(player1));
            }
        }
    }

    IEnumerator Player2RepawnCountMethod(GameObject player2)
    {
        while (player2CurrentRespawnCount > 0)
        {
            player2CurrentRespawnCount = player2CurrentRespawnCount - 1;
            yield return new WaitForSeconds(1f);
            if (player2CurrentRespawnCount > 1)
            {
                player2.GetComponent<PlayerHealth>().playerUI.GetComponent<PlayerUI>().hpText.text = "Respawn in " + player2CurrentRespawnCount.ToString() + " seconds";
            }
            else
            {
                player2.GetComponent<PlayerHealth>().playerUI.GetComponent<PlayerUI>().hpText.text = "Respawn in " + player2CurrentRespawnCount.ToString() + " second";
            }

            if (player2CurrentRespawnCount == 0)
            {
                player2.GetComponent<PlayerHealth>().playerUI.GetComponent<PlayerUI>().hpText.text = "Jump to Respawn";
                player2ReadyToRespawn = player2;
                //StopCoroutine(player2RespawnTimer);
                StopCoroutine(Player2RepawnCountMethod(player2));
                
            }
        }
    }

    IEnumerator Player3RepawnCountMethod(GameObject player3)
    {
        while (player3CurrentRespawnCount > 0)
        {
            player3CurrentRespawnCount = player3CurrentRespawnCount - 1;
            yield return new WaitForSeconds(1f);
            if (player3CurrentRespawnCount > 1)
            {
                player3.GetComponent<PlayerHealth>().playerUI.GetComponent<PlayerUI>().hpText.text = "Respawn in " + player3CurrentRespawnCount.ToString() + " seconds";
            }
            else
            {
                player3.GetComponent<PlayerHealth>().playerUI.GetComponent<PlayerUI>().hpText.text = "Respawn in " + player3CurrentRespawnCount.ToString() + " second";
            }
            if (player3CurrentRespawnCount == 0)
            {
                player3.GetComponent<PlayerHealth>().playerUI.GetComponent<PlayerUI>().hpText.text = "Jump to Respawn";
                player3ReadyToRespawn = player3;
                //StopCoroutine(player3RespawnTimer);
                StopCoroutine(Player3RepawnCountMethod(player3));
            }
        }
    }

    IEnumerator Player4RepawnCountMethod(GameObject player4)
    {
        while (player4CurrentRespawnCount > 0)
        {
            Debug.Log("Got to this point");
            player4CurrentRespawnCount = player4CurrentRespawnCount - 1;
            yield return new WaitForSeconds(1f);
            if (player4CurrentRespawnCount > 1)
            {
                player4.GetComponent<PlayerHealth>().playerUI.GetComponent<PlayerUI>().hpText.text = "Respawn in " + player4CurrentRespawnCount.ToString() + " seconds";
            }
            else
            {
                player4.GetComponent<PlayerHealth>().playerUI.GetComponent<PlayerUI>().hpText.text = "Respawn in " + player4CurrentRespawnCount.ToString() + " second";
            }
                
            if (player4CurrentRespawnCount == 0)
            {
                player4.GetComponent<PlayerHealth>().playerUI.GetComponent<PlayerUI>().hpText.text = "Jump to Respawn";
                player4ReadyToRespawn = player4;
                //StopCoroutine(player4RespawnTimer);
                StopCoroutine(Player4RepawnCountMethod(player4));
            }
        }
    }
}
