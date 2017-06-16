using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour {

    //These are all set by the player attacks or action script.
    [HideInInspector]//where this attack comes from
    public Transform myGun;
    [HideInInspector]//Who attacked
    public GameObject myPlayer;

    [HideInInspector]//How long I will stay out for
    public float meleeHitBoxLife = 0;

    [HideInInspector]//How much damage I deal
    public float meleeDamage;
    [HideInInspector]//How long I stun enemies for
    public float stunLockOut;
    [HideInInspector]//How much of a knockback do I have
    public float knockBack;

    private float currentLife;//how long I have been out for

    [HideInInspector]public GameObject player;//used for 3D set up.
    [HideInInspector]//what element am I?
    public Element myElement;

    // Use this for initialization
    public virtual void Start () {
        //Debug.Log("Jump Attack exists");
        //These should not be used but are left over from 3D.
        if(player.GetComponent<PlayerController>() != null)
        {
            meleeHitBoxLife = player.GetComponent<PlayerController>().meleeHitBoxLife;
            meleeDamage = player.GetComponent<PlayerController>().meleeDamage;
            stunLockOut = player.GetComponent<PlayerController>().meleeHitStun;
        }

        //Sets my values from the player that called me
        myElement = player.GetComponent<PlayerHealth>().element;
        GetComponent<PointEffector2D>().forceMagnitude = knockBack;
        currentLife = Time.time + meleeHitBoxLife;
    }

    // Update is called once per frame
    public virtual void Update () {

        //where should I attack from?
        transform.position = myGun.transform.position;

        //Enables my collider 
        if (gameObject.GetComponent<Collider2D>().enabled == false)
        {
            gameObject.GetComponent<Collider2D>().enabled = true;
        }

        //Controls how long I can sit out for.
        if (Time.time >= currentLife)
        {
            //Debug.Log("Jump Attack dead");
            Destroy(gameObject);
        }
    }

    public virtual void OnCollisionEnter2D(Collision2D other)
    {
        //lets me do damage to enemies when touching them.
        if (other.transform.tag == ("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            EnemyHealth health = other.gameObject.GetComponent<EnemyHealth>();

            if (enemy && health)
            {
                health.TakeDamage(gameObject, meleeDamage, stunLockOut);
                //Uncomment if you only want it to hit a single guy, we can add a bool for hitting multipule guys if we want.
                //Destroy(gameObject);
            }

        }
    }
}
