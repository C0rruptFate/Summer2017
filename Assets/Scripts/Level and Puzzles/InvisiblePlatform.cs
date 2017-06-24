using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisiblePlatform : MonoBehaviour {

    private SpriteRenderer sR;

	// Use this for initialization
	void Start () {
        sR = GetComponent<SpriteRenderer>();
        //Maybe change from flipping active/inactive to an opacity depending on how close the player is.
        sR.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (gameObject.CompareTag("Ground"))
        {
            if (other.CompareTag("Player") || other.CompareTag("Enemy"))
            {
                sR.enabled = true;
            }
        }
        else if(gameObject.CompareTag("Wisp Stopper"))
        {
            if (other.CompareTag("Wisp"))
            {
                sR.enabled = true;
            }
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (gameObject.CompareTag("Ground"))
        {
            if (other.CompareTag("Player") || other.CompareTag("Enemy"))
            {
                sR.enabled = false;
            } 
        }
        else if (gameObject.CompareTag("Wisp Stopper"))
        {
            if (other.CompareTag("Wisp"))
            {
                sR.enabled = false;
            }
        }
    }
}
