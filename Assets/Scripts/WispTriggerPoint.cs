using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WispTriggerPoint : MonoBehaviour {

    public GameObject wispTriggerHitEffect;
    public float effectDuration;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wisp"))
        {
            GameObject myEffect = Instantiate(wispTriggerHitEffect, transform.position, transform.rotation);
            wispTriggerHitEffectStats(myEffect);
        }
    }

    void wispTriggerHitEffectStats(GameObject effect)
    {
        effect.GetComponent<DestroySelfRightAway>().waitToDestroyTime = effectDuration;
        effect.transform.parent = transform;
    }
}
