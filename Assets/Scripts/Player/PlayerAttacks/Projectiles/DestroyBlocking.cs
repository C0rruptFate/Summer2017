using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBlocking : MonoBehaviour {

    [HideInInspector]public bool blocking;//Am I blocking

    [HideInInspector]
    public PlayerAttacks playerAttacks;

    [HideInInspector]
    public GameObject player;
	// Use this for initialization
	void Start () {
        //Debug.Log("Defend object came into existence.");
        //Sets up the player actions script
        playerAttacks = player.GetComponent<PlayerAttacks>();
        if (playerAttacks == null)
        {
            Debug.Log("Can't find player attacks");
        }

    }
	
	// Update is called once per frame
	void Update () {

        transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, player.transform.position.z);

        //When the player isn't blocking destroyes the object.
        if (!playerAttacks.blocking)
        {
            //Debug.Log("DESTROY block effect");
            Destroy(gameObject);
        }
	}
}
