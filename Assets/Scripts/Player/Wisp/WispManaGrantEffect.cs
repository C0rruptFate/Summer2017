using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WispManaGrantEffect : MonoBehaviour {

    [HideInInspector]//The transform the wisp tells me to spawn at.
    public Transform targetTransform;

    [HideInInspector]//The transform the wisp tells me to spawn at.
    public GameObject attachedPlayer;
    [HideInInspector]//How much mana is granted.
    public int manaRegenAmount;
    [HideInInspector]//How much mana is granted.
    public float manaRegenRate;

    // Use this for initialization
    void Start () {
        InvokeRepeating("GainMana", 5, manaRegenRate);
    }
	
	// Update is called once per frame
	void Update () {
        //Tracks to it's location.
        transform.position = new Vector2(targetTransform.position.x, targetTransform.position.y);

    }
}
