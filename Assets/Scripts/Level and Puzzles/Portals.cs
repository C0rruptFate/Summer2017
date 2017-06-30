using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portals : MonoBehaviour {

    public GameObject sisterPortal;

    [HideInInspector]
    public Transform myOutPoint;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.position, transform.localScale);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(myOutPoint.position, transform.localScale);
    }

    void Start()
    {
        myOutPoint = transform.Find("Portal Out Point");

        if (sisterPortal == null)
        {
            Debug.LogError("Missing Sister Portal for: " + gameObject.name);
        }

        if (myOutPoint == null)
        {
            Debug.LogError("Missing My Out Point for: " + gameObject.name);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.GetComponent<PlayerHealth>() != null || other.transform.GetComponent<EnemyHealth>() != null)
        {
            other.transform.position = sisterPortal.GetComponent<Portals>().myOutPoint.position;
        }
    }
}
