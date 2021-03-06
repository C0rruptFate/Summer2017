﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCCWizard : Enemy {

    public Transform shootPoint;
    public GameObject projectile;
    public bool aimProjectile = false;
    public float projectileSpeed;
    public float projectileMaxDuration;
    public float projectileBreakChance;
    public bool projectileBreaksHittingWall = true;
    private bool hurtsPlayers = true;


    private bool casting;
    private float startSpeed;


    public float closestIWillGet;
    public float furthestIWillGet;
    private float dist;

    void OnDrawGizmos()
    {
        //Wire for start position
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(shootPoint.position, shootPoint.localScale);
    }

    protected override void Start()
    {
        base.Start();
        startSpeed = speed;
    }

    void Update()
    {
        if (aimProjectile)
        {
            shootPoint.LookAt(target.transform.position, Vector3.up);
        }

        if (Time.time > newSwingTimer && target != null && dist <= furthestIWillGet && !casting)
        {
            Shoot();
            
        }
    }

    public override void FixedUpdate()
    {
        //Moves the enemy
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxMoveSpeed, maxMoveSpeed), rb.velocity.y);

        //Finds a new target if my target dies or something happens.
        if (target == null || !target.gameObject.activeSelf)
        {
            if (enemyTargetType != EnemyTargetType.Roam)
            {
                TargetSelection();
            }
            else
            {
                // Roaming Enemy
                if (relentless == false && Time.time > newNextTarget)
                {
                    TargetSelection();
                    if (direction.x == 0)
                    {
                        direction = new Vector2(Random.Range(-1, 2), 0);
                    }
                }
                rb.AddForce(direction * speed, ForceMode2D.Force);
            }
        }
        else
        {//Moves towards my target.
            if (enemyTargetType != EnemyTargetType.Roam)
            {
                if (GetComponent<Rigidbody2D>().gravityScale == 0 && (transform.position.y < target.transform.position.y - 2 || transform.position.y > target.transform.position.y + 2))
                {
                    Vector2 lowerYTransform = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), target.transform.position, (speed * Time.deltaTime));

                    transform.position = new Vector2(transform.position.x, lowerYTransform.y);
                }

                dist = Vector2.Distance(target.transform.position, gameObject.transform.position);
                if (dist >= furthestIWillGet)
                {
                    transform.position = Vector2.MoveTowards(transform.position, target.transform.position, (speed * Time.deltaTime));
                }
                else if (dist < closestIWillGet)
                {
                    if (grounded)
                    {
                        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                    }
                    if (GetComponent<Rigidbody2D>().gravityScale == 0 && transform.position.y != target.transform.position.y)
                    {
                        Vector2 lowerYTransform = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), target.transform.position, (-speed * Time.deltaTime));

                        transform.position = new Vector2(lowerYTransform.x, lowerYTransform.y);
                    }
                    transform.position = Vector2.MoveTowards(transform.position, target.transform.position, (-speed * Time.deltaTime));
                }


            }
            if (relentless == false && Time.time > newNextTarget)
            {
                TargetSelection();
            }
        }
        DirectionFacing();
    }

    void Shoot()
    {
        StartCasting();
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
        myProjectile.GetComponent<EnemyCCProjectile>().target = target;
        //Set up shooter for the projectile
    }

    public override void OnCollisionStay2D(Collision2D other)
    {
    }

    public void StartCasting()
    {
        casting = true;
        speed = 0;
    }

    public void StopCasting()
    {
        casting = false;
        newSwingTimer = Time.time + swingTimer;
        speed = startSpeed;
    }
}
