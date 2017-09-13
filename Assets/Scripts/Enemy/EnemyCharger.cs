using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharger : Enemy {

    private GameObject[] players;
    public float yTargetingOffset = 5;
    public float chargingSpeed = 30;
    public float aggroRange;
    public float chargeRampUp = 1;
    public float timeBetweenCharge = 2;
    public float chargingKnockback = 20;
    public Vector2 excited = new Vector2(0f, 1f);
    public Vector2 jumpBack = new Vector2(1f,1f);
    
    private float defaultSpeed;
    private float currentTimeBetweenCharge;
    private bool isExcited = false;

    protected override void Start()
    {
        base.Start();
        players = GameObject.FindGameObjectsWithTag("Player");
        defaultSpeed = speed;
    }

    void Update()
    {
        float closestTargetDist = 0.0f;
        foreach (var possibleTarget in players)//Finds the closest target by looping though all players and seeing who is closest.
        {
            if (possibleTarget.gameObject.activeSelf && (possibleTarget.transform.position.y <= yTargetingOffset + transform.position.y && possibleTarget.transform.position.y >= transform.position.y - yTargetingOffset))
            {
                float dist = Vector2.Distance(possibleTarget.transform.position, gameObject.transform.position);
                if (dist <= aggroRange && Time.time > currentTimeBetweenCharge && !isExcited)
                {
                    currentTimeBetweenCharge = Time.time + timeBetweenCharge;
                    closestTargetDist = dist;
                    target = possibleTarget;
                    enemyTargetType = EnemyTargetType.Proximity;
                    speed = 0;
                    StartCoroutine("WindUp");
                    //[TODO] Charger ramp up animation
                    Invoke("Charge", chargeRampUp);
                    
                }
                else if (closestTargetDist == 0.0f)
                {
                    closestTargetDist = dist;
                }
            }
        }

        if (target != null && (target.transform.position.y > yTargetingOffset + transform.position.y || target.transform.position.y < transform.position.y - yTargetingOffset))
        {
            StopCharging();
        }
    }

    public void StopCharging()
    {
        enemyTargetType = EnemyTargetType.Roam;
        speed = defaultSpeed;
        knockback = 1f;
        target = null;
    }

    IEnumerator WindUp()
    {
        isExcited = true;
        //rb.AddForce(excited, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        if (target != null && target.transform.position.x < transform.position.x)
        {
            rb.AddForce(new Vector2(jumpBack.x, jumpBack.y), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(new Vector2(-jumpBack.x, jumpBack.y), ForceMode2D.Impulse);
        }
        isExcited = false;
        StopCoroutine("WindUp");

    }

    void Charge()
    {
        speed = chargingSpeed;
        knockback = chargingKnockback;
    }

    public override void OnCollisionStay2D(Collision2D other)
    {
        //Deals damage to the player as long as they are touching this enemy.
        if (other.transform.tag == ("Player") && Time.time > newSwingTimer) //Lets the charger hit players
        {
            //Debug.Log("Player should take damage");
            newSwingTimer = Time.time + swingTimer;
            PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
            PlayerHealth health = other.gameObject.GetComponent<PlayerHealth>();
            //PlayerAttacks playerAttacks = health.playerAttacks;
            Rigidbody2D otherRB = other.gameObject.GetComponent<Rigidbody2D>();

            //If what I am colliding with has both a player Controller and Health script, deal damage to them and knock them back.
            if (playerMovement && health)
            {
                float distX = (other.transform.position.x - transform.position.x) * knockback;
                otherRB.velocity = new Vector2(0.0f, 0.0f);
                otherRB.AddForce(new Vector2(otherRB.velocity.x + distX, otherRB.velocity.y), ForceMode2D.Impulse);
                health.TakeDamage(gameObject, damage, hitStun);
                //[TODO]
                if (hitEffect != null)
                {
                    Instantiate(hitEffect, other.transform.position, hitEffect.transform.rotation, enemyWeaponParent.transform);
                }
                else
                {
                    Debug.Log(gameObject.name + " is missing it's hit effect.");
                }

            }
        }
        else if (other.transform.tag == ("Enemy") && Time.time > newSwingTimer)//Lets the charger hit enemies
        {
            //Debug.Log("Player should take damage");
            newSwingTimer = Time.time + swingTimer;
            Enemy enemyScript = other.gameObject.GetComponent<Enemy>();
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            //PlayerAttacks playerAttacks = health.playerAttacks;
            Rigidbody2D otherRB = other.gameObject.GetComponent<Rigidbody2D>();

            //If what I am colliding with has both a player Controller and Health script, deal damage to them and knock them back.
            if (enemyScript && enemyHealth)
            {
                float distX = (other.transform.position.x - transform.position.x) * knockback;
                otherRB.velocity = new Vector2(0.0f, 0.0f);
                otherRB.AddForce(new Vector2(otherRB.velocity.x + distX, otherRB.velocity.y), ForceMode2D.Impulse);
                enemyHealth.TakeDamage(gameObject, damage, hitStun);
                //[TODO]
                if (hitEffect != null)
                {
                    Instantiate(hitEffect, other.transform.position, hitEffect.transform.rotation, enemyWeaponParent.transform);
                }
                else
                {
                    Debug.Log(gameObject.name + " is missing it's hit effect.");
                }

            }
        }
    }
}
