using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingHazard : Hazard
{
    public Transform targetPosition;
    public float activateHazardWait;
    private bool hazardIsActive;

    void OnDrawGizmos()
    {
        //Wire for start position
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(targetPosition.position, transform.localScale);
    }

    public override void Start()
    {
        base.Start();
        //targetPosition.parent = null;
        Invoke("ActivateHazard", activateHazardWait);
    }

    public override void FixedUpdate()
    {
        if (hazardIsActive)
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, moveSpeed * Time.deltaTime);
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        //Deals damage to the player as long as they are touching this enemy.
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
                health.TakeDamage(gameObject, damage, hitStun);
            }
        }
        else if (other.transform.tag == ("Enemy"))//Lets it hurt enemies
        {
            //Debug.Log("Player should take damage");
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            EnemyHealth health = other.gameObject.GetComponent<EnemyHealth>();
            //PlayerAttacks playerAttacks = health.playerAttacks;
            //Rigidbody otherRB = other.gameObject.GetComponent<Rigidbody>();

            //If what I am colliding with has both a player Controller and Health script, deal damage to them and knock them back.
            if (enemy && health)
            {
                //float distX = (other.transform.position.x - transform.position.x) * knockback;
                //otherRB.velocity = new Vector3(0.0f, 0.0f, otherRB.velocity.z);
                //otherRB.AddForce(new Vector3(distX, otherRB.velocity.y, 0), ForceMode.Impulse);
                health.TakeDamage(gameObject, damage, hitStun);
            }
        }
    }

    public void ActivateHazard()
    {
        Debug.Log("Hazard is active");
        hazardIsActive = true;
    }
}

