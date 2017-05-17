using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour {

    [HideInInspector]
    public float projectileSpeed;
    [HideInInspector]
    public float projectileDamage;
    [HideInInspector]
    public float projectileHitStun;

    [HideInInspector]
    public float projectileMaxDuration;
    [HideInInspector]
    public float projectileBreakChance;
    [HideInInspector]
    public bool usesConstantForceProjectile = true;
    [HideInInspector]
    public bool breaksHittingWall = true;

    private float currentLife;
    private bool breaking = false;

    [HideInInspector]public GameObject player;
    [HideInInspector]public Element myElement;

    // Use this for initialization
    void Start () {

        if (player.GetComponent<PlayerController>() != null)
        {
            projectileSpeed = player.GetComponent<PlayerController>().projectileSpeed;
            projectileDamage = player.GetComponent<PlayerController>().projectileDamage;
            projectileHitStun = player.GetComponent<PlayerController>().projectileHitStun;
            projectileMaxDuration = player.GetComponent<PlayerController>().projectileMaxDuration;
            projectileBreakChance = player.GetComponent<PlayerController>().projectileBreakChance;
            usesConstantForceProjectile = player.GetComponent<PlayerController>().usesConstantForceProjectile;
            breaksHittingWall = player.GetComponent<PlayerController>().breaksHittingWall;
        }
        myElement = player.GetComponent<PlayerHealth>().element;

        if (gameObject.GetComponent<Collider>().enabled == false)
        {
            gameObject.GetComponent<Collider>().enabled = true;
        }

        currentLife = Time.time + projectileMaxDuration;
        float breakNumber = Random.Range(0, 100);
        if (breakNumber <= projectileBreakChance)
        {
            breaking = true;
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (Time.time >= currentLife)
        {
            Destroy(gameObject);
        }

        if (usesConstantForceProjectile)
        {
            transform.Translate(Vector3.right * projectileSpeed * Time.deltaTime);
        }


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            EnemyHealth health = other.gameObject.GetComponent<EnemyHealth>();
            //Rigidbody otherRB = other.gameObject.GetComponent<Rigidbody>();

            if (enemy && health)
            {
                //float distX = (other.transform.position.x - transform.position.x) * knockback;
                //float distY = (other.transform.position.y - transform.position.y) * knockback;
                //otherRB.velocity = new Vector3(0.0f, 0.0f, otherRB.velocity.z);
                //otherRB.AddForce(new Vector3(distX, distY, 0), ForceMode.Impulse);
                health.TakeDamage(gameObject, projectileDamage, projectileHitStun);
                Destroy(gameObject);
            }
            
        }else if (other.tag != ("Player") && breaking)
        {
            Destroy(gameObject);
        }
        else if (other.tag == ("Ground") && breaksHittingWall)
        {
            Destroy(gameObject);
        }
    }
}
