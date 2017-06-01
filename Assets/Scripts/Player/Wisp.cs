﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wisp : MonoBehaviour {

    [HideInInspector] //The transform location that the wisp will be flying to.
    public Transform targetLocation;
    private ParticleSystem ps;
    private ParticleSystemShapeMultiModeValue arcMode = ParticleSystemShapeMultiModeValue.Loop;
    private float wispNextCall = 0.0f;

    //[HideInInspector]
    public bool moving = false;
    public float speed;
    public float attachedSpeed;
    [Tooltip("How much time must take place between picking up and releasing the wisp")]
    public float wispCallTime = 1f;


    //Testing player Attachment
    private bool attachedToPlayer = false;
    public GameObject attachedPlayer;
    public GameObject playerICouldAttachTo;

    //Buffs to the player holding the wisp
    public float manaRegenAmount = 10f;
    public float manaRegenRate = 5f;

    // Use this for initialization
    void Start () {
        targetLocation = gameObject.transform;
        ps = gameObject.GetComponent<ParticleSystem>();

	}
	
	// Update is called once per frame
	void Update () {
        var shape = ps.shape;
        if(moving)
        {
            shape.arcMode = ParticleSystemShapeMultiModeValue.Random;
            MoveToTarget();
        }

        if (transform.position == targetLocation.position && moving && !attachedToPlayer)
        {
            moving = false;
            //Causes the particles to move clockwise
            shape.arcMode = ParticleSystemShapeMultiModeValue.Loop;
        }

        //New
        //if (attachedToPlayer)
        //{
        //    //Causes the wisp to pulse and grant mana

        //    //transform.position = new Vector2(attachedPlayer.transform.position.x, attachedPlayer.transform.position.y);
        //    //Debug.Log("I am attached to " + attachedPlayer.name);
        //}

        if (playerICouldAttachTo != null && playerICouldAttachTo.GetComponent<PlayerAttacks>().callingWisp && Time.time > wispNextCall)
        {
            wispNextCall = Time.time + wispCallTime;
            attachedPlayer = playerICouldAttachTo;
            WispPlayerBuff();
        }

        if(attachedPlayer != null && attachedPlayer.GetComponent<PlayerAttacks>().callingWisp && Time.time > wispNextCall)
        {
            //Debug.Log("Remove Wisp from player");
            attachedPlayer = null;
            RemoveWispPlayerBuff();
        }

        if(attachedPlayer != null)
        {
            CurrentlyAttachedToPlayer();
        }


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            playerICouldAttachTo = other.gameObject;
        }
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" && other.gameObject == playerICouldAttachTo)
        {
            playerICouldAttachTo = null;
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {

        if (attachedPlayer == null && other.GetComponent<PlayerAttacks>().callingWisp && Time.time > wispNextCall)
        {
            attachedPlayer = other.gameObject;
            WispPlayerBuff();
        }
        //Debug.Log("Touching " + other.name);
        ////Used for attaching to a player 
        //if (other.tag == "Player" && other.GetComponent<PlayerAttacks>().callingWisp && Time.time > wispNextCall)
        //{
        //    wispNextCall = Time.time + wispCallTime;
        //    Debug.Log("Telling Wisp to attach to " + other.name);
        //    //CatchOrRelease(other.gameObject);
        //}
    }

    void MoveToTarget()
    {
        //Moves to the target transform over time.
        //if (gameObject.transform.parent != null)
        //{
        //    gameObject.transform.parent = null;
        //}
        attachedPlayer = null;
        RemoveWispPlayerBuff();
        transform.position = Vector2.MoveTowards(transform.position, targetLocation.position, speed * Time.deltaTime);
        //Debug.Log("Moving To " + targetLocation.name);
    }

    void CurrentlyAttachedToPlayer()
    {
        transform.position = new Vector2(attachedPlayer.transform.position.x, attachedPlayer.transform.position.y);
        //Debug.Log("I am attached to " + attachedPlayer.name);
    }

    void WispPlayerBuff()
    {
        var emission = ps.emission;
        var shape = ps.shape;

        switch (attachedPlayer.GetComponent<PlayerHealth>().element)
        {
            case Element.Fire:
                ps.startColor = Constants.fireColor;
                break;
            case Element.Ice:
                ps.startColor = Constants.waterColor;
                break;
            case Element.Earth:
                ps.startColor = Constants.earthColor;
                break;
            case Element.Wind:
                ps.startColor = Constants.airColor;
                break;
        }
            
        shape.arcMode = ParticleSystemShapeMultiModeValue.Random;
        emission.rateOverTime = 0f;
        emission.rateOverDistance = 50f;
        emission.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(0, 100, 100, 0, manaRegenRate) });
    }
    void RemoveWispPlayerBuff()
    {
        var emission = ps.emission;
        var shape = ps.shape;

        ps.startColor = Color.white;
        shape.arcMode = ParticleSystemShapeMultiModeValue.Random;
        emission.rateOverTime = 50f;
        emission.rateOverDistance = 0f;
        emission.SetBursts(new ParticleSystem.Burst[] { });
    }

    //Currently not being used
    //void CatchOrRelease(GameObject other)
    //{
    //    // Wisp becomes child of player who picked it up until it receives a new command.
    //    //New Remove parent
    //    //gameObject.transform.parent = gameObject.transform.parent == null ? other.transform : null;
    //    //New
    //    if(attachedPlayer == null)
    //    {
    //        attachedToPlayer = true;
    //        attachedPlayer = other;
    //        Debug.Log("Attaching to player: " + other.name);
    //    }
    //    else
    //    {
    //        attachedPlayer = null;
    //        attachedToPlayer = false;
    //        Debug.Log("No Longer Attached to player: " + other.name);
    //    }
    //}

}
