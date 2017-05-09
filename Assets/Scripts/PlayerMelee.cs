using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour {

    [HideInInspector]
    public Transform myGun;
    [HideInInspector]
    public GameObject myPlayer;

    [Tooltip("How Long the hitbox stays out. 'Keep very small'")]
    private float meleeHitBoxLife = 0;

    [Tooltip("How much damge this object deals.")]
    private float meleeDamage;
    [Tooltip("How much damage this object deals. 'The size of what you are hitting will be important.'")]
    private float stunLockOut;

    private float currentLife;

    [HideInInspector]public GameObject player;
    [HideInInspector]public Element myElement;

    // Use this for initialization
    void Start () {
        meleeHitBoxLife = player.GetComponent<PlayerController>().meleeHitBoxLife;
        meleeDamage = player.GetComponent<PlayerController>().meleeDamage;
        stunLockOut = player.GetComponent<PlayerController>().meleeHitStun;
        myElement = player.GetComponent<PlayerController>().element;
        currentLife = Time.time + meleeHitBoxLife;
    }
	
	// Update is called once per frame
	void Update () {

        transform.position = myGun.transform.position;

        if (gameObject.GetComponent<Collider>().enabled == false)
        {
            gameObject.GetComponent<Collider>().enabled = true;
        }

        if (Time.time >= currentLife)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == ("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            EnemyHealth health = other.gameObject.GetComponent<EnemyHealth>();

            if (enemy && health)
            {
                health.TakeDamage(gameObject, meleeDamage, stunLockOut);
                Destroy(gameObject);
            }

        }
    }
}
