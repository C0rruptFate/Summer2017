using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthPullInEffect : MonoBehaviour {
    [HideInInspector]
    public float pullSpeed;

    [HideInInspector]
    public float effectDuration;

    public List<GameObject> projectilesIAttract;

    public void Start()
    {
        Invoke("EffectDurationFunction", effectDuration);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Earth Player Basic Projectile(Clone)")
        {
            other.GetComponent<PlayerProjectileEarthBasic>().beingPulledIn = true;
            other.GetComponent<Rigidbody2D>().gravityScale = 0;
            other.GetComponent<PlayerProjectileEarthBasic>().pullSpeed = pullSpeed;
            other.GetComponent<PlayerProjectileEarthBasic>().pulledInTarget = gameObject;
            projectilesIAttract.Add(other.gameObject);
        }
        else if (other.gameObject.name == "Earth Player Special Projectile(Clone)")
        {
            other.GetComponent<PlayerProjectileEarthSpecial>().beingPulledIn = true;
            other.GetComponent<Rigidbody2D>().gravityScale = 0;
            other.GetComponent<PlayerProjectileEarthSpecial>().pullSpeed = pullSpeed;
            other.GetComponent<PlayerProjectileEarthSpecial>().pulledInTarget = gameObject;
            projectilesIAttract.Add(other.gameObject);
        }
    }

    void EffectDurationFunction()
    {
        foreach(GameObject myProjectiles in projectilesIAttract)
        {
            myProjectiles.GetComponent<Rigidbody2D>().gravityScale = 0;
            //myProjectiles.GetComponent<PlayerProjectileEarthSpecial>().pulledInTarget = null;
        }
        Destroy(gameObject);
    }
}
