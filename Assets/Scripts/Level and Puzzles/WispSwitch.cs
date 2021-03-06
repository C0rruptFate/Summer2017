﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WispSwitch : MonoBehaviour {

    public GameObject switchActiveEffect;//The effect that is played when this switch is flipped

    [HideInInspector]//If this switch has been activated
    public bool activated;

    private Transform torchLightSpot;//Where to spawn the effect when this has been activated
    //private Transform parentObject;//What door/end level or other object I am a part of.
    public GameObject[] switchCheckers;

    private GameObject unlitTorch;

	// Use this for initialization
	void Start () {
        torchLightSpot = transform.Find("Trigger Location");//Sets the position of where the effect should spawn when this is active.
        //parentObject = transform.parent;//Sets what door or other object I am a part of, used to solve puzzles.
        unlitTorch = transform.Find("Wisp Trigger Point").gameObject;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Should Wisp switch hit");
        //If the wisp flys though me and I am not active set me to active, play the effect and let my parent know.
        if (other.tag == "Wisp" && !activated)
        {
            //Debug.Log("Wisp switch hit");
            activated = true;
            Instantiate(switchActiveEffect, torchLightSpot.position, transform.rotation);
            Destroy(unlitTorch);

            foreach (GameObject switchChecker in switchCheckers)
            {
                if (switchChecker.GetComponent<WispSwitchChecker>() != null)
                {
                    switchChecker.GetComponent<WispSwitchChecker>().CheckSwitches();
                }
                else if (switchChecker.GetComponent<NewMovingPlatform>() != null)
                {
                    switchChecker.GetComponent<NewMovingPlatform>().allowedToMove = true;
                }
            }
            //if (parentObject != null)
            //{
            //    parentObject.GetComponent<WispSwitchChecker>().CheckSwitches();
            //}
        }
    }
}
