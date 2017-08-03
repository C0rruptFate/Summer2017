using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTriggerPoint : MonoBehaviour {
    [HideInInspector]
    public GameObject shooter;
    [HideInInspector]
    public GameObject attachedTo;
	
    void Start ()
    {
        shooter.GetComponent<EnemyHealthCCWizard>().ccTriggerPoint = gameObject;
        attachedTo.GetComponent<PlayerAttacks>().crowdControl = true;
        attachedTo.GetComponent<PlayerMovement>().crowdControl = true;
        attachedTo.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(attachedTo.transform.position.x, attachedTo.transform.position.y, attachedTo.transform.position.z);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wisp"))
        {
            BreakCasting();
        }
    }

    public void BreakCasting()
    {
        attachedTo.GetComponent<PlayerAttacks>().crowdControl = false;
        attachedTo.GetComponent<PlayerMovement>().crowdControl = false;
        attachedTo.GetComponent<Rigidbody2D>().gravityScale = 1;
        attachedTo.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        shooter.GetComponent<EnemyCCWizard>().StopCasting();
        Destroy(gameObject);
    }

}
