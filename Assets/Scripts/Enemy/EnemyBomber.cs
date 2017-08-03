using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomber : Enemy {

    public GameObject bomb;
    public float projectileMaxDuration;
    public float explosion_radius;
    public float explosion_force;
    private bool hurtsPlayers = true;

    private Transform dropPoint;

    protected override void Start()
    {
        base.Start();

        dropPoint = transform.Find("Drop Point");
    }

    void Update()
    {
        if (Time.time > newSwingTimer)
        {
            DropBomb();
            newSwingTimer = Time.time + swingTimer;
        }
    }

    void DropBomb()
    {
        GameObject myProjectile = Instantiate(bomb, dropPoint.position, dropPoint.rotation);
        ProjectileStats(myProjectile);
    }

    void ProjectileStats(GameObject myProjectile)
    {
        //Set up shooter for the projectile
        myProjectile.transform.parent = enemyWeaponParent.transform;
        myProjectile.GetComponent<EnemyProjectile>().element = element;
        myProjectile.GetComponent<EnemyProjectile>().shooter = gameObject;
        myProjectile.GetComponent<EnemyProjectile>().projectileDamage = damage;
        myProjectile.GetComponent<EnemyProjectile>().projectileHitStun = hitStun;
        myProjectile.GetComponent<EnemyProjectile>().projectileMaxDuration = projectileMaxDuration;
        myProjectile.GetComponent<EnemyProjectile>().hurtsPlayers = hurtsPlayers;
        if (myProjectile.GetComponent<Bomb>() != null)
        {
            myProjectile.GetComponent<Bomb>().explosion_radius = explosion_radius;
            myProjectile.GetComponent<Bomb>().explosion_force = explosion_force;
        }
    }
}
