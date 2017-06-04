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

    [HideInInspector]
    public GameObject parent;
    [HideInInspector]
    public bool wakeUp = false;

    public virtual bool isTimeToSpawn(GameObject enemyGameObject)
    {
        return false;
    }

    public virtual void Spawn(GameObject myGameObject)
    {

    }

    public virtual void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if (wakeUp == false)
            {
                wakeUp = true;
            }
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            wakeUp = false;
        }
    }
}
