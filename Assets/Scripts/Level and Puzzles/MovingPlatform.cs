using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    [Tooltip("How fast does this platform move?")]
    public float speed = 5;
    [Tooltip("Check if you want the platform to go vertically.")]
    public bool moveVertical = false;

    private Transform thisTransform;

    private Vector3 startPosition;

    private float velocity;

    // Use this for initialization
    void Start () {
        thisTransform = transform;
        startPosition = thisTransform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (!moveVertical)
        {
            Vector3 pos = startPosition + Vector3.right * Mathf.Sin(Time.time) * speed;
            velocity -= 14 * Time.deltaTime;

            if (velocity < 0)
                velocity = 0;

            pos.y = velocity + startPosition.y;
            thisTransform.position = pos; 
        }else
        {
            Vector3 pos = startPosition + Vector3.up * Mathf.Sin(Time.time) * speed;
            velocity -= 14 * Time.deltaTime;

            if (velocity < 0)
                velocity = 0;

            pos.x = velocity + startPosition.x;
            thisTransform.position = pos;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == ("Player") || other.transform.tag == ("Enemy") || other.transform.tag == ("Projectile"))
        {
            other.transform.parent = gameObject.transform;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.tag == ("Player") || other.transform.tag == ("Enemy") || other.transform.tag == ("Projectile"))
        {
            other.transform.parent = null;
        }
    }
}
