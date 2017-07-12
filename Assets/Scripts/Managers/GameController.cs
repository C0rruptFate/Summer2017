using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [Tooltip("Attach the pause text located on the CameraRig>UI>Pause Text!")]
    public Text pauseText;//Text displayed when a player pauses the game.

    //[HideInInspector]//list of all players that joined the level.
    public GameObject[] players;

    //[HideInInspector]//Total count of all players that joined the level.
    public int totalPlayerCount;

    //[HideInInspector]//Total count of all living players.
    public int alivePlayerCount;

    private GameObject levelManager;//Object of the level manager so that we can load levels.
    [HideInInspector]
    public GameObject wisp;

    //Respawn Values
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
    void Awake () {
        //Sets gameobject and components.
        levelManager = GameObject.Find("Level Manager");
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
	
	// Update is called once per frame
    void Update()
    {
        //Allows for a global pause [TODO] set up per player pause.
        if (Input.GetButtonDown("Pause"))
        {
            Pause();
        }

        //Quits the game, [TODO] remove redunency of levelmanager and game manager and put in a confermation when quit is clicked. Attach this to pause.
        if (Input.GetKeyDown(KeyCode.Escape))
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
            pauseText.enabled = true;
        }
        else
        {
            Time.timeScale = 1;
            //Disable Pause Text
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

    public void BeatLevel()
    {
        //[TODO] change to a beat level screen or the next level.
        levelManager.GetComponent<LevelManager>().LoadLevel("GameOver");
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
