using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingHazard : MonoBehaviour {

    private Transform triggerPoint;

    [HideInInspector]
    public GameObject myHazard;

    // Use this for initialization
    void Start () {
        myHazard = transform.Find("Hazard").gameObject;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            myHazard.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
        Debug.Log("Found: " + other.tag);
    }
}
