using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyKillCountCheck : MonoBehaviour {

    public GameObject[] thingsToEnable;

    public GameObject[] thingsToDisable;

    public GameObject[] spawners;

    public int needToKill;

    [HideInInspector]
    public Text microQuestText;

    private int playersInArea;

    private GameObject gameManager;

    private bool beenBeaten;

    private bool startedSpawning;

    void OnDrawGizmos()
    {
        //Wire for start position
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }

    // Use this for initialization
    void Start () {
        gameManager = GameObject.Find("Game Manager");
        microQuestText = GameObject.Find("Micro Quest Text").GetComponent<Text>();

        //foreach (GameObject parts in thingsToDisable)
        //{
        //    parts.SetActive(false);
        //}
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !beenBeaten && !startedSpawning)
        {
            CheckPlayers();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !beenBeaten)
        {
            playersInArea--;
            if (playersInArea < 0)
                playersInArea = 0;
        }
        else if (other.CompareTag("Enemy") && !beenBeaten)
        {
            EnemyKill();
        }
    }

    void CheckPlayers()
    {
        playersInArea++;
        if (playersInArea > gameManager.GetComponent<GameController>().totalPlayerCount)
            playersInArea = gameManager.GetComponent<GameController>().totalPlayerCount;

        if (playersInArea == gameManager.GetComponent<GameController>().totalPlayerCount)
        {
            Invoke("LockDoorsAndEnable", 0.25f);
        }
    }

    void LockDoorsAndEnable()
    {
        if (playersInArea == gameManager.GetComponent<GameController>().totalPlayerCount)
        {
            microQuestText.text = needToKill + " enemies left to slay.";
            foreach (GameObject parts in thingsToEnable)
            {
                parts.SetActive(true);
            }

            foreach (GameObject spawner in spawners)
            {
                spawner.GetComponent<Spawner>().active = true;
            }
            startedSpawning = true;
        }
    }

    void EnemyKill()
    {
        needToKill--;

        microQuestText.text = needToKill + " enemies left to slay.";

        if (needToKill <= 0)
        {
            foreach (GameObject parts in thingsToDisable)
            {
                parts.SetActive(false);
            }

            foreach (GameObject spawner in spawners)
            {
                spawner.GetComponent<Spawner>().active = false;
            }

            beenBeaten = true;
            if (microQuestText.text.Contains(" enemies left to slay."))
            {
                microQuestText.text = "";
            }
        }
    }
}
