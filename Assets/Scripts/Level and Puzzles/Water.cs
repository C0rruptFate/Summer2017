using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().floatingOnWater = true;
            other.GetComponent<PlayerMovement>().arialJumpsUsed = 0;
            other.GetComponent<PlayerMovement>().maxSpeed = other.GetComponent<PlayerMovement>().maxSpeed * 2;
            other.GetComponent<PlayerMovement>().runForce = other.GetComponent<PlayerMovement>().runForce * 2;
            //Debug.Log("player enters water");
            if (other.GetComponent<PlayerHealth>().element == Element.Ice)
            {
                other.GetComponent<PlayerMovement>().maxSpeed = other.GetComponent<PlayerMovement>().maxSpeed * 2;
                other.GetComponent<PlayerMovement>().runForce = other.GetComponent<PlayerMovement>().runForce * 2;
            }
        }

        if (other.tag == "Enemy" && other.GetComponent<EnemyHealth>().element == Element.Ice)//Water enemy is no longer swimming.
        {
            other.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().floatingOnWater = false;
            other.GetComponent<PlayerMovement>().inWater = false;
            other.GetComponent<Rigidbody2D>().mass = other.GetComponent<PlayerMovement>().outofWaterMass;
            other.GetComponent<PlayerMovement>().arialJumpsUsed = 0;
            other.GetComponent<PlayerMovement>().maxSpeed = other.GetComponent<PlayerMovement>().maxSpeed / 2;
            other.GetComponent<PlayerMovement>().runForce = other.GetComponent<PlayerMovement>().runForce / 2;
            //Debug.Log("player left water");
            if (other.GetComponent<PlayerHealth>().element == Element.Ice)
            {
                other.GetComponent<PlayerMovement>().maxSpeed = other.GetComponent<PlayerMovement>().maxSpeed / 2;
                other.GetComponent<PlayerMovement>().runForce = other.GetComponent<PlayerMovement>().runForce / 2;
            }
        }

        if (other.tag == "Enemy" && other.GetComponent<EnemyHealth>().element == Element.Ice)//Water enemy is no longer swimming.
        {
            other.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
}
