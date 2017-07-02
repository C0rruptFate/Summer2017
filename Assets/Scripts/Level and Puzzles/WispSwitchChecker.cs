using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WispSwitchChecker : MonoBehaviour
{
    [Tooltip("Leave this alone if you don't want action to happen until all switches are flipped. If you want enemies to spawn when X triggers are flipped put X in here. This can happen several times if you want.")]
    public int[] actionArray;//Used cause spawners toggle their active state when switches are hit.

    [Tooltip("What happens when all switches are flipped?")]
    public PuzzleType puzzleType;

    public GameObject door;

    private GameObject gameManager;

    [HideInInspector]//All switches that are children of mine.
    public GameObject[] mySwitches;
    [HideInInspector]//All spawners that are children of mine.
    public List<Transform> mySpawners;
    [HideInInspector]//How many switches are children of mine.
    public int switchTotal;
    [HideInInspector]//How many switches have been activated.
    public int switchesActive;

    // Use this for initialization
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<WispSwitch>() != null)//Finds all switches that are children of mine.
            {
                switchTotal++;
            }

            if(child.GetComponent<Spawner>() != null)//Finds all spawners that are children of mine.
            {
                mySpawners.Add(child);
            }
        }

        if (puzzleType == PuzzleType.EndLevel)
        {
            gameManager = GameObject.Find("Game Manager");
        }
    }

    public void CheckSwitches()
    {
        switchesActive++;//This is called by the switch when it is flipped, it increases the flipped switch count by 1.

        //Checks to see if any spawners should become active when a switch is flipped.
        if (mySpawners != null && actionArray != null && actionArray.Contains(switchesActive))
        {
            foreach (Transform spawner in mySpawners)
            {
                spawner.GetComponent<Spawner>().active = true;
            }
        }
        //Checks when all switches are flipped and the puzzle is solved. This will also flip the active state of all spawners.
        if (switchesActive == switchTotal)
        {
            //[TODO] open door, end level, whatever set up enum with these different options for it to look at.
            foreach (Transform spawner in mySpawners)
            {
                spawner.GetComponent<Spawner>().active = spawner.GetComponent<Spawner>().active ? false : true;
            }

            switch (puzzleType)
            {
                case PuzzleType.Door:
                    OpenDoor();
                    break;
                case PuzzleType.EndLevel:
                    BeatLevel();
                    break;
                case PuzzleType.RemoveHazard:
                    RemoveHazards();
                    break;
            }
        }
    }

    public void OpenDoor()
    {
        //[TODO] Set open more function.
        //[TODO] play music animate door opening or fading.
        Destroy(door);
    }

    public void BeatLevel()
    {
        //[TODO] Set end level function.
        gameManager.GetComponent<GameController>().BeatLevel();
    }

    public void RemoveHazards()
    {

    }
}
