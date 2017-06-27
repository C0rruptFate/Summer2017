using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour {

    public Element element;
    public float damage;
    [Tooltip("How much time must take place between swings.")]
    public float swingTimer = 0.5f;

    //[HideInInspector]//How long it has been sense the enemy last attacked, use so that the enemy can't attack every frame.
    private float newSwingTimer = 0f;
    private float hitStun = 0;

    public bool lowerHazard;
    public bool raiseHazard;
    private Vector3 startPosition;
    private Vector3 endPosition;
    [SerializeField]
    private float moveSpeed;
    // Use this for initialization
    void Start () {
        startPosition = transform.position;
        endPosition = new Vector3(startPosition.x, startPosition.y - 0.8f, startPosition.z);

    }
	
	// Update is called once per frame
	void Update () {
		
        if(raiseHazard)
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            if (transform.position.y >= startPosition.y)
            {
                transform.position = startPosition;
                raiseHazard = false;
                lowerHazard = false;
            }
        }
        else if (lowerHazard)
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
            if (transform.position.y <= endPosition.y)
            {
                transform.position = endPosition;
                lowerHazard = false;
            }
        }

	}

    public virtual void OnCollisionStay2D(Collision2D other)
    {
        //Deals damage to the player as long as they are touching this enemy.
        if (other.transform.tag == ("Player") && Time.time > newSwingTimer)
        {
            //Debug.Log("Player should take damage");
            newSwingTimer = Time.time + swingTimer;
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
                Debug.Log("did damage " + damage);
            }
        }
    }
}
