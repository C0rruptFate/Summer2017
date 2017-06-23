using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBlocking : MonoBehaviour {

    [HideInInspector]public bool blocking;//Am I blocking

    [HideInInspector]
    public PlayerAttacks playerAttacks;
	// Use this for initialization
	void Start () {
        //Sets up the player actions script
        playerAttacks = GetComponentInParent<PlayerMovement>().playerAttacks;

    }
	
	// Update is called once per frame
	void Update () {
        //Used for 3D, we shouldn't worry about this.
        if (gameObject.GetComponentInParent<PlayerController>() != null)
        {
            blocking = GetComponentInParent<PlayerController>().blocking;
        }
        else
        {
            //Looks at the 2D scripts.
            blocking = playerAttacks.blocking;
        }
        
        //When the player isn't blocking destroyes the object.
        if (!blocking)
        {
            Debug.Log("DESTROY block effect");
            Destroy(gameObject);
        }
	}
}
