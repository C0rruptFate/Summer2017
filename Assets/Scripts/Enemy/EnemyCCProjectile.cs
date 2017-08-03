using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCCProjectile : EnemyProjectile {
    //[HideInInspector]
    public GameObject target;
    public GameObject ccTriggerPoint;

    public override void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    void Update () {
		
	}

    public override void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, (projectileSpeed * Time.deltaTime));
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == ("Player"))
        {
            //Debug.Log("Player should take damage");
            PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
            PlayerHealth health = other.gameObject.GetComponent<PlayerHealth>();

            //If what I am colliding with has both a player Controller and Health script, deal damage to them and knock them back.
            if (playerMovement && health)
            {
                health.TakeDamage(gameObject, projectileDamage, projectileHitStun);
                //playerMovement.enabled = false;
                //other.GetComponent<PlayerAttacks>().enabled = false;
                //other.GetComponent<Rigidbody2D>().gravityScale = 0;
                GameObject constrictor = Instantiate(ccTriggerPoint, other.transform.position, ccTriggerPoint.transform.rotation);
                constrictor.GetComponent<CCTriggerPoint>().shooter = shooter;
                constrictor.GetComponent<CCTriggerPoint>().attachedTo = other.gameObject;
                constrictor.transform.parent = transform.parent;
                //If this is true it will destroy itself after hitting a single enemy false lets it hit several enemies.
                if (breaking)
                {
                    Destroy(gameObject);
                }
            }
        }
        else if (other.tag == ("Ground") && breaksHittingWall) //Gets destroyed when hitting the ground/walls
        {
            Destroy(gameObject);
        }
    }
}
