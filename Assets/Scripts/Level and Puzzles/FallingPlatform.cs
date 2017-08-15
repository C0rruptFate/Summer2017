using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        if (other.gameObject.CompareTag("Player") && !alreadyFalling)
        {
            //Animation for shaking platform.

            Invoke("PlatformFalling", fallWaitTime);
        }
    }

    public void PlatformFalling()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        alreadyFalling = true;
        Invoke("ResetPlatform", fallWaitTime * 5);
    }

    public void ResetPlatform()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.position = startPosition;
        transform.rotation = startRotation;
        alreadyFalling = false;
    }
}
