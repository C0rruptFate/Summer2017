using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Tooltip("How long does it take to load the next level, leave at 0 to avoid loading the next level.")]
    public float autoLoadNextLevelAfter;

    private GameObject spawnPoint1;
    private GameObject spawnPoint2;
    private GameObject spawnPoint3;
    private GameObject spawnPoint4;

    public GameObject fireCharacter;
    public GameObject waterCharacter;
    public GameObject windCharacter;
    public GameObject earthCharacter;

    public Element player1Element;
    public Element player2Element;
    public Element player3Element;
    public Element player4Element;

    void Awake()
    {
            DontDestroyOnLoad(transform.gameObject);
    }

    void Start()
    {
        if (autoLoadNextLevelAfter <= 0)
        {
            Debug.Log("Auto Load Disabled, or use a positive number.");
        }
        else
        {
            Invoke("LoadNextLevel", autoLoadNextLevelAfter);
        }
    }

    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
        //SpawnPlayers();
        //Invoke("SpawnPlayers", .5f);
    }

    public void QuitRequest()
    {
        Debug.Log("Quit Requested.");
        Application.Quit();
    }

    public void LoadNextLevel(string name)
    {
        SceneManager.LoadScene(name);
        //SceneManager.LoadScene(SceneManager.loadedlevel + 1);
    }

    public void SpawnPlayers()
    {
        spawnPoint1 = GameObject.Find("Spawn Point 1");
        spawnPoint2 = GameObject.Find("Spawn Point 2");
        spawnPoint3 = GameObject.Find("Spawn Point 3");
        spawnPoint4 = GameObject.Find("Spawn Point 4");
        GameObject newPlayer;
        //Spawn Player 1
        switch (player1Element)
        {
            case Element.Fire:
                newPlayer = Instantiate(fireCharacter, spawnPoint1.transform.position, spawnPoint1.transform.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 1;
                newPlayer.name = "Player 1";
                Debug.Log("Summon "+ newPlayer.name + " at " + spawnPoint1.name + player1Element);
                break;
            case Element.Ice:
                newPlayer = Instantiate(waterCharacter, spawnPoint1.transform.position, spawnPoint1.transform.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 1;
                newPlayer.name = "Player 1";
                Debug.Log("Summon " + newPlayer.name + " at " + spawnPoint1.name + player1Element);
                break;
            case Element.Earth:
                newPlayer = Instantiate(earthCharacter, spawnPoint1.transform.position, spawnPoint1.transform.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 1;
                newPlayer.name = "Player 1";
                Debug.Log("Summon " + newPlayer.name + " at " + spawnPoint1.name + player1Element);
                break;
            case Element.Wind:
                newPlayer = Instantiate(windCharacter, spawnPoint1.transform.position, spawnPoint1.transform.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 1;
                newPlayer.name = "Player 1";
                Debug.Log("Summon " + newPlayer.name + " at " + spawnPoint1.name + player1Element);
                break;
            default:
                break;
        }

        //Spawn Player 2
        switch (player2Element)
        {
            case Element.Fire:
                newPlayer = Instantiate(fireCharacter, spawnPoint2.transform.position, spawnPoint2.transform.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 2;
                newPlayer.name = "Player 2";
                Debug.Log("Summon " + newPlayer.name + " at " + spawnPoint1.name + player1Element);
                break;
            case Element.Ice:
                newPlayer = Instantiate(waterCharacter, spawnPoint2.transform.position, spawnPoint2.transform.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 2;
                newPlayer.name = "Player 2";
                Debug.Log("Summon " + newPlayer.name + " at " + spawnPoint1.name + player1Element);
                break;
            case Element.Earth:
                newPlayer = Instantiate(earthCharacter, spawnPoint2.transform.position, spawnPoint2.transform.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 2;
                newPlayer.name = "Player 2";
                Debug.Log("Summon " + newPlayer.name + " at " + spawnPoint1.name + player1Element);
                break;
            case Element.Wind:
                newPlayer = Instantiate(windCharacter, spawnPoint2.transform.position, spawnPoint2.transform.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 2;
                newPlayer.name = "Player 2";
                Debug.Log("Summon " + newPlayer.name + " at " + spawnPoint1.name + player1Element);
                break;
            default:
                break;
        }

        //Spawn Player 3
        switch (player3Element)
        {
            case Element.Fire:
                newPlayer = Instantiate(fireCharacter, spawnPoint3.transform.position, spawnPoint3.transform.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 3;
                newPlayer.name = "Player 3";
                Debug.Log("Summon " + newPlayer.name + " at " + spawnPoint1.name + player1Element);
                break;
            case Element.Ice:
                newPlayer = Instantiate(waterCharacter, spawnPoint3.transform.position, spawnPoint3.transform.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 3;
                newPlayer.name = "Player 3";
                Debug.Log("Summon " + newPlayer.name + " at " + spawnPoint1.name + player1Element);
                break;
            case Element.Earth:
                newPlayer = Instantiate(earthCharacter, spawnPoint3.transform.position, spawnPoint3.transform.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 3;
                newPlayer.name = "Player 3";
                Debug.Log("Summon " + newPlayer.name + " at " + spawnPoint1.name + player1Element);
                break;
            case Element.Wind:
                newPlayer = Instantiate(windCharacter, spawnPoint3.transform.position, spawnPoint3.transform.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 3;
                newPlayer.name = "Player 3";
                Debug.Log("Summon " + newPlayer.name + " at " + spawnPoint1.name + player1Element);
                break;
            default:
                break;
        }

        //Spawn Player 4
        switch (player4Element)
        {
            case Element.Fire:
                newPlayer = Instantiate(fireCharacter, spawnPoint4.transform.position, spawnPoint4.transform.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 4;
                newPlayer.name = "Player 4";
                Debug.Log("Summon " + newPlayer.name + " at " + spawnPoint1.name + player1Element);
                break;
            case Element.Ice:
                newPlayer = Instantiate(waterCharacter, spawnPoint4.transform.position, spawnPoint4.transform.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 4;
                newPlayer.name = "Player 4";
                Debug.Log("Summon " + newPlayer.name + " at " + spawnPoint1.name + player1Element);
                break;
            case Element.Earth:
                newPlayer = Instantiate(earthCharacter, spawnPoint4.transform.position, spawnPoint4.transform.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 4;
                newPlayer.name = "Player 4";
                Debug.Log("Summon " + newPlayer.name + " at " + spawnPoint1.name + player1Element);
                break;
            case Element.Wind:
                newPlayer = Instantiate(windCharacter, spawnPoint4.transform.position, spawnPoint4.transform.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 4;
                newPlayer.name = "Player 4";
                Debug.Log("Summon " + newPlayer.name + " at " + spawnPoint1.name + player1Element);
                break;
            default:
                break;
        }
    }
}