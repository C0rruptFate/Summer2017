using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverTimeSpawner : Spawner {

    //Used to spawn enemies over time (so long as a player is inside the trigger space and this wakes up).

    // Update is called once per frame
    void Update()
    {
        //Spawns each enemy in the enemy array list of this transform
        foreach (GameObject thisEnemy in enemyPrefabArray)
        {
            if (wakeUp && active && isTimeToSpawn(thisEnemy) && Time.time >= spawnActiveDelay)
            {
                Spawn(thisEnemy);
            }
        }
    }

    public override bool isTimeToSpawn(GameObject enemyGameObject)
    {
        EnemyHealth enemy = enemyGameObject.GetComponent<EnemyHealth>();

        float meanSpawnDelay = enemy.seenEverySeconds;
        float spawnsPerSecond = 1 / meanSpawnDelay;

        if (Time.deltaTime > meanSpawnDelay)
        {
            Debug.LogWarning("Spawn capped by frame rate");
        }

        float threshold = spawnsPerSecond * Time.deltaTime;

        return (Random.value < threshold / slowDownSpawner * Constants.difficulty);
    }

    //Used to spawn enemies and parent them to the spawner.
    public override void Spawn(GameObject myGameObject)
    {
        if(transform.childCount < maxEnemies)
        {
            GameObject myEnemy = Instantiate(myGameObject) as GameObject;
            myEnemy.transform.parent = transform;
            myEnemy.transform.position = transform.position;
        }

        if (singleTimeSpawner)
        {
            active = false;
        }
    }

    public override void OnTriggerStay2D(Collider2D other)
    {//Wakes up if a player is inside this trigger spawn area.
        if (other.CompareTag("Player"))
        {
            if (wakeUp == false)
            {
                wakeUp = true;
            }
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {//Wake up becomes false when a player leaves the trigger area.
        if (other.CompareTag("Player"))
        {
            wakeUp = false;
        }
    }
}
