using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : EnemyProjectile
{

    //explosion fields
    [SerializeField]
    private GameObject explosion;
    [HideInInspector]
    public float explosion_radius;
    [HideInInspector]
    public float explosion_force;

    //public override void Start()
    //{
    //    //initialize countdown timer
    //    base.Start();
    //    countdown_timer = countdown_time;
    //}

    public override void FixedUpdate()
    {
        if (Time.time >= currentLife)
        {
            Explode();
        }
    }

    private void Explode()
    {
        //instantiate explosion and set radius / explosion force
        GameObject temp_explosion = Instantiate(explosion, transform.position, transform.rotation);
        temp_explosion.GetComponent<CircleCollider2D>().radius = explosion_radius;
        ExplosionStats(temp_explosion);
        PointEffector2D point_effector = temp_explosion.GetComponent<PointEffector2D>();
        point_effector.forceMagnitude = explosion_force;

        //destroy grenade
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Explode();
        }
    }

    void ExplosionStats(GameObject explosion)
    {
        explosion.GetComponent<EnemyProjectile>().element = element;
        explosion.GetComponent<EnemyProjectile>().shooter = gameObject;
        explosion.GetComponent<EnemyProjectile>().projectileDamage = projectileDamage;
        explosion.GetComponent<EnemyProjectile>().projectileHitStun = projectileHitStun;
        explosion.GetComponent<EnemyProjectile>().projectileMaxDuration = 0.2f;
        explosion.GetComponent<EnemyProjectile>().hurtsPlayers = hurtsPlayers;
    }
}
