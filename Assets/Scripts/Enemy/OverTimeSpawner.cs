using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverTimeSpawner : Spawner {

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject thisEnemy in enemyPrefabArray)
        {
            if (wakeUp && active)
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

    public override void Spawn(GameObject myGameObject)
    {
        GameObject myEnemy = Instantiate(myGameObject) as GameObject;
        myEnemy.transform.parent = transform;
        myEnemy.transform.position = transform.position;
    }

    public override void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (wakeUp == false)
            {
                wakeUp = true;
            }
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            wakeUp = false;
        }
    }
}
