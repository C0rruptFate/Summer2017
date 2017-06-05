﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wisp : MonoBehaviour {

    [HideInInspector] //The transform location that the wisp will be flying to.
    public Transform targetLocation;
    private ParticleSystem ps;// the wisps particle system used to show active/inactive

    [HideInInspector]
    public bool moving = false; //flipped on when the wisp is moving used to help the wisp find it's target
    [Tooltip("Speed of the Wisp.")]
    public float speed; //How fast the wisp moves
    //public float attachedSpeed; 
    [Tooltip("How much time must take place between picking up and releasing the wisp")]
    public float wispCallTime = 1f;


    //Testing player Attachment
    private bool attachedToPlayer = false;
    [HideInInspector]//If I am attached to a player they will be here.
    public GameObject attachedPlayer;
    [HideInInspector]//This is who I could attach to, it gets transfered to attached player if the player I am touching calls the wisp.
    public GameObject playerICouldAttachTo;

    //Buffs to the player holding the wisp
    [Tooltip("How much mana I grant the attached player each tick.")]
    public float manaRegenAmount = 10f;
    [Tooltip("how quickly do I tick, in seconds.")]
    public float manaRegenRate = 5f;

    // Use this for initialization
    void Start () {
        targetLocation = gameObject.transform;
        ps = gameObject.GetComponent<ParticleSystem>();

	}
	
	// Update is called once per frame
	void Update () {
        //Used to change the Wisps particles.
        var shape = ps.shape;

        //if Moving will move to the target wisp location.
        if(moving)
        {
            shape.arcMode = ParticleSystemShapeMultiModeValue.Random;
            MoveToTarget();
        }

        //Turns off moving and plays the particle for idel.
        if (transform.position == targetLocation.position && moving && !attachedToPlayer)
        {
            moving = false;
            //Causes the particles to move clockwise
            shape.arcMode = ParticleSystemShapeMultiModeValue.Loop;
        }

        //attaches to the player I am touching and starts to grant them mana regen
        if (playerICouldAttachTo != null && playerICouldAttachTo.GetComponent<PlayerAttacks>().callingWisp && playerICouldAttachTo.GetComponent<PlayerAttacks>().callingWispTime >= 20)
        {
            attachedPlayer = playerICouldAttachTo;
            WispPlayerBuff();
        }

        //Removes from the attached player
        if(attachedPlayer != null && attachedPlayer.GetComponent<PlayerAttacks>().callingWisp && playerICouldAttachTo.GetComponent<PlayerAttacks>().callingWispTime < 20)
        {
            //Debug.Log("Remove Wisp from player");
            attachedPlayer = null;
            RemoveWispPlayerBuff();
        }

        //Keeps my location the same as the player I am attached to.
        if(attachedPlayer != null)
        {
            CurrentlyAttachedToPlayer();
        }


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //when touching a player tells me that I could attach to them.
        if (other.tag == "Player")
        {
            playerICouldAttachTo = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //Removes who I could attach to when I am no longer touching them.
        if (other.tag == "Player" && other.gameObject == playerICouldAttachTo)
        {
            playerICouldAttachTo = null;
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        //allows me to attach to a player when touchign them.
        if (other.tag == "Player" && attachedPlayer == null && other.GetComponent<PlayerAttacks>().callingWisp)
        {
            attachedPlayer = other.gameObject;
            WispPlayerBuff();
        }
        //Debug.Log("Touching " + other.name);
    }

    void MoveToTarget()
    {
        //Moves to the target transform over time.
        attachedPlayer = null;
        RemoveWispPlayerBuff();
        transform.position = Vector2.MoveTowards(transform.position, targetLocation.position, speed * Time.deltaTime);
        //Debug.Log("Moving To " + targetLocation.name);
    }

    void CurrentlyAttachedToPlayer()
    {
        //Keeps my location the same as the player I am attached to
        transform.position = new Vector2(attachedPlayer.transform.position.x, attachedPlayer.transform.position.y);
        //Debug.Log("I am attached to " + attachedPlayer.name);
    }

    void WispPlayerBuff()
    {
        //Causes me to grant the player mana when attached to them.
        InvokeRepeating("GainMana", 2, manaRegenRate);
        //used to show I am giving a player mana when attached to them. Will change with wisp system changes
        var emission = ps.emission;
        var shape = ps.shape;
        var main = ps.main;

        switch (attachedPlayer.GetComponent<PlayerHealth>().element)
        {
            case Element.Fire:
                main.startColor = Constants.fireColor;
                break;
            case Element.Ice:
                main.startColor = Constants.waterColor;
                break;
            case Element.Earth:
                main.startColor = Constants.earthColor;
                break;
            case Element.Wind:
                main.startColor = Constants.airColor;
                break;
        }

        shape.arcMode = ParticleSystemShapeMultiModeValue.Random;
        emission.rateOverTime = 0f;
        emission.rateOverDistance = 50f;
        emission.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(0, 100, 100, 0, manaRegenRate) });
    }
    void RemoveWispPlayerBuff()
    {
        //Moves my particles back to idle or moving when no longer attached to the player.
        var emission = ps.emission;
        var shape = ps.shape;
        var main = ps.main;

        main.startColor = Color.white;
        shape.arcMode = ParticleSystemShapeMultiModeValue.Random;
        emission.rateOverTime = 50f;
        emission.rateOverDistance = 0f;
        emission.SetBursts(new ParticleSystem.Burst[] { });
    }

    void GainMana()
    {
        //Grants the players mana when I am attached to them.
        if(attachedPlayer != null)
        {
            attachedPlayer.GetComponent<PlayerHealth>().GainMana(manaRegenAmount);
        }
    }
}
