using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrike : MonoBehaviour {

    public GameObject sparks;

    public Element element;

    public float waitTime = 1;

    // Use this for initialization
    void Start() {
        GetComponent<BoxCollider2D>().enabled = false;
        Invoke("DealDamage", waitTime);
    }

    // Update is called once per frame
    void Update() {

    }

    void DealDamage()
    {//Summon effects
        //Enable Box Collider2D
    }

    void OnTriggerEnter2D(Collider2D other)
    {

    }
}
