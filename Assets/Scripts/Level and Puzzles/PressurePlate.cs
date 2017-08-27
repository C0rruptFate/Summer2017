using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {
    
    public bool holdDownButtons = true;

    [Tooltip("If true this is used to end the level. Buttons should be held down for this.")]
    public bool endLevelButtons = false;

    [HideInInspector]
    public int pressurePlatePushedCount;

    [HideInInspector]
    public int pressurePlateTotal;

    public GameObject[] myButtons;
    //private int myButtonCount;

    public GameObject[] nonButtonObjects;

    private int activeButtonCount;

    private GameObject gm;
    // Use this for initialization
    void Start () {
        //Makes sure that buttons only need to be held down if there are 2 or more players in the game.
        gm = GameObject.Find("Game Manager");
        if (gm.GetComponent<GameController>().totalPlayerCount <= 1)
        {
            holdDownButtons = false;
        }

        foreach (Transform child in transform)
        {
            if (child.GetComponent<Button>() != null && child.gameObject.activeSelf)
            {
                pressurePlateTotal++;
            }

            else if (child.GetComponent<NewMovingPlatform>() != null)
            {
                child.GetComponent<NewMovingPlatform>().allowedToMove = false;
                //Debug.Log("Found a child");
            }
        }

        //if (pressurePlateTotal >= gm.GetComponent<GameController>().totalPlayerCount)
        //int targetButton = pressurePlateTotal - 1;
        pressurePlateTotal = 0;

        for (int i = 0; i < gm.GetComponent<GameController>().totalPlayerCount; i++)
        {
            //Still showing out of range (myButtons[i] !=null)
            if (i < myButtons.Length)
            {
                myButtons[i].GetComponent<Button>().pressurePlateMaster = gameObject;
                myButtons[i].GetComponent<Button>().IShouldBeActive();
                myButtons[i].GetComponent<Button>().iShouldBeActiveBool = true;
                activeButtonCount++;
                //Debug.Log("I " + i + "Pressure Plat Total: " + pressurePlateTotal);
            }
        }

        if (endLevelButtons == true)
        {
            gm.GetComponent<GameController>().numberOfSwitchesToPush = activeButtonCount;
            gm.GetComponent<GameController>().LevelProgression();
        }
    }

    public void CheckandIncreaseButtons()
    {
        pressurePlatePushedCount++;
        if (endLevelButtons)
        {
            gm.GetComponent<GameController>().currentNumberOfSwitchesToPush = pressurePlatePushedCount;
            gm.GetComponent<GameController>().LevelProgression();
        }
        if (holdDownButtons == false)
        {
            //Needs to check for only active buttons
            pressurePlateTotal = activeButtonCount;
        }

        if (pressurePlatePushedCount > pressurePlateTotal)
            pressurePlatePushedCount = pressurePlateTotal;


        if (pressurePlatePushedCount == pressurePlateTotal)
        {
            foreach (GameObject nonButtonObject in nonButtonObjects)
            {
                if (nonButtonObject.GetComponent<Hazard>() != null)
                {//causes the hazards to rise up.
                    nonButtonObject.GetComponent<Hazard>().lowerHazard = true;
                }
                else if (nonButtonObject.GetComponent<NewMovingPlatform>() != null)
                {
                    nonButtonObject.GetComponent<NewMovingPlatform>().allowedToMove = true;
                }
                else if (endLevelButtons)
                {
                    BeatLevel();
                }
                else if (nonButtonObject.gameObject.activeSelf == true)
                {
                    nonButtonObject.gameObject.SetActive(false);
                }
                else if (nonButtonObject.gameObject.activeSelf == false)
                {
                    nonButtonObject.gameObject.SetActive(true);
                }
            }
        }
    }

    public void CheckandDecreaseButtons()
    {
        pressurePlatePushedCount--;
        if (pressurePlatePushedCount < 0)
            pressurePlatePushedCount = 0;

        foreach (GameObject nonButtonObject in nonButtonObjects)
        {
            if (nonButtonObject.GetComponent<Hazard>() != null)
            {//causes the hazards to rise up.
                nonButtonObject.GetComponent<Hazard>().raiseHazard = true;
            }
            else if (nonButtonObject.GetComponent<NewMovingPlatform>() != null)
            {
                nonButtonObject.GetComponent<NewMovingPlatform>().allowedToMove = false;
            }
            else if (nonButtonObject.gameObject.activeSelf == true)
            {
                nonButtonObject.gameObject.SetActive(false);
            }
            else if (nonButtonObject.gameObject.activeSelf == false)
            {
                nonButtonObject.gameObject.SetActive(true);
            }
        }
    }

    public void PressurePlateTotalIncrease()
    {
        pressurePlateTotal++;
        //&& holdDownButtons
        if ((pressurePlateTotal >= gm.GetComponent<GameController>().totalPlayerCount) && (pressurePlateTotal > 1))
        {
            pressurePlateTotal = gm.GetComponent<GameController>().totalPlayerCount - 1;
            //Debug.Log("Running this code: " + pressurePlateTotal);
        }
    }

    public void BeatLevel()
    {
        //[TODO]Maybe add some sound or something for this type.
        gm.GetComponent<GameController>().BeatLevel();
    }
}
