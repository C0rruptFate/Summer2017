using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfRightAway : MonoBehaviour {

    [Tooltip("Destroys this object after X seconds.")]
    public float waitToDestroyTime;

	// Use this for initialization
	void Start () {
        //Starts counting then destroys the object.
        Invoke("DestroyAfterX", waitToDestroyTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void DestroyAfterX()
    {
        //Destroys object
        Destroy(gameObject);
    }
}
