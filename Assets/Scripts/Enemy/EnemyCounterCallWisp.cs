﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounterCallWisp : Enemy {

    public Transform shootPoint;
    public GameObject projectile;
    public GameObject explosion;
    public bool aimProjectile = false;
    public float projectileSpeed;
    public float projectileMaxDuration;
    public float projectileBreakChance;
    public float forceMagnitude;
    public float forceVariation;
    public bool projectileBreaksHittingWall = true;
    public float aggroRange = 10f;
    private bool hurtsPlayers = true;

    private GameObject wisp;


    private float dist;
    private GameObject enemyWeaponParent;

    void OnDrawGizmos()
    {
        //Wire for start position
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(shootPoint.position, shootPoint.localScale);
    }

    protected override void Start()
    {
        base.Start();
        enemyWeaponParent = GameObject.Find("Enemy Attacks");
        if (!enemyWeaponParent)//If it can't find the weapon parent it will create one (the first player on each level should create this automatically).
        {
            enemyWeaponParent = new GameObject("Enemy Attacks");
        }

        //numberOfShots = shotsPerRound;
        wisp = GameObject.Find("Wisp");
    }

    void Update()
    {
        foreach (GameObject player in wisp.GetComponent<Wisp>().players)
        {
            
            if (player.GetComponent<PlayerAttacks>().callingWisp && Time.time > newSwingTimer)
            {
                Transform possibleTarget = wisp.GetComponent<Wisp>().targetLocation;
                dist = Vector2.Distance(possibleTarget.transform.position, gameObject.transform.position);

                if (dist <= aggroRange)
                {
                    target = possibleTarget.gameObject;
                    shootPoint.LookAt(target.transform.position, Vector3.up);
                    Shoot();
                    newSwingTimer = Time.time + swingTimer;
                }

            }
        }
    }

    public override void FixedUpdate()
    {
    }

    void Shoot()
    {
        if (aimProjectile)
        {
            GameObject myProjectile = Instantiate(projectile, shootPoint.position, shootPoint.transform.Find("Direction").transform.rotation);
            ProjectileStats(myProjectile);
        }
        else
        {
            GameObject myProjectile = Instantiate(projectile, shootPoint.position, shootPoint.transform.rotation);
            ProjectileStats(myProjectile);
        }
    }

    void ProjectileStats(GameObject projectile)
    {
        projectile.transform.parent = enemyWeaponParent.transform;
        projectile.GetComponent<CounterCallWispProjectile>().damage = damage;
        projectile.GetComponent<CounterCallWispProjectile>().moveSpeed = projectileSpeed;
        projectile.GetComponent<CounterCallWispProjectile>().hitStun = hitStun;
        projectile.GetComponent<CounterCallWispProjectile>().element = element;
        projectile.GetComponent<CounterCallWispProjectile>().targetLocation = target;
        projectile.GetComponent<CounterCallWispProjectile>().hurtsPlayers = hurtsPlayers;
        projectile.GetComponent<CounterCallWispProjectile>().explosion = explosion;
        projectile.GetComponent<PointEffector2D>().forceMagnitude = forceMagnitude;
        projectile.GetComponent<PointEffector2D>().forceVariation = forceVariation;


    }
}