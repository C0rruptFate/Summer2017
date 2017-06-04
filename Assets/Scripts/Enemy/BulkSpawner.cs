using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulkSpawner : Spawner {

    [Tooltip("how many unit's to spawn, set this then times by playercount and plus difficulty (if it is greater than 1).")]
    public int spawnCount;

    int newDifficulty = 0;

    // Use this for initialization
    void Start () {

        if (Constants.difficulty != 1)
        {
            newDifficulty = Constants.difficulty;
        }

        if (Constants.playerCount > 1)
        {
            newDifficulty = Constants.playerCount;
        }

        spawnCount = spawnCount + Constants.playerCount;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override bool isTimeToSpawn(GameObject enemyGameObject)
    {
        return false;
    }

    public override void Spawn(GameObject myGameObject)
    {
        int spawnSelection = 0;//The position pulled from the array
        for (int i = 0; i < spawnCount; i++)
        {
            spawnSelection++;
            Instantiate(enemyPrefabArray[spawnSelection], transform.position, transform.rotation);

            if (spawnSelection >= spawnSelection + Constants.playerCount + newDifficulty)
            {
                spawnSelection = 0;
            }
        }
    }
}
