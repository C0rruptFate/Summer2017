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


    public float closestIWillGet;
    public float furthestIWillGet;
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

        target = GameObject.Find("Wisp");
    }

    void Update()
    {
        if (aimProjectile)
        {
            shootPoint.LookAt(target.transform.position, Vector3.up);
        }

        if (Time.time > newSwingTimer && target != null && dist <= furthestIWillGet)
        {
            Shoot();
            newSwingTimer = Time.time + swingTimer;
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
