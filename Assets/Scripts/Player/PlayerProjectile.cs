using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour {

    //These are all set by the players attacks/actions script.
    [HideInInspector]//How fast the projectile moves.
    public float projectileSpeed;
    [HideInInspector]//How much damage the projectile deals.
    public float projectileDamage;
    [HideInInspector]//How long the projectile stuns for.
    public float projectileHitStun;

    [HideInInspector]//the projectile will expire after X seconds.
    public float projectileMaxDuration;
    [HideInInspector]//The chance it will break when hitting an enemy.
    public float projectileBreakChance;
    [HideInInspector]//Does this fly strait or is is lobbed? True to fly strait.
    public bool usesConstantForceProjectile = true;
    [HideInInspector]//Does this expire when hitting a wall?
    public bool breaksHittingWall = true;
    [HideInInspector]//The force this is lobbed at when the player is using a lobbed projectile.
    public Vector2 lobbedForce;
    [HideInInspector]//How long this is held for before being thrown.
    public float throwWaitTime;
    private Transform formerParent;//Used when holding a lobbed projectile.

    private float currentLife;//How long this has been alive for.
    private bool breaking = false;//used with the break change to decide if this will break.

    [HideInInspector]public GameObject player; //Who this belongs to.
    [HideInInspector]//What element is this projectile.
    public Element myElement;

    private Rigidbody2D rb; //My Rigidibody

    // Use this for initialization
    void Start () {

        //Set's my element
        myElement = player.GetComponent<PlayerHealth>().element;

        //enables my collider as they start disabled.
        if (gameObject.GetComponent<Collider2D>().enabled == false)
        {
            gameObject.GetComponent<Collider2D>().enabled = true;
        }

        //Used to set up lobbed projectiles.
        if(!usesConstantForceProjectile && GetComponent<Rigidbody2D>() == null)
        {
            formerParent = transform.parent;
            transform.parent = player.transform;
            Invoke("ThrowForce", throwWaitTime);
        }

        currentLife = Time.time + projectileMaxDuration;//sets the max life of this object.
        float breakNumber = Random.Range(0, 100);//Used to help decide if this will break when hitting an enemy.
        if (breakNumber <= projectileBreakChance)
        {
            breaking = true;
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {

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


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ("Enemy"))//If this hits an enemy deals damage to them.
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
                health.TakeDamage(gameObject, projectileDamage, projectileHitStun);
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

    private void ThrowForce()
    {
        //Throws the lobbed projectile.
        transform.parent = formerParent;
        gameObject.AddComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
        BoxCollider2D physicalCollider = gameObject.AddComponent<BoxCollider2D>();
        physicalCollider.size = new Vector2(3f,3f);

        //gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        if (player.GetComponent<Transform>().rotation.y < 0)
        {
            rb.AddForce(new Vector2(-lobbedForce.x, lobbedForce.y), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(lobbedForce, ForceMode2D.Impulse);
        }
    }

}
