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

    // Use this for initialization
    void Start () {
        //pressurePlateMaster = transform.parent.gameObject;

        if (!iShouldBeActiveBool)
        gameObject.SetActive(false);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if ((other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy")) && !pushed)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.25f, transform.position.z);
            pushed = true;
            pressurePlateMaster.GetComponent<PressurePlate>().CheckandIncreaseButtons();
            Debug.Log("Button pushed");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //Checks to see if a player or enemy is touching it. And if their is more than 1 player in the game
        if ((other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy")) && pressurePlateMaster.GetComponent<PressurePlate>().holdDownButtons)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z);
            pushed = false;
            pressurePlateMaster.GetComponent<PressurePlate>().CheckandDecreaseButtons();
            Debug.Log("Button released");
        }
    }

    public void IShouldBeActive()
    {
        Debug.Log("Button is now active");
        gameObject.SetActive(true);
        pressurePlateMaster.GetComponent<PressurePlate>().PressurePlateTotalIncrease();
        Debug.Log("Presure plat total: " + pressurePlateMaster.GetComponent<PressurePlate>().pressurePlateTotal);
    }
}
