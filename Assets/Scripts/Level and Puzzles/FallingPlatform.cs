using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallingPlatform : MonoBehaviour {

    public float fallWaitTime = 1;

    private Rigidbody2D rb;

    private Vector3 startPosition;
    private Quaternion startRotation;

    public Text debugText;

    private bool alreadyFalling;
    // Use this for initialization
    public void Start () {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        startRotation = transform.rotation;
        debugText = GameObject.Find("Debug Text").GetComponent<Text>();
	}

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && !alreadyFalling)
        {
            debugText.text = "Got to here";
            //Animation for shaking platform.

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
        rb.bodyType = RigidbodyType2D.Kinematic;
        transform.position = startPosition;
        transform.rotation = startRotation;
        alreadyFalling = false;
    }
}
