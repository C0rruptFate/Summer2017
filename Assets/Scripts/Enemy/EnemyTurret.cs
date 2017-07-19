using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : Enemy {

    public float sightRange = 7;
    public Transform idleTarget;
    [HideInInspector]
    public GameObject shootPoint;
    public GameObject laserSight;
    public GameObject projectile;
    public bool aimProjectile = false;
    public float projectileSpeed;
    public float projectileMaxDuration;
    public float projectileBreakChance;
    public bool projectileBreaksHittingWall = true;
    public float multiShotCount;
    public float multiShotDelay;
    private bool hurtsPlayers = true;

    private GameObject enemyWeaponParent;

    void OnDrawGizmos()
    {
        //Wire for start position
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(idleTarget.position, idleTarget.localScale);
    }

    protected override void Start()
    {
        base.Start();
        shootPoint = transform.Find("Shoot Point").gameObject;
        laserSight.GetComponent<TurretLaserSight>().myTurret = gameObject;
        shootPoint.name = "Laser Sight for " + gameObject.name;
        shootPoint.transform.parent = null;
        enemyWeaponParent = GameObject.Find("Enemy Attacks");
        if (!enemyWeaponParent)//If it can't find the weapon parent it will create one (the first player on each level should create this automatically).
        {
            enemyWeaponParent = new GameObject("Enemy Attacks");
        }
        target = idleTarget.gameObject;
    }

    void Update()
    {
        //if (aimProjectile)
        //{
        //    shootPoint.LookAt(target.transform.position, Vector3.up);
        //}

        if (Time.time > newSwingTimer && target != idleTarget.gameObject)
        {
            for (int i = 0; i < multiShotCount; i++)
            {
                Invoke("Shoot", multiShotDelay + i * 0.5f);
            }
            newSwingTimer = Time.time + swingTimer;
        }

        Vector3 relativePos = target.transform.position - shootPoint.transform.position;
        shootPoint.transform.rotation = Quaternion.LookRotation(relativePos);
    }

    public override void FixedUpdate()
    {
        //shootPoint.LookAt(new Vector2(target.transform.position.x, target.transform.position.y + 5));

    }

    //public override void TargetSelection()
    //{
    //    base.TargetSelection();
    //}

    void Shoot()
    {
        if (aimProjectile)
        {
            GameObject myProjectile = Instantiate(projectile, shootPoint.transform.position, shootPoint.transform.Find("Direction").transform.rotation);
            ProjectileStats(myProjectile);
        }
        else
        {
            GameObject myProjectile = Instantiate(projectile, shootPoint.transform.position, shootPoint.transform.rotation);
            ProjectileStats(myProjectile);
        }
    }

    void ProjectileStats(GameObject myProjectile)
    {
        myProjectile.transform.parent = enemyWeaponParent.transform;
        myProjectile.GetComponent<EnemyProjectile>().element = element;
        myProjectile.GetComponent<EnemyProjectile>().shooter = gameObject;
        myProjectile.GetComponent<EnemyProjectile>().projectileSpeed = projectileSpeed;
        myProjectile.GetComponent<EnemyProjectile>().projectileDamage = damage;
        myProjectile.GetComponent<EnemyProjectile>().projectileHitStun = hitStun;
        myProjectile.GetComponent<EnemyProjectile>().projectileMaxDuration = projectileMaxDuration;
        myProjectile.GetComponent<EnemyProjectile>().projectileBreakChance = projectileBreakChance;
        myProjectile.GetComponent<EnemyProjectile>().breaksHittingWall = projectileBreaksHittingWall;
        myProjectile.GetComponent<EnemyProjectile>().hurtsPlayers = hurtsPlayers;
        //Set up shooter for the projectile
    }

    void TargetIdleTarget()
    {
        if (target == null)
        {
            target = idleTarget.gameObject;
        }
    }

    public override void OnCollisionStay2D(Collision2D other)
    {
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = other.gameObject;
        }
    }

    public override void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && target != other.gameObject)
        {
            target = other.gameObject;
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = null;
            Invoke("TargetIdleTarget", 1);
            //target = idleTarget.gameObject;
        }
    }
}
