using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparks : MonoBehaviour {

    [HideInInspector]
    public GameObject thunderStrikes;

	// Use this for initialization
	void Start () {
        Invoke("LightningStrikes", thunderStrikes.GetComponent<ThunderStrikes>().sparkToStrikeDelay);
	}

    void LightningStrikes()
    {
        GameObject newLightningStrke = Instantiate(thunderStrikes.GetComponent<ThunderStrikes>().lightningStrikes, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation);
        newLightningStrke.GetComponent<LightningStrike>().damage = thunderStrikes.GetComponent<ThunderStrikes>().damage;

        Destroy(gameObject);
    }
}
