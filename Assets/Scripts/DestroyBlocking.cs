using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBlocking : MonoBehaviour {

    public bool blocking;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        blocking = GetComponentInParent<PlayerController>().blocking;
        if (!blocking)
        {
            Debug.Log("DESTROY block effect");
            Destroy(gameObject);
        }
	}
}
