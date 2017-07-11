using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {

    private bool attachedToSomething;

    private GameObject whatIAmAttachedTo;

    [HideInInspector]
    public float popWait = 5f;

	// Use this for initialization
	void Start () {
        Invoke("PopBubbleInWater", popWait);
    }
	
	// Update is called once per frame
	void Update () {
		
        if (attachedToSomething)//what I am attached to stays with me.
        {
            whatIAmAttachedTo.transform.position = transform.position;
        }

	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.GetComponent<PlayerHealth>())
        {
            attachedToSomething = true;
            whatIAmAttachedTo = other.gameObject;
            other.transform.GetComponent<Rigidbody2D>().mass = 1;
        }
        else if (other.transform.GetComponent<EnemyHealth>())
        {
            attachedToSomething = true;
            whatIAmAttachedTo = other.gameObject;
            other.transform.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }

    public void PopBubble()
    {
        Destroy(gameObject);
    }

    void PopBubbleInWater()
    {
        if (whatIAmAttachedTo != null && whatIAmAttachedTo.transform.GetComponent<PlayerHealth>() != null)
        {
            whatIAmAttachedTo.transform.GetComponent<Rigidbody2D>().mass = whatIAmAttachedTo.transform.GetComponent<PlayerMovement>().inWaterMass;
        }
        Destroy(gameObject);
    }
}