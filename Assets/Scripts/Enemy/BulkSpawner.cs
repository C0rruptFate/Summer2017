using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulkSpawner : Spawner {

    [Tooltip("how many unit's to spawn, set this then add the playercount and plus difficulty (if it is greater than 1).")]
    public int spawnCount;

    private int playerCounted = 0;

    int newDifficulty = 0;

    // Use this for initialization
    void Start () {

        if (Constants.difficulty != 1)
        {
            newDifficulty = Constants.difficulty;
        }

        if (Constants.playerCount > 1)
        {
            playerCounted = Constants.playerCount;
            
        }

        spawnCount = spawnCount + playerCounted + newDifficulty;

    }
	
	// Update is called once per frame
	void Update () {
		
        if(active)
        {
            Spawn();
            active = false;
        }
        
	}

    public override bool isTimeToSpawn(GameObject enemyGameObject)
    {
        return false;
    }

    public void Spawn()
    {
        int spawnSelection = 0;//The position pulled from the array
        for (int i = 0; i < spawnCount; i++)
        {
            Instantiate(enemyPrefabArray[spawnSelection], transform.position, transform.rotation);
            spawnSelection++;

            if (spawnSelection >= (enemyPrefabArray.Length - 1))
            {
                spawnSelection = 0;
            }
        }
    }
}
