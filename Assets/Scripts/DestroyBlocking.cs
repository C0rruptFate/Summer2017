using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBlocking : MonoBehaviour {

    [HideInInspector]public bool blocking;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.GetComponentInParent<PlayerController>() != null)
        {
            blocking = GetComponentInParent<PlayerController>().blocking;
        }
        else
        {
            blocking = GetComponentInParent<PlayerMovement>().blocking;
        }
        
        if (!blocking)
        {
            Debug.Log("DESTROY block effect");
            Destroy(gameObject);
        }
	}
}
