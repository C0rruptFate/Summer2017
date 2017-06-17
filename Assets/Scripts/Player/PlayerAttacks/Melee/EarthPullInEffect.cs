using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthPullInEffect : MonoBehaviour {
    [HideInInspector]
    public float pullSpeed;

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Earth Player Basic Projectile(Clone)")
        {
            other.GetComponent<PlayerProjectileEarthBasic>().beingPulledIn = true;
            other.GetComponent<PlayerProjectileEarthBasic>().pullSpeed = pullSpeed;
            other.GetComponent<PlayerProjectileEarthBasic>().pulledInTarget = gameObject;
        }
        else if (other.gameObject.name == "Earth Player Special Projectile(Clone)")
        {
            other.GetComponent<PlayerProjectileEarthSpecial>().beingPulledIn = true;
            other.GetComponent<PlayerProjectileEarthSpecial>().pullSpeed = pullSpeed;
            other.GetComponent<PlayerProjectileEarthSpecial>().pulledInTarget = gameObject;
        }
    }
}
