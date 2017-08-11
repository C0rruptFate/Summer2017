using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMapPathEffect : MonoBehaviour {

    public Transform point1;
    public Transform point2;
    public float speed = 12f;

    Vector3 direction;

    Transform destination;

    // Use this for initialization
    void Start () {
        SetDestination(point1);
        speed = 6;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);

        if (Vector3.Distance(transform.position, destination.position) < speed * Time.fixedDeltaTime)
        {
            SetDestination(destination == point1 ? point2 : point1);
            //transform.LookAt(destination);
        }
    }

    void SetDestination(Transform dest)
    {
        destination = dest;
        speed = 12f;
        direction = (destination.position - transform.position).normalized;
        transform.LookAt(destination);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<DestroySelfRightAway>() != null)
        {
            Destroy(other.gameObject);
        }
        if (other.GetComponent<OverWorldSmoke>() != null)
        {
            
            other.GetComponent<OverWorldSmoke>().StartCoroutine("DecreaseSmoke"); ;
        }
    }
}
