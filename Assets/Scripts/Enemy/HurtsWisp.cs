using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtsWisp : MonoBehaviour {

    [HideInInspector]
    public float damage;
    [HideInInspector]
    public float projectileSpeed;
    [HideInInspector]
    public float maxLife;
    [HideInInspector]
    public float hitStun;
    [HideInInspector]
    public Element element;
    [HideInInspector]
    public bool breaksHittingWall = false;

    private GameObject[] players;

    private float currentLife;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        //Reduces the life of the object at 0 it is destroyed.
        if (Time.time >= currentLife)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        transform.Translate(Vector2.right * projectileSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wisp"))
        {
            players = GameObject.FindGameObjectsWithTag("Player");

            foreach(GameObject player in players)
            {
                player.GetComponent<PlayerHealth>().TakeDamage(gameObject, damage, hitStun);
            }
        }
        else if (other.tag == ("Ground") && breaksHittingWall) //Gets destroyed when hitting the ground/walls
        {
            Destroy(gameObject);
        }
    }
}
