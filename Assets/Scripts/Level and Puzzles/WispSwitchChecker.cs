using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WispSwitchChecker : MonoBehaviour
{

    public GameObject[] mySwitches;
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

        if (switchesActive == switchTotal)
        {
            //[TODO] open door, end level, whatever
            if (mySpawners != null)
            {
                foreach (Transform spawner in mySpawners)
                {
                    spawner.GetComponent<Spawner>().active = spawner.GetComponent<Spawner>().active ? false : true;
                }
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
