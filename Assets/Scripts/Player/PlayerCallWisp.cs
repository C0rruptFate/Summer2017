using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.UI;

public class PlayerCallWisp : MonoBehaviour
{

    //Wisp Settings 
    #region
    [HideInInspector]//The transform that I will be calling the wisp to.
    public Transform myWispTargetLocation;
    [HideInInspector]//The Wisp that I will be telling where to go.
    public GameObject wisp;
    [HideInInspector]//Am I currently calling the Wisp?
    public bool callingWisp = false;
    #endregion

    [HideInInspector]//My HP script
    public PlayerHealth playerHealth;
    [HideInInspector]//What player # is controlling me.
    public int playerNumber = 1;
    protected Player input_manager;
    protected int player_id;

    // Use this for initialization
    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerNumber = playerHealth.playerNumber;
        player_id = playerNumber - 1;
        input_manager = ReInput.players.GetPlayer(player_id);
        //Finds the wisp target location object, changes it's name, and removes it as a child.
        myWispTargetLocation = transform.Find("Wisp Target Location");
        myWispTargetLocation.name = gameObject.name + "Wisp Target Location";
        myWispTargetLocation.parent = null;
        //Find the wisp object
        wisp = GameObject.Find("Wisp");
        if (wisp == null)
        {//If the wisp can't be found it will inform the designer.
            Debug.LogError("Can't find the Wisp, it might not be added to the scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Calls Wisp and ticks up how long it has been held down. When greater than 20 the Wisp will attach to you.
        //Debug.Log("Callwisp" + Input.GetAxisRaw("CallWisp" + playerNumber));
        if (input_manager.GetAxis("Call_Wisp") == 1f)//was > 0.25f
        {
            CallWisp();
            callingWisp = true;
        }//Stops calling the Wisp when the button isn't held down.
        else if (input_manager.GetAxis("Call_Wisp") == 0f)
        {
            callingWisp = false;
        }
    }

    public void CallWisp()
    {
        //Moves target location to the player
        myWispTargetLocation.position = gameObject.transform.position;
        //Tells the Wisp to move to the targeted location
        wisp.GetComponent<WispScript>().targetLocation = myWispTargetLocation;
        wisp.GetComponent<WispScript>().moving = true;
        //callingWisp = false;
    }
}
