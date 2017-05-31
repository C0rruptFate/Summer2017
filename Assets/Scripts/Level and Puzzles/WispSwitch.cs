using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WispSwitch : MonoBehaviour {

    public GameObject switchActiveEffect;

    private bool activated;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wisp" && !activated)
        {
            activated = true;
            Instantiate(switchActiveEffect, transform.position, switchActiveEffect.transform.rotation);
        }
    }
}
