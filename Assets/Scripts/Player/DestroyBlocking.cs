using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBlocking : MonoBehaviour {

    [HideInInspector]public bool blocking;

    public PlayerAttacks playerAttacks;
	// Use this for initialization
	void Start () {
        playerAttacks = GetComponentInParent<PlayerMovement>().playerAttacks;

    }
	
	// Update is called once per frame
	void Update () {
        if (gameObject.GetComponentInParent<PlayerController>() != null)
        {
            blocking = GetComponentInParent<PlayerController>().blocking;
        }
        else
        {
            blocking = playerAttacks.blocking;
        }
        
        if (!blocking)
        {
            Debug.Log("DESTROY block effect");
            Destroy(gameObject);
        }
	}
}
