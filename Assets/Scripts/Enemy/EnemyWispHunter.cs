using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWispHunter : Enemy {

    public Transform shootPoint;
    public GameObject projectile;
    public bool aimProjectile = false;
    public float projectileSpeed;
    public float projectileMaxDuration;
    public float projectileBreakChance;
    public bool projectileBreaksHittingWall = true;
    //private bool hurtsPlayers = true;
    public float aggroRange = 10f;
    public int shotsPerRound = 5;


    public float closestIWillGet;
    public float furthestIWillGet;
    private float dist;
    //private int numberOfShots;

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
        target = GameObject.Find("Wisp");
    }

    void Update()
    {
        dist = Vector2.Distance(target.transform.position, gameObject.transform.position);
        if (aimProjectile && dist <= aggroRange)
        {
            shootPoint.LookAt(target.transform.position, Vector3.up);
        }

        if (Time.time > newSwingTimer && target != null && dist <= aggroRange)
        {
            //Shoot();
            Firing();
            newSwingTimer = Time.time + swingTimer;
        }
    }

    void Firing()
    {
        for(int i = 0; i < shotsPerRound; i++)
        {
            Invoke("Shoot", i - (i * 0.75f));
        }
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
        projectile.GetComponent<HurtsWisp>().damage = damage;
        projectile.GetComponent<HurtsWisp>().projectileSpeed = projectileSpeed;
        projectile.GetComponent<HurtsWisp>().projectileMaxDuration = projectileMaxDuration;
        projectile.GetComponent<HurtsWisp>().hitStun = hitStun;
        projectile.GetComponent<HurtsWisp>().element = element;
        projectile.GetComponent<HurtsWisp>().breaksHittingWall = projectileBreaksHittingWall;

    }
}
