using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour {

    public float fallWaitTime = 1;

    private Rigidbody2D rb;

    private Vector3 startPosition;
    private Quaternion startRotation;

    private bool alreadyFalling;
    // Use this for initialization
    public void Start () {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        startRotation = transform.rotation;
	}

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Animation for shaking platform.
            if(!alreadyFalling)
            Invoke("PlatformFalling", fallWaitTime);
        }
    }

    public void PlatformFalling()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        Invoke("ResetPlatform", fallWaitTime * 5);
    }

    public void ResetPlatform()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        rb.bodyType = RigidbodyType2D.Kinematic;
        alreadyFalling = false;
    }
}
