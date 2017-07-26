using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterCallWispProjectile : Hazard {

    [HideInInspector]
    public bool hurtsPlayers;

    [HideInInspector]
    public GameObject explosion;
    [HideInInspector]
    public float forceMagnitude;
    [HideInInspector]
    public float forceVariation;

    [HideInInspector]
    public GameObject targetLocation;

    private Vector3 realTargetLocation;

    private float currentLife;

	// Use this for initialization
	public override void Start () {

        realTargetLocation = targetLocation.transform.position;
	}

    public override void Update()
    {
        if (transform.position == new Vector3(realTargetLocation.x, realTargetLocation.y, realTargetLocation.z))
        {
            Explode();
        }
    }

    public override void FixedUpdate()
    {

        transform.position = Vector3.MoveTowards(transform.position, realTargetLocation, moveSpeed * Time.deltaTime);
        //transform.Translate(Vector2.right * projectileSpeed * Time.deltaTime);
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (hurtsPlayers == false)
        {
            if (other.tag == ("Enemy"))//If this hits an enemy deals damage to them.
            {
                Enemy enemy = other.gameObject.GetComponent<Enemy>();
                EnemyHealth health = other.gameObject.GetComponent<EnemyHealth>();
                //Rigidbody otherRB = other.gameObject.GetComponent<Rigidbody>();

                if (enemy && health)
                {
                    health.TakeDamage(gameObject, damage, hitStun);
                }
            }
        }
        else if (hurtsPlayers == true)
        {
            if (other.transform.tag == ("Player"))
            {
                //Debug.Log("Player should take damage");
                PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
                PlayerHealth health = other.gameObject.GetComponent<PlayerHealth>();

                //If what I am colliding with has both a player Controller and Health script, deal damage to them and knock them back.
                if (playerMovement && health)
                {
                    health.TakeDamage(gameObject, damage, hitStun);
                }
            }
        }
    }

    public void Explode()
    {
        GameObject myExplosion = Instantiate(explosion, transform.position, explosion.transform.rotation);
        ExplosionStats(myExplosion);
        Destroy(gameObject);
    }

    void ExplosionStats(GameObject explosion)
    {
        explosion.GetComponent<EnemyProjectile>().element = element;
        explosion.GetComponent<EnemyProjectile>().shooter = gameObject;
        explosion.GetComponent<EnemyProjectile>().projectileDamage = damage * 1.25f;
        explosion.GetComponent<EnemyProjectile>().projectileHitStun = hitStun;
        explosion.GetComponent<EnemyProjectile>().projectileMaxDuration = 0.2f;
        explosion.GetComponent<EnemyProjectile>().hurtsPlayers = hurtsPlayers;
        explosion.transform.localScale = new Vector3(transform.localScale.x * 1.25f, transform.localScale.y * 1.25f, transform.localScale.z * 1.25f);
        explosion.transform.Find("Fire_Player_Explosion").transform.Find("Fire ball").transform.localScale = new Vector3(4, 4, 4);
        explosion.GetComponent<PointEffector2D>().forceMagnitude = GetComponent<PointEffector2D>().forceMagnitude * 2f;
        explosion.GetComponent<PointEffector2D>().forceVariation = GetComponent<PointEffector2D>().forceVariation;

    }
}
