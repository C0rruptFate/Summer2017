using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyExplosion : MonoBehaviour {

    //explosion effect
    [SerializeField]
    private GameObject explosion_effect;

    //death delay
    [SerializeField]
    private float death_delay;
    private float death_timer;

    private void Start()
    {
        StartCoroutine("AutoDestroy");

        //instantiate explosion effect
        GameObject explosionEffect = Instantiate(explosion_effect, transform.position, Quaternion.Euler(270f, 0f, 0f));
        explosionEffect.transform.parent = gameObject.transform;
    }

    private IEnumerator AutoDestroy()
    {
        death_timer = death_delay;

        while (death_timer > 0)
        {
            death_timer -= Time.deltaTime;

            yield return null;
        }

        Destroy(gameObject);
    }
}
