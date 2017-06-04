using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WispSwitchChecker : MonoBehaviour
{
    public int[] actionArray;

    [HideInInspector]
    public GameObject[] mySwitches;
    [HideInInspector]
    public List<Transform> mySpawners;
    [HideInInspector]
    public int switchTotal;
    [HideInInspector]
    public int switchesActive;

    // Use this for initialization
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<WispSwitch>() != null)
            {
                switchTotal++;
            }

            if(child.GetComponent<Spawner>() != null)
            {
                mySpawners.Add(child);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckSwitches()
    {
        switchesActive++;
        if (mySpawners != null && actionArray != null && actionArray.Contains(switchesActive))
        {
            foreach (Transform spawner in mySpawners)
            {
                spawner.GetComponent<Spawner>().active = true;
            }
        }

        if (switchesActive == switchTotal)
        {
            //[TODO] open door, end level, whatever
            foreach (Transform spawner in mySpawners)
            {
                spawner.GetComponent<Spawner>().active = spawner.GetComponent<Spawner>().active ? false : true;
            }
        }
    }

    void OpenDoor()
    {

    }

    void BeatLevel()
    {

    }
}
