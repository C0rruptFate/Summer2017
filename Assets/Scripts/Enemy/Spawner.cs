using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [Tooltip("List of all things that this spawner can spawn.")]
    public GameObject[] enemyPrefabArray;
    [Tooltip("divides the spawn rate of each enemy by this number.")]
    public int slowDownSpawner = 5;
    [Tooltip("Set to true to have a spawner enabled when the level loads, false if the spawner is enabled by a trigger in game.")]
    public bool active = true;

    [Tooltip("How many enemies can this have before it stops spawning new ones.")]
    public int maxEnemies = 5;
    public bool adjustForPlayerCount = false;

    [HideInInspector]//If this is a child object of a Wisp switch
    public GameObject parent;
    [HideInInspector]//Wake up becomes true when a player is inside of the trigger space of this spawner.
    public bool wakeUp = false;

    public float spawnActiveDelay;

    public virtual bool isTimeToSpawn(GameObject enemyGameObject)
    {//Used by the over time spawner to control how often something should spawn.
        return false;
    }

    public virtual void Spawn(GameObject myGameObject)
    {

    }

    public virtual void OnTriggerStay2D(Collider2D other)
    {//Wake up is true as long as a player inside of the trigger space.
        if(other.CompareTag("Player"))
        {
            if (wakeUp == false)
            {
                wakeUp = true;
            }
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {//Wake up becomes false when the player leaves the trigger space.
        if (other.CompareTag("Player"))
        {
            wakeUp = false;
        }
    }
}
