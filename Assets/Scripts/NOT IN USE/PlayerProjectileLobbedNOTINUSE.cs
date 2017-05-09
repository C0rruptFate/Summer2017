using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileLobbed : MonoBehaviour
{

    [Tooltip("How fast the projectile moves.")]
    public float speed = 10;
    [Tooltip("How much damge this object deals.")]
    public float damage = 10;
    [Tooltip("How much damge this object deals. 'The size of what you are hitting will be important.'")]
    public float stunLockOut = 0.25f;
    [Tooltip("The Max amount of time this object can stay alive")]
    public float maxLife = 10;
    [Tooltip("Chance that the projectile will break when hitting something. (Higher # is more likely to break) (Only active if breakable is checked on).")]
    public float breakChance = 0;

    private float currentLife;
    private bool breaking = false;


    // Use this for initialization
    void Start()
    {
        currentLife = Time.time + maxLife;
        float breakNumber = Random.Range(0, 100);
        if (breakNumber <= breakChance)
        {
            breaking = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time >= currentLife)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == ("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            EnemyHealth health = other.gameObject.GetComponent<EnemyHealth>();
            //Rigidbody otherRB = other.gameObject.GetComponent<Rigidbody>();

            if (enemy && health)
            {
                //float distX = (other.transform.position.x - transform.position.x) * knockback;
                //float distY = (other.transform.position.y - transform.position.y) * knockback;
                //otherRB.velocity = new Vector3(0.0f, 0.0f, otherRB.velocity.z);
                //otherRB.AddForce(new Vector3(distX, distY, 0), ForceMode.Impulse);
                health.TakeDamage(gameObject, damage, stunLockOut);
                Destroy(gameObject);
            }
        }
        else if (other.tag != ("Player") && breaking)
        {
            Destroy(gameObject);
        }


    }
}