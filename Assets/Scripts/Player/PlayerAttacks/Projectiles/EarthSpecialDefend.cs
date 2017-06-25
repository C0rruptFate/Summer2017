using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSpecialDefend : PlayerMelee {
    [HideInInspector]
    public float moveSpeed;

    private Rigidbody2D rb;
    [HideInInspector]
    public float destroyWait = 3;

    public Transform[] spawnPoints;
    [HideInInspector]
    public int maxHits;

    private int currentHits = 0;

    private bool facingRight;
    

    public override void Start()
    {
        Debug.Log("I exist");
        rb = GetComponent<Rigidbody2D>();
        myElement = player.GetComponent<PlayerHealth>().element;
        facingRight = player.GetComponent<PlayerMovement>().facingRight;
        Invoke("DestroySelf", destroyWait);
    }

    public override void Update()
    {
    }

    // Update is called once per frame
    void FixedUpdate () {

        if (facingRight)
        {
            rb.AddForce(Vector2.right * moveSpeed, ForceMode2D.Force);
        }
        else
        {
            rb.AddForce(Vector2.left * moveSpeed, ForceMode2D.Force);
        }
        //rb.AddForce(Vector2.right * moveSpeed, ForceMode2D.Force);
        player.transform.position = transform.position;
        player.GetComponent<Rigidbody2D>().gravityScale = 0;
        
    }

    void DestroySelf()
    {
        Debug.Log("I died");
        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject shard = Instantiate(player.GetComponent<PlayerAttacks>().groundProjectile, spawnPoint.position, spawnPoint.rotation);
            player.GetComponent<AttacksEarth>().SetBasicRangedAttackStats(shard);
            shard.GetComponent<Projectiles>().player = player;
            shard.GetComponent<PlayerProjectileEarthBasic>().throwWaitTime = 0;
        }
        player.GetComponent<Rigidbody2D>().gravityScale = 1;

        Destroy(gameObject);
    }

    public override void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == ("Enemy"))//If this hits an enemy deals damage to them.
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            EnemyHealth health = other.gameObject.GetComponent<EnemyHealth>();
            //Rigidbody otherRB = other.gameObject.GetComponent<Rigidbody>();

            if (enemy && health)
            {
                //Debug.Log("Hit enemy");
                health.TakeDamage(gameObject, player.GetComponent<AttacksEarth>().projectileDamage, player.GetComponent<AttacksEarth>().projectileHitStun);
                currentHits++;
                if (currentHits >= maxHits)
                {
                    DestroySelf();
                }
            }
        }
    }
}
