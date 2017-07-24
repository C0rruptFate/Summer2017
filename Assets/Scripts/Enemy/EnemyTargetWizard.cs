using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetWizard : Enemy {
    [Tooltip("Where my attacks are targeting.")]
    public GameObject attackLocation;
    [Tooltip("My Attack.")]
    public GameObject wizardAttack;
    [Tooltip("How fast my targeting will move.")]
    public float targetingSpeed;
    [Tooltip("How long after targeting will my targeting stop moving.")]
    public float stopMovingTime;
    [Tooltip("How long after my target stops moving does it take for my attack to spawn?")]
    public float firePostMoveDelay;

    [Tooltip("How long my attack lasts for.")]
    public float projectileMaxDuration = 1;
    [Tooltip("My Attacks radius")]
    public float explosion_radius;
    [Tooltip("The force that my attack pushes with.")]
    public float explosion_force;

    [Tooltip("What is the closest I will get?")]
    public float closestIWillGet;
    [Tooltip("What is the furtherest I will get?")]
    public float furthestIWillGet;

    private float dist;
    [HideInInspector]
    public bool targeting;
    private float nextFireDelay;
    [HideInInspector]
    public GameObject enemyWeaponParent;

    protected override void Start()
    {
        base.Start();
        enemyWeaponParent = GameObject.Find("Enemy Attacks");
        if (!enemyWeaponParent)//If it can't find the weapon parent it will create one (the first player on each level should create this automatically).
        {
            enemyWeaponParent = new GameObject("Enemy Attacks");
        }
    }

    void Update()
    {
        dist = Vector2.Distance(target.transform.position, gameObject.transform.position);
        if (Time.time > newSwingTimer && target != null && dist >= closestIWillGet && dist < furthestIWillGet)
        {
            newSwingTimer = Time.time + swingTimer;
            Target();
            
        }
    }

    public override void FixedUpdate()
    {

        //Moves the enemy
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxMoveSpeed, maxMoveSpeed), rb.velocity.y);

        //Finds a new target if my target dies or something happens.

        if (!targeting)
        {
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
                        if (useBoundries)
                        {
                            if (transform.position.x > startPosition.x + boundryRange)
                            {
                                direction.x = -1;
                            }
                            else if (transform.position.x < startPosition.x - boundryRange)
                            {
                                direction.x = 1;
                            }
                        }
                    }
                    rb.AddForce(direction * speed, ForceMode2D.Force);

                }
            }
            else
            {//Moves towards my target.
                if (enemyTargetType != EnemyTargetType.Roam)
                {
                    if (dist >= furthestIWillGet)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, (speed * Time.deltaTime));
                    }
                    else if (dist < closestIWillGet)
                    {
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
    }


    void Target()
    {
        targeting = true;
        speed = 0;
        GameObject myAttackLocation = Instantiate(attackLocation, target.transform.position, attackLocation.transform.rotation);
        myAttackLocation.GetComponent<AttackLocation>().target = target;
        myAttackLocation.GetComponent<AttackLocation>().targetingSpeed = targetingSpeed;
        myAttackLocation.GetComponent<AttackLocation>().stopMovingTime = stopMovingTime;
        myAttackLocation.GetComponent<AttackLocation>().shooter = gameObject;
        myAttackLocation.GetComponent<AttackLocation>().firePostMoveDelay = firePostMoveDelay;
        myAttackLocation.transform.parent = gameObject.transform;
    }
}
