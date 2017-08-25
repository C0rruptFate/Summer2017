using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisiblePlatform : MonoBehaviour {

    [HideInInspector]
    public bool fadingIn;
    private float fadeInSpeed = 1f;
    private float fadeAlpha;
    [SerializeField]
    private float fadeInCap;


    // Use this for initialization
    void Start () {
        //Maybe change from flipping active/inactive to an opacity depending on how close the player is.

        if (gameObject.tag == "Ground")
        {
            fadeInCap = 10f;
        }
        else
        {
            fadeInCap = 1f;
        }
        this.GetComponent<SpriteRenderer>().material.color = new Color(this.GetComponent<SpriteRenderer>().material.color.r, this.GetComponent<SpriteRenderer>().material.color.g, this.GetComponent<SpriteRenderer>().material.color.b, 0f);
	}

    void Update()
    {
        if (fadingIn)
        {
            if (fadeAlpha < fadeInCap)
            {
                fadeAlpha = fadeAlpha + (fadeInSpeed) * Time.deltaTime;
                //Debug.Log("Fade Alpha: " + fadeAlpha);
            }

            this.GetComponent<SpriteRenderer>().material.color = new Color(this.GetComponent<SpriteRenderer>().material.color.r, this.GetComponent<SpriteRenderer>().material.color.g, this.GetComponent<SpriteRenderer>().material.color.b, fadeAlpha);
            if (fadeAlpha >= fadeInCap)
            {
                fadeAlpha = fadeInCap;
            }
        }
        else
        {
            if (fadeAlpha > 0)
            {
                fadeAlpha = fadeAlpha - fadeInSpeed * Time.deltaTime;
            }

            this.GetComponent<SpriteRenderer>().material.color = new Color(this.GetComponent<SpriteRenderer>().material.color.r, this.GetComponent<SpriteRenderer>().material.color.g, this.GetComponent<SpriteRenderer>().material.color.b, fadeAlpha);

        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (gameObject.CompareTag("Ground"))
        {
            if (other.CompareTag("Player") || other.CompareTag("Enemy"))
            {
                //Debug.Log(other + " Is close to it, but not touching it");
                if (!fadingIn)
                fadingIn = true;
            }
        }
        else if (gameObject.CompareTag("Wisp Stopper"))
        {
            if (other.CompareTag("Wisp"))
            {
                //Debug.Log(other + " Is close to it, but not touching it");
                if(!fadingIn)
                fadingIn = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (gameObject.CompareTag("Ground"))
        {
            if (other.CompareTag("Player") || other.CompareTag("Enemy"))
            {
                //sR.enabled = false;
                //Debug.Log(other + " Is NO LONGER CLOSE to it, but not touching it");
                fadingIn = false;
            }
        }
        else if (gameObject.CompareTag("Wisp Stopper"))
        {
            if (other.CompareTag("Wisp"))
            {
                //sR.enabled = false;
                //Debug.Log(other + " Is NO LONGER CLOSE to it, but not touching it");
                fadingIn = false;
            }
        }
    }
}
