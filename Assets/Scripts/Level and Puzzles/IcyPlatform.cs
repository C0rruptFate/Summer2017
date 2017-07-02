using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcyPlatform : MonoBehaviour {

    public float slideSpeed = 50;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.GetComponent<PlayerMovement>() != null)
        {
            other.transform.GetComponent<PlayerMovement>().minSpeed = slideSpeed;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.GetComponent<PlayerMovement>() != null)
        {
            other.transform.GetComponent<PlayerMovement>().minSpeed = 0;
        }
    }
}
