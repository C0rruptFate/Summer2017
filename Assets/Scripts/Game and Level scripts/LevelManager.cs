using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Tooltip("How long does it take to load the next level, leave at 0 to avoid loading the next level.")]
    public float autoLoadNextLevelAfter;

    private Transform spawnPoint1;
    private Transform spawnPoint2;
    private Transform spawnPoint3;
    private Transform spawnPoint4;

    public string testString;

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
        spawnPoint1 = transform.Find("Spawn Point 1");
        spawnPoint2 = transform.Find("Spawn Point 2");
        spawnPoint3 = transform.Find("Spawn Point 3");
        spawnPoint4 = transform.Find("Spawn Point 4");
    }

    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
        testString = "String is working";
        SpawnPlayers();
        //Invoke("SpawnPlayers", 0.01f);
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
        GameObject newPlayer;
        //Spawn Player 1
        switch (player1Element)
        {
            case Element.Fire:
                newPlayer = Instantiate(fireCharacter, spawnPoint1.position, spawnPoint1.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 1;
                newPlayer.name = "Player 1";
                break;
            case Element.Ice:
                newPlayer = Instantiate(waterCharacter, spawnPoint1.position, spawnPoint1.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 1;
                newPlayer.name = "Player 1";
                break;
            case Element.Earth:
                newPlayer = Instantiate(earthCharacter, spawnPoint1.position, spawnPoint1.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 1;
                newPlayer.name = "Player 1";
                break;
            case Element.Wind:
                newPlayer = Instantiate(windCharacter, spawnPoint1.position, spawnPoint1.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 1;
                newPlayer.name = "Player 1";
                break;
            default:
                break;
        }

        //Spawn Player 2
        switch (player2Element)
        {
            case Element.Fire:
                newPlayer = Instantiate(fireCharacter, spawnPoint2.position, spawnPoint2.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 2;
                newPlayer.name = "Player 2";
                break;
            case Element.Ice:
                newPlayer = Instantiate(waterCharacter, spawnPoint2.position, spawnPoint2.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 2;
                newPlayer.name = "Player 2";
                break;
            case Element.Earth:
                newPlayer = Instantiate(earthCharacter, spawnPoint2.position, spawnPoint2.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 2;
                newPlayer.name = "Player 2";
                break;
            case Element.Wind:
                newPlayer = Instantiate(windCharacter, spawnPoint2.position, spawnPoint2.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 2;
                newPlayer.name = "Player 2";
                break;
            default:
                break;
        }

        //Spawn Player 3
        switch (player3Element)
        {
            case Element.Fire:
                newPlayer = Instantiate(fireCharacter, spawnPoint3.position, spawnPoint3.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 3;
                newPlayer.name = "Player 3";
                break;
            case Element.Ice:
                newPlayer = Instantiate(waterCharacter, spawnPoint3.position, spawnPoint3.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 3;
                newPlayer.name = "Player 3";
                break;
            case Element.Earth:
                newPlayer = Instantiate(earthCharacter, spawnPoint3.position, spawnPoint3.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 3;
                newPlayer.name = "Player 3";
                break;
            case Element.Wind:
                newPlayer = Instantiate(windCharacter, spawnPoint3.position, spawnPoint3.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 3;
                newPlayer.name = "Player 3";
                break;
            default:
                break;
        }

        //Spawn Player 4
        switch (player4Element)
        {
            case Element.Fire:
                newPlayer = Instantiate(fireCharacter, spawnPoint4.position, spawnPoint4.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 4;
                newPlayer.name = "Player 4";
                break;
            case Element.Ice:
                newPlayer = Instantiate(waterCharacter, spawnPoint4.position, spawnPoint4.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 4;
                newPlayer.name = "Player 4";
                break;
            case Element.Earth:
                newPlayer = Instantiate(earthCharacter, spawnPoint4.position, spawnPoint4.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 4;
                newPlayer.name = "Player 4";
                break;
            case Element.Wind:
                newPlayer = Instantiate(windCharacter, spawnPoint4.position, spawnPoint4.rotation);
                newPlayer.GetComponent<PlayerHealth>().playerNumber = 4;
                newPlayer.name = "Player 4";
                break;
            default:
                break;
        }
    }
}