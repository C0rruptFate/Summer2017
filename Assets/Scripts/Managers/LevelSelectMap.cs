using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class LevelSelectMap : MonoBehaviour {

    public List<GameObject> levelList;

    [HideInInspector]
    public GameObject wisp;

    private Player input_manager;

    [HideInInspector]//Use to find what direction the player is trying to move
    public float horizontalDir;

    [HideInInspector]//Use to find what direction the player is trying to move
    public float verticalDir;

    // Use this for initialization
    void Start () {
        wisp = GameObject.FindGameObjectWithTag("Wisp");
        input_manager = ReInput.players.GetPlayer("System");
    }
	
	// Update is called once per frame
	void Update () {

    }
}
