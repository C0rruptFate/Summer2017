using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileFireBasic : PlayerProjectile
{
    private bool alreadyGeneratedComboPoint = false;
    // Use this for initialization
    public override void Start()
    {

        //Set's my element
        element = player.GetComponent<PlayerHealth>().element;

        //enables my collider as they start disabled.
        if (gameObject.GetComponent<Collider2D>().enabled == false)
        {
            gameObject.GetComponent<Collider2D>().enabled = true;
        }

        currentLife = Time.time + projectileMaxDuration;//sets the max life of this object.
        float breakNumber = Random.Range(0, 100);//Used to help decide if this will break when hitting an enemy.
        if (breakNumber <= projectileBreakChance)
        {
            breaking = true;
        }
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {

        //Reduces the life of the object at 0 it is destroyed.
        if (Time.time >= currentLife)
        {
            Destroy(gameObject);
        }

        //Causes the object to fly forward.
        if (usesConstantForceProjectile)
        {
            transform.Translate(Vector2.right * projectileSpeed * Time.deltaTime);
        }
        else
        {//Reflects the projectile.
            transform.position = Vector3.MoveTowards(transform.position, reflectedPoint.position, -projectileSpeed * Time.deltaTime);
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (hurtsPlayers == false)
        {
            if (other.tag == ("Enemy"))//If this hits an enemy deals damage to them.
            {
                Enemy enemy = other.gameObject.GetComponent<Enemy>();
                EnemyHealth health = other.gameObject.GetComponent<EnemyHealth>();
                //Rigidbody otherRB = other.gameObject.GetComponent<Rigidbody>();

                if (enemy && health)
                {
                    health.TakeDamage(gameObject, projectileDamage, projectileHitStun);

                    //Generates one combo point per attack when hitting an enemy. This can't go above max points.
                    if (alreadyGeneratedComboPoint == false)
                    {
                        alreadyGeneratedComboPoint = true;
                        player.GetComponent<AttacksFire>().currentComboPoints++;
                        player.GetComponent<AttacksFire>().currentTimeDelaySet = Time.time;
                        player.GetComponent<AttacksFire>().comboPointAlreadyCounting = false;
                        //player.GetComponent<AttacksFire>().comboPointCountDown = !player.GetComponent<AttacksFire>().comboPointCountDown;
                        if (player.GetComponent<AttacksFire>().currentComboPoints >= player.GetComponent<AttacksFire>().maxComboPoints)
                        {
                            player.GetComponent<AttacksFire>().currentComboPoints = player.GetComponent<AttacksFire>().maxComboPoints;
                        }
                        //Debug.Log("Generates a combo point" + player.GetComponent<AttacksFire>().currentComboPoints);
                        player.GetComponent<PlayerHealth>().playerUI.GetComponent<PlayerUI>().UpdateComboPointUI();
                    }

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
        else if (hurtsPlayers == true)
        {
            if (other.transform.tag == ("Player"))
            {
                //Debug.Log("Player should take damage");
                PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
                PlayerHealth health = other.gameObject.GetComponent<PlayerHealth>();
                //PlayerAttacks playerAttacks = health.playerAttacks;
                //Rigidbody otherRB = other.gameObject.GetComponent<Rigidbody>();

                //If what I am colliding with has both a player Controller and Health script, deal damage to them and knock them back.
                if (playerMovement && health)
                {
                    //float distX = (other.transform.position.x - transform.position.x) * knockback;
                    //otherRB.velocity = new Vector3(0.0f, 0.0f, otherRB.velocity.z);
                    //otherRB.AddForce(new Vector3(distX, otherRB.velocity.y, 0), ForceMode.Impulse);
                    health.TakeDamage(gameObject, projectileDamage, projectileHitStun);
                }
            }
            else if (other.tag == ("Ground") && breaksHittingWall) //Gets destroyed when hitting the ground/walls
            {
                Destroy(gameObject);
            }
        }
    }
}

