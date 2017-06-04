using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WispSwitch : MonoBehaviour {

    public GameObject switchActiveEffect;

    [HideInInspector]
    public bool activated;

    private Transform torchLightSport;
    private Transform parentObject;

	// Use this for initialization
	void Start () {
        torchLightSport = transform.Find("Trigger Location");
        parentObject = transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Should Wisp switch hit");
        if (other.tag == "Wisp" && !activated)
        {
            Debug.Log("Wisp switch hit");
            activated = true;
            Instantiate(switchActiveEffect, torchLightSport.position, switchActiveEffect.transform.rotation);
            parentObject.GetComponent<WispSwitchChecker>().CheckSwitches();
        }
    }
}
