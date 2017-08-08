using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectCamera : MonoBehaviour {

    public float speed = 20f; //How long the camera takes to move to it's target location.
    private Camera m_Camera; //The camera that is part of this rig

    private GameObject wisp;

    private void Awake()//On Spawn finds the camera component under this rig.
    {
        m_Camera = GetComponentInChildren<Camera>();
    }

    // Use this for initialization
    void Start () {
        wisp = GameObject.FindGameObjectWithTag("Wisp");
	}
	
	// Update is called once per frame
	void Update () {
        MoveCamera();
    }

    void MoveCamera()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(wisp.transform.position.x, wisp.transform.position.y, transform.position.z), speed * Time.deltaTime);
    }
}
