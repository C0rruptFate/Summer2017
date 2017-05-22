using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour {

    [HideInInspector]
    public Transform myGun;
    [HideInInspector]
    public GameObject myPlayer;

    [HideInInspector]
    public float meleeHitBoxLife = 0;

    [HideInInspector]
    public float meleeDamage;
    [HideInInspector]
    public float stunLockOut;

    private float currentLife;

    [HideInInspector]public GameObject player;
    [HideInInspector]public Element myElement;

    // Use this for initialization
    void Start () {
        //Debug.Log("Jump Attack exists");
        if(player.GetComponent<PlayerController>() != null)
        {
            meleeHitBoxLife = player.GetComponent<PlayerController>().meleeHitBoxLife;
            meleeDamage = player.GetComponent<PlayerController>().meleeDamage;
            stunLockOut = player.GetComponent<PlayerController>().meleeHitStun;
        }
        myElement = player.GetComponent<PlayerHealth>().element;
        currentLife = Time.time + meleeHitBoxLife;
    }
	
	// Update is called once per frame
	void Update () {

        transform.position = myGun.transform.position;

        if (gameObject.GetComponent<Collider2D>().enabled == false)
        {
            gameObject.GetComponent<Collider2D>().enabled = true;
        }

        if (Time.time >= currentLife)
        {
            //Debug.Log("Jump Attack dead");
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
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
