using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {
    [Tooltip("The force added to me when I leave the water.")]
    public float jumpOutOfWaterForce = 3;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().inWater = true;
            other.GetComponent<PlayerMovement>().arialJumpsUsed = 0;
            other.GetComponent<Rigidbody2D>().mass = other.GetComponent<PlayerMovement>().inWaterMass;
            other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        else if (other.tag == "Enemy" && other.GetComponent<EnemyHealth>().element == Element.Water)//Water enemy is no longer swimming.
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
            other.GetComponent<Rigidbody2D>().mass = other.GetComponent<PlayerMovement>().outOfWaterMass;
            other.GetComponent<PlayerMovement>().arialJumpsUsed = 0;
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,jumpOutOfWaterForce),ForceMode2D.Impulse);
        }

        if (other.tag == "Enemy" && other.GetComponent<EnemyHealth>().element == Element.Water)//Water enemy is no longer swimming.
        {
            other.GetComponent<Rigidbody2D>().gravityScale = 1;
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpOutOfWaterForce), ForceMode2D.Impulse);
        }

        if (other.GetComponent<Bubble>() != null)
        {
            other.GetComponent<Bubble>().PopBubble();
        }
    }
}
