using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderStrikes : MonoBehaviour {

    public GameObject lightningStrikes;

    public GameObject sparks;

    public Element element = Element.Wind;

    public float lightningStrikeFireRate;

    public float sparkToStrikeDelay = 1;

    public float damage;

    private float nextLightningStrike;

    [SerializeField]
    private List <GameObject> players;
	
	// Update is called once per frame
	void Update () {
		//Strike Lightning strike.

        if (Time.time > nextLightningStrike)
        {
            Sparks();
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.GetComponent<PlayerHealth>() != null)
        {
            players.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.GetComponent<PlayerHealth>() != null)
        {
            players.Remove(other.gameObject);
        }
    }

    void Sparks()
    {
        foreach (GameObject player in players)
        {
            nextLightningStrike = Time.time + lightningStrikeFireRate;
            if (player.GetComponent<PlayerMovement>().grounded)
            {
                GameObject newSpark = Instantiate(sparks, new Vector3(player.transform.position.x, player.transform.position.y - 1, player.transform.position.z), player.transform.rotation);
                newSpark.GetComponent<Sparks>().thunderStrikes = gameObject;
            }
        }
    }

    void LightningStrikes()
    {
        foreach (GameObject player in players)
        {
            nextLightningStrike = Time.time + lightningStrikeFireRate;
            if (player.GetComponent<PlayerMovement>().grounded)
            {
                Instantiate(sparks, new Vector3(player.transform.position.x, player.transform.position.y - 1, player.transform.position.z), player.transform.rotation);
                Instantiate(lightningStrikes, player.transform.position, player.transform.rotation);
            }
        }
    }
}
