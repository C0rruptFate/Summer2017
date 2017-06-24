using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWispStopper : MonoBehaviour {

    private GameObject wisp;

    public float wispStopDelay;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wisp")
        {
            //Debug.Log("Wisp tossed");
            wisp = other.gameObject;
            Invoke("StopWispMovement", wispStopDelay);
        }
    }

    void StopWispMovement()
    {
        wisp.GetComponent<Rigidbody2D>().velocity = new Vector3(0,0,0);
        wisp.GetComponent<Wisp>().moving = false;
    }
}
