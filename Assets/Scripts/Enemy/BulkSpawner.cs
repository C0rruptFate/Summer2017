using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulkSpawner : Spawner {

    //Used to spawn the set units as soon as this is active and players are inside of it's trigger space. 

    [Tooltip("how many unit's to spawn, set this then add the playercount and plus difficulty (if they are greater than 1).")]
    public int spawnCount;

    public bool eventSpawner;

    private int playerCounted = 0;//This looks at how many players joined the game. If there are 2 or more players more enemies will spawn.

    int newDifficulty = 0; //This looks at what difficulty the game is set to if it has been increased more enemies will spawn.

    // Use this for initialization
    void Start () {

        //Used to check the difficulty (1 is normal) if 2 or greater then more enemies will spawn.
        if (Constants.difficulty != 1)
        {
            newDifficulty = Constants.difficulty;
        }

        //Used to check the player count, if it is 2 or greater than more enemies will spawn.
        if (Constants.playerCount > 1)
        {
            playerCounted = Constants.playerCount;
            
        }

        //Combines the difficulty, player count, and the set spawn count to decide how many to spawn.
        if (adjustForPlayerCount)
        {
            spawnCount = spawnCount + playerCounted + newDifficulty;
        }
        

    }
	
	// Update is called once per frame
	void Update () {
		
        //If this isn't active then it will not spawn. Set to active to have it spawn when a player enters it's trigger space.
        if(active && wakeUp && Time.time >= spawnActiveDelay)
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
        //Used to spawn enemies 
        int spawnSelection = 0;//The position pulled from the array
        for (int i = 0; i < spawnCount; i++)
        {
            GameObject myEnemy = Instantiate(enemyPrefabArray[spawnSelection], transform.position, transform.rotation);//Spawns the enemy in that array's position.
            myEnemy.transform.parent = transform;
            myEnemy.transform.position = transform.position;
            spawnSelection++;//Increase the spawn selection

            if (spawnSelection >= (enemyPrefabArray.Length - 1))//Resets where in the array the spawn selection looks to spawn the units.
            {//This is used if the difficulty and player count cause it to spawn more enemies so that it can pull from the first second, and third slots if needed.
                spawnSelection = 0;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!wakeUp && !active && transform.childCount < maxEnemies && !eventSpawner)
        {
            active = true;
        }
    }
}
