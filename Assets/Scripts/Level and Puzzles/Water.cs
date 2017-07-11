﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().inWater = true;
            other.GetComponent<PlayerMovement>().arialJumpsUsed = 0;
            other.GetComponent<Rigidbody2D>().mass = other.GetComponent<PlayerMovement>().inWaterMass;
        }
        else if (other.tag == "Enemy" && other.GetComponent<EnemyHealth>().element == Element.Ice)//Water enemy is no longer swimming.
        {
            other.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && !other.GetComponent<PlayerMovement>().inWater)
        {
            other.GetComponent<PlayerMovement>().inWater = true;
            other.GetComponent<Rigidbody2D>().mass = other.GetComponent<PlayerMovement>().inWaterMass;
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().inWater = false;
            other.GetComponent<Rigidbody2D>().mass = other.GetComponent<PlayerMovement>().outofWaterMass;
            other.GetComponent<PlayerMovement>().arialJumpsUsed = 0;
        }

        if (other.tag == "Enemy" && other.GetComponent<EnemyHealth>().element == Element.Ice)//Water enemy is no longer swimming.
        {
            other.GetComponent<Rigidbody2D>().gravityScale = 1;
        }

        if (other.GetComponent<Bubble>() != null)
        {
            other.GetComponent<Bubble>().PopBubble();
        }
    }
}
