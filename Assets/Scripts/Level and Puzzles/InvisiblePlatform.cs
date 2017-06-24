using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisiblePlatform : MonoBehaviour {
    [SerializeField]
    private float physicalTouchingColor = 255f;
    [SerializeField]
    private float physicalCloseColor = 128f;

    [SerializeField]
    private float wispTouchingColor = 128f;
    [SerializeField]
    private float wispCloseColor = 60f;

    private bool wispClose;
    private bool wispTouching;
    private bool playerOrEnemyClose;
    private bool playerOrEnemyTouching;
    //WispClose
    //WispTouching
    //PlayerOrEnemyClose
    //PlayerOrEnemyTouching

    // Use this for initialization
    void Start () {
        //Maybe change from flipping active/inactive to an opacity depending on how close the player is.

        this.GetComponent<SpriteRenderer>().material.color = new Color(this.GetComponent<SpriteRenderer>().material.color.r, this.GetComponent<SpriteRenderer>().material.color.g, this.GetComponent<SpriteRenderer>().material.color.b, 0f);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.CompareTag("Ground"))
        {
            if (other.CompareTag("Player") || other.CompareTag("Enemy"))
            {
                //sR.enabled = true;
                Debug.Log(other + " Is close to it, but not touching it");
                this.GetComponent<SpriteRenderer>().material.color = new Color(this.GetComponent<SpriteRenderer>().material.color.r, this.GetComponent<SpriteRenderer>().material.color.g, this.GetComponent<SpriteRenderer>().material.color.b, physicalCloseColor);
            }
        }
        else if(gameObject.CompareTag("Wisp Stopper"))
        {
            if (other.CompareTag("Wisp"))
            {
                //sR.enabled = true;
                Debug.Log(other + " Is close to it, but not touching it");
                this.GetComponent<SpriteRenderer>().material.color = new Color(this.GetComponent<SpriteRenderer>().material.color.r, this.GetComponent<SpriteRenderer>().material.color.g, this.GetComponent<SpriteRenderer>().material.color.b, wispCloseColor);
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
                Debug.Log(other + " Is NO LONGER CLOSE to it, but not touching it");
                this.GetComponent<SpriteRenderer>().material.color = new Color(this.GetComponent<SpriteRenderer>().material.color.r, this.GetComponent<SpriteRenderer>().material.color.g, this.GetComponent<SpriteRenderer>().material.color.b, 0f);
            } 
        }
        else if (gameObject.CompareTag("Wisp Stopper"))
        {
            if (other.CompareTag("Wisp"))
            {
                //sR.enabled = false;
                Debug.Log(other + " Is NO LONGER CLOSE to it, but not touching it");
                this.GetComponent<SpriteRenderer>().material.color = new Color(this.GetComponent<SpriteRenderer>().material.color.r, this.GetComponent<SpriteRenderer>().material.color.g, this.GetComponent<SpriteRenderer>().material.color.b, 0f);
            }
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (gameObject.CompareTag("Ground"))
        {
            if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
            {
                //sR.enabled = true;
                Debug.Log(other + " Is TOUCHING it");
                this.GetComponent<SpriteRenderer>().material.color = new Color(this.GetComponent<SpriteRenderer>().material.color.r, this.GetComponent<SpriteRenderer>().material.color.g, this.GetComponent<SpriteRenderer>().material.color.b, physicalTouchingColor);
            }
        }
        else if (gameObject.CompareTag("Wisp Stopper"))
        {
            if (other.gameObject.CompareTag("Wisp"))
            {
                //sR.enabled = true;
                Debug.Log(other + " Is TOUCHING it");
                this.GetComponent<SpriteRenderer>().material.color = new Color(this.GetComponent<SpriteRenderer>().material.color.r, this.GetComponent<SpriteRenderer>().material.color.g, this.GetComponent<SpriteRenderer>().material.color.b, wispTouchingColor);
            }
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (gameObject.CompareTag("Ground"))
        {
            if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
            {
                //sR.enabled = false;
                Debug.Log(other + " Is NO LONGER TOUCHING it, but not touching it");
                this.GetComponent<SpriteRenderer>().material.color = new Color(this.GetComponent<SpriteRenderer>().material.color.r, this.GetComponent<SpriteRenderer>().material.color.g, this.GetComponent<SpriteRenderer>().material.color.b, 0f);
            }
        }
        else if (gameObject.CompareTag("Wisp Stopper"))
        {
            if (other.gameObject.CompareTag("Wisp"))
            {
                //sR.enabled = false;
                Debug.Log(other + " Is NO LONGER TOUCHING it, but not touching it");
                this.GetComponent<SpriteRenderer>().material.color = new Color(this.GetComponent<SpriteRenderer>().material.color.r, this.GetComponent<SpriteRenderer>().material.color.g, this.GetComponent<SpriteRenderer>().material.color.b, 0f);
            }
        }
    }

    void PlatformDisplay()
    {
        //WispClose
        //WispTouching
        //PlayerOrEnemyClose
        //PlayerOrEnemyTouching

        if (wispClose)
        {
            this.GetComponent<SpriteRenderer>().material.color = new Color(this.GetComponent<SpriteRenderer>().material.color.r, this.GetComponent<SpriteRenderer>().material.color.g, this.GetComponent<SpriteRenderer>().material.color.b, wispCloseColor);
        }
    }
}
