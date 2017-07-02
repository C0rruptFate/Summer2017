using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingHazardTriggerPoint : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.CompareTag("Player") || other.CompareTag("Enemy")) && transform.parent.GetComponent<FallingHazard>().myHazard != null)
        {
            transform.parent.GetComponent<FallingHazard>().myHazard.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            //myHazard.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
