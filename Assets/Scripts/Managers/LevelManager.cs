using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //Handling level management
    [Tooltip("How long does it take to load the next level, leave at 0 to avoid loading the next level.")]
    public float autoLoadNextLevelAfter;
    [HideInInspector]
    public string previousScene;
    public string playableZoneGroup;
    public string playableLevelIndex;
    public int currentLevelEnemyDeathCount;
    public bool playingBossLevel;

    public GameObject levelSelectMap;

    //Spawn points 
    private GameObject spawnPoint1;
    private GameObject spawnPoint2;
    private GameObject spawnPoint3;
    private GameObject spawnPoint4;

    //possible characters to spawn
    public GameObject fireCharacter;
    public GameObject waterCharacter;
    public GameObject windCharacter;
    public GameObject earthCharacter;

    //What each player chose
    [HideInInspector]
    public Element player1Element;
    [HideInInspector]
    public Element player2Element;
    [HideInInspector]
    public Element player3Element;
    [HideInInspector]
    public Element player4Element;

    public int levelIndex;

    //Tells the camera to track these players
    private GameObject cameraRig;

    void Awake()
    {
            DontDestroyOnLoad(transform.gameObject);//Makes sure this object stays each time a new level is loaded. 
    }

    void Start()
    {
        if (autoLoadNextLevelAfter <= 0)//Loads the next level after X seconds, X must be greater than 0. Mainly used for a splash screen.
        {
            Debug.Log("Auto Load Disabled, or use a positive number.");
        }
        else
        {
            Invoke("LoadNextLevel", autoLoadNextLevelAfter);
        }
    }

    //Used for loading scenes (other than playable levels, and boss levels.)
    public void LoadLevel(string sceneName, string levelIndexName)//Loads the level that is put into the string.
    {
        previousScene = SceneManager.GetActiveScene().name;//Saves the last level I was in so that I can go back.
        //Debug.Log("level I'm loading " + sceneName);
        playableLevelIndex = levelIndexName;
        SceneManager.LoadScene(sceneName);
        //SpawnPlayers();
        //Invoke("SpawnPlayers", .5f);
    }
    //Used for loading playable levels that are not boss levels.
    public void LoadLevel(string sceneName, string levelIndexName, string zoneGroup, bool bossLevel)//Loads the level that is put into the string.
    {
        previousScene = SceneManager.GetActiveScene().name;//Saves the last level I was in so that I can go back.
        playableZoneGroup = zoneGroup;
        playableLevelIndex = levelIndexName;
        playingBossLevel = bossLevel;
        SceneManager.LoadScene(sceneName);
        //SpawnPlayers();
        //Invoke("SpawnPlayers", .5f);
    }

    public void LoadScene(string name)
    {
        previousScene = SceneManager.GetActiveScene().name;//Saves the last level I was in so that I can go back.
        SceneManager.LoadScene(name);
    }

    public void QuitRequest()//Closes the game when this is called. [TODO] put in a confirmation option.
    {
        Debug.Log("Quit Requested.");
        Application.Quit();
    }

    public void LoadNextLevel(string name)//Not being used ATM. [TODO] set this up to load the next level.
    {
        previousScene = SceneManager.GetActiveScene().name; //Saves the last level I was in so that I can go back.
        SceneManager.LoadScene(name);
        //SceneManager.LoadScene(SceneManager.loadedlevel + 1);
    }

    //Spawns all playrs and sets up their controls.
    public void SpawnPlayers()
    {
        //Finds the camera to track with.
        cameraRig = GameObject.Find("Camera Rig");

        //Finds the spawn points in the world
        spawnPoint1 = GameObject.Find("Spawn Point 1");
        spawnPoint2 = GameObject.Find("Spawn Point 2");
        spawnPoint3 = GameObject.Find("Spawn Point 3");
        spawnPoint4 = GameObject.Find("Spawn Point 4");
        GameObject newPlayer;

        //Spawn Player 1, set their element, controls and tells the camera to follow them.
        switch (player1Element)
        {
            case Element.Fire:
                newPlayer = Instantiate(fireCharacter, spawnPoint1.transform.position, spawnPoint1.transform.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 1;
                newPlayer.name = "Player 1";
                //Debug.Log("Summon "+ newPlayer.name + " at " + spawnPoint1.name + player1Element);
                cameraRig.GetComponent<CameraControls>().players.Add(newPlayer.transform);
                break;
            case Element.Water:
                newPlayer = Instantiate(waterCharacter, spawnPoint1.transform.position, spawnPoint1.transform.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 1;
                newPlayer.name = "Player 1";
                //Debug.Log("Summon " + newPlayer.name + " at " + spawnPoint1.name + player1Element);
                cameraRig.GetComponent<CameraControls>().players.Add(newPlayer.transform);
                break;
            case Element.Earth:
                newPlayer = Instantiate(earthCharacter, spawnPoint1.transform.position, spawnPoint1.transform.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 1;
                newPlayer.name = "Player 1";
                //Debug.Log("Summon " + newPlayer.name + " at " + spawnPoint1.name + player1Element);
                cameraRig.GetComponent<CameraControls>().players.Add(newPlayer.transform);
                break;
            case Element.Air:
                newPlayer = Instantiate(windCharacter, spawnPoint1.transform.position, spawnPoint1.transform.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 1;
                newPlayer.name = "Player 1";
                //Debug.Log("Summon " + newPlayer.name + " at " + spawnPoint1.name + player1Element);
                cameraRig.GetComponent<CameraControls>().players.Add(newPlayer.transform);
                break;
            default:
                break;
        }

        //Spawn Player 2, set their element, controls and tells the camera to follow them.
        switch (player2Element)
        {
            case Element.Fire:
                newPlayer = Instantiate(fireCharacter, spawnPoint2.transform.position, spawnPoint2.transform.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 2;
                newPlayer.name = "Player 2";
                //Debug.Log("Summon " + newPlayer.name + " at " + spawnPoint1.name + player1Element);
                cameraRig.GetComponent<CameraControls>().players.Add(newPlayer.transform);
                break;
            case Element.Water:
                newPlayer = Instantiate(waterCharacter, spawnPoint2.transform.position, spawnPoint2.transform.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 2;
                newPlayer.name = "Player 2";
                //Debug.Log("Summon " + newPlayer.name + " at " + spawnPoint1.name + player1Element);
                cameraRig.GetComponent<CameraControls>().players.Add(newPlayer.transform);
                break;
            case Element.Earth:
                newPlayer = Instantiate(earthCharacter, spawnPoint2.transform.position, spawnPoint2.transform.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 2;
                newPlayer.name = "Player 2";
                //Debug.Log("Summon " + newPlayer.name + " at " + spawnPoint1.name + player1Element);
                cameraRig.GetComponent<CameraControls>().players.Add(newPlayer.transform);
                break;
            case Element.Air:
                newPlayer = Instantiate(windCharacter, spawnPoint2.transform.position, spawnPoint2.transform.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 2;
                newPlayer.name = "Player 2";
                //Debug.Log("Summon " + newPlayer.name + " at " + spawnPoint1.name + player1Element);
                cameraRig.GetComponent<CameraControls>().players.Add(newPlayer.transform);
                break;
            default:
                break;
        }

        //Spawn Player 3, set their element, controls and tells the camera to follow them.
        switch (player3Element)
        {
            case Element.Fire:
                newPlayer = Instantiate(fireCharacter, spawnPoint3.transform.position, spawnPoint3.transform.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 3;
                newPlayer.name = "Player 3";
                //Debug.Log("Summon " + newPlayer.name + " at " + spawnPoint1.name + player1Element);
                cameraRig.GetComponent<CameraControls>().players.Add(newPlayer.transform);
                break;
            case Element.Water:
                newPlayer = Instantiate(waterCharacter, spawnPoint3.transform.position, spawnPoint3.transform.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 3;
                newPlayer.name = "Player 3";
                //Debug.Log("Summon " + newPlayer.name + " at " + spawnPoint1.name + player1Element);
                cameraRig.GetComponent<CameraControls>().players.Add(newPlayer.transform);
                break;
            case Element.Earth:
                newPlayer = Instantiate(earthCharacter, spawnPoint3.transform.position, spawnPoint3.transform.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 3;
                newPlayer.name = "Player 3";
                //Debug.Log("Summon " + newPlayer.name + " at " + spawnPoint1.name + player1Element);
                cameraRig.GetComponent<CameraControls>().players.Add(newPlayer.transform);
                break;
            case Element.Air:
                newPlayer = Instantiate(windCharacter, spawnPoint3.transform.position, spawnPoint3.transform.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 3;
                newPlayer.name = "Player 3";
                //Debug.Log("Summon " + newPlayer.name + " at " + spawnPoint1.name + player1Element);
                cameraRig.GetComponent<CameraControls>().players.Add(newPlayer.transform);
                break;
            default:
                break;
        }

        //Spawn Player 4, set their element, controls and tells the camera to follow them.
        switch (player4Element)
        {
            case Element.Fire:
                newPlayer = Instantiate(fireCharacter, spawnPoint4.transform.position, spawnPoint4.transform.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 4;
                newPlayer.name = "Player 4";
                //Debug.Log("Summon " + newPlayer.name + " at " + spawnPoint1.name + player1Element);
                cameraRig.GetComponent<CameraControls>().players.Add(newPlayer.transform);
                break;
            case Element.Water:
                newPlayer = Instantiate(waterCharacter, spawnPoint4.transform.position, spawnPoint4.transform.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 4;
                newPlayer.name = "Player 4";
                //Debug.Log("Summon " + newPlayer.name + " at " + spawnPoint1.name + player1Element);
                cameraRig.GetComponent<CameraControls>().players.Add(newPlayer.transform);
                break;
            case Element.Earth:
                newPlayer = Instantiate(earthCharacter, spawnPoint4.transform.position, spawnPoint4.transform.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 4;
                newPlayer.name = "Player 4";
                //Debug.Log("Summon " + newPlayer.name + " at " + spawnPoint1.name + player1Element);
                cameraRig.GetComponent<CameraControls>().players.Add(newPlayer.transform);
                break;
            case Element.Air:
                newPlayer = Instantiate(windCharacter, spawnPoint4.transform.position, spawnPoint4.transform.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 4;
                newPlayer.name = "Player 4";
                //Debug.Log("Summon " + newPlayer.name + " at " + spawnPoint1.name + player1Element);
                cameraRig.GetComponent<CameraControls>().players.Add(newPlayer.transform);
                break;
            default:
                break;
        }
    }
}