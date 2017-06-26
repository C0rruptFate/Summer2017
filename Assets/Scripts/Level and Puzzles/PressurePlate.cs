using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {

    public bool holdDownButtons = true;

    [HideInInspector]
    public int pressurePlatePushedCount;

    private int pressurePlateTotal;

    //private Transform Door;

    public GameObject[] myButtons;
    private int myButtonCount;

    private GameObject gm;
    // Use this for initialization
    void Start () {
        //Makes sure that buttons only need to be held down if there are 2 or more players in the game.
        gm = GameObject.Find("Game Manager");
        if (gm.GetComponent<GameController>().totalPlayerCount <= 1)
            holdDownButtons = false;

        foreach (Transform child in transform)
        {
            if (child.GetComponent<Button>() != null && child.gameObject.activeSelf)
            {
                //myButtons.Add(child.gameObject);
                pressurePlateTotal++;
            }
        }

        if (pressurePlateTotal >= gm.GetComponent<GameController>().totalPlayerCount)
        {
            int targetButton = pressurePlateTotal - 1;
            for (int i = pressurePlateTotal; i >= gm.GetComponent<GameController>().totalPlayerCount; i--)
            {
                myButtons[targetButton].GetComponent<Button>().IShouldBeActive();
                myButtons[targetButton].GetComponent<Button>().iShouldBeActiveBool = true;
                pressurePlateTotal--;
            }
        }
        //Debug.Log("Total Player Count " + gm.GetComponent<GameController>().totalPlayerCount);
        //int activePressurePlate = pressurePlateTotal + 1;
        //Debug.Log("pressurePlateTotal" + pressurePlateTotal);
        //int targetbutton = 0;
        //for (int i = activePressurePlate; i >= gm.GetComponent<GameController>().totalPlayerCount; i--)
        //{
        //    Debug.Log("Going to set button to active" + i);
            
        //    myButtons[targetbutton].GetComponent<Button>().IShouldBeActive();
        //    myButtons[targetbutton].GetComponent<Button>().iShouldBeActiveBool = true;
        //    targetbutton++;
        //    pressurePlateTotal--;
        //}
    }

    public void CheckandIncreaseButtons()
    {
        pressurePlatePushedCount++;
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
}
