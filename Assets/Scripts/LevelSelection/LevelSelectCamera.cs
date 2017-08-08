using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectCamera : MonoBehaviour {

    public float speed = 20f; //How long the camera takes to move to it's target location.

    private GameObject wisp;

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
