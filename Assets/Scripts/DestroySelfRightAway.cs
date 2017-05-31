using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfRightAway : MonoBehaviour {

    public float waitToDestroyTime;

	// Use this for initialization
	void Start () {
        Invoke("DestroyAfterX", waitToDestroyTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void DestroyAfterX()
    {
        Destroy(gameObject);
    }
}
