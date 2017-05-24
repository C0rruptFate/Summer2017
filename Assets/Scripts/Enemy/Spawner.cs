using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [Tooltip("List of all things that this spawner can spawn.")]
    public GameObject[] enemyPrefabArray;
    [Tooltip("divides the spawn rate of each enemy by this number.")]
    public int numOfSpawners = 5;

    private GameObject parent;
    private bool wakeUp = false;

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject thisEnemy in enemyPrefabArray)
        {
            if (isTimeToSpawn(thisEnemy) && wakeUp)
            {
                Spawn(thisEnemy);
            }
        }
    }

    bool isTimeToSpawn(GameObject enemyGameObject)
    {
        EnemyHealth enemy = enemyGameObject.GetComponent<EnemyHealth>();

        float meanSpawnDelay = enemy.seenEverySeconds;
        float spawnsPerSecond = 1 / meanSpawnDelay;

        if (Time.deltaTime > meanSpawnDelay)
        {
            Debug.LogWarning("Spawn capped by frame rate");
        }

        float threshold = spawnsPerSecond * Time.deltaTime;

        return (Random.value < threshold / numOfSpawners);
    }

    void Spawn(GameObject myGameObject)
    {
        GameObject myEnemy = Instantiate(myGameObject) as GameObject;
        myEnemy.transform.parent = transform;
        myEnemy.transform.position = transform.position;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if (wakeUp == false)
            {
                wakeUp = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            wakeUp = false;
        }
    }
}
