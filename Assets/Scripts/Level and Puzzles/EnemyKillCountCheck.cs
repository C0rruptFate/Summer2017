using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKillCountCheck : MonoBehaviour {

    public GameObject[] thingsToEnable;

    public GameObject[] thingsToDisable;

    public GameObject[] spawners;

    public int needToKill;

    private int playersInArea;

    private GameObject gameManager;

    private bool beenBeaten;

    void OnDrawGizmos()
    {
        //Wire for start position
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }

    // Use this for initialization
    void Start () {
        gameManager = GameObject.Find("Game Manager");

        foreach (GameObject parts in thingsToDisable)
        {
            parts.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !beenBeaten)
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
            foreach (GameObject parts in thingsToEnable)
            {
                parts.SetActive(true);
            }

            foreach (GameObject spawner in spawners)
            {
                spawner.GetComponent<Spawner>().active = true;
            }
        }
    }

    void EnemyKill()
    {
        needToKill--;

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
        }
    }
}
