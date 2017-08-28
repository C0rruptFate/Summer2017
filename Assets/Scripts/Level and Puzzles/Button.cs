using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    [HideInInspector]
    public bool pushed = false;

    [HideInInspector]
    public GameObject pressurePlateMaster;

    [HideInInspector]
    public bool iShouldBeActiveBool;

    private Vector3 startPosition;

    // Use this for initialization
    void Start () {
        //pressurePlateMaster = transform.parent.gameObject;

        startPosition = transform.position;

        if (!iShouldBeActiveBool)
        gameObject.SetActive(false);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if ((other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy")) && !pushed)
        {
            transform.position = new Vector3(startPosition.x, startPosition.y - 0.25f, startPosition.z);
            pushed = true;
            pressurePlateMaster.GetComponent<PressurePlate>().CheckandIncreaseButtons();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //Checks to see if a player or enemy is touching it. And if their is more than 1 player in the game
        if ((other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy")) && pressurePlateMaster.GetComponent<PressurePlate>().holdDownButtons)
        {
            transform.position = startPosition;
            pushed = false;
            pressurePlateMaster.GetComponent<PressurePlate>().CheckandDecreaseButtons();
            Debug.Log("Button released");
        }
    }

    public void IShouldBeActive()
    {
        //Debug.Log("Button is now active");
        gameObject.SetActive(true);
        pressurePlateMaster.GetComponent<PressurePlate>().PressurePlateTotalIncrease();
        //Debug.Log("Presure plat total: " + pressurePlateMaster.GetComponent<PressurePlate>().pressurePlateTotal);
    }
}
