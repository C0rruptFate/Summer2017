using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {

    public bool holdDownButtons = true;

    [HideInInspector]
    public int pressurePlatePushedCount;

    [HideInInspector]
    public int pressurePlateTotal;

    //private Transform Door;

    public GameObject[] myButtons;
    private int myButtonCount;

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
        }

        //if (pressurePlateTotal >= gm.GetComponent<GameController>().totalPlayerCount)
        int targetButton = pressurePlateTotal - 1;
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
                Debug.Log("I " + i + "Pressure Plat Total: " + pressurePlateTotal);
            }
        }
    }

    public void CheckandIncreaseButtons()
    {
        pressurePlatePushedCount++;
        if (holdDownButtons == false)
        {
            //Needs to check for only active buttons
            pressurePlateTotal = activeButtonCount;
        }

        if (pressurePlatePushedCount > pressurePlateTotal)
            pressurePlatePushedCount = pressurePlateTotal;
        

        if (pressurePlatePushedCount == pressurePlateTotal)
        {
            foreach (Transform child in transform)
            {
                if(child.name == "Door")
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
    }

    public void CheckandDecreaseButtons()
    {
        pressurePlatePushedCount--;
        if (pressurePlatePushedCount < 0)
            pressurePlatePushedCount = 0;

            foreach (Transform child in transform)
            {
                if (child.name == "Door")
                {
                    child.gameObject.SetActive(true);
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
            Debug.Log("Running this code: " + pressurePlateTotal);
        }
    }
}
