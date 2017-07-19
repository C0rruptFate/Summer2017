using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLaserSight : MonoBehaviour {

    [HideInInspector]
    public GameObject myTurret;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void TargetIdleTarget()
    {
        if (myTurret.GetComponent<EnemyTurret>().target == null)
        {
            myTurret.GetComponent<EnemyTurret>().target = myTurret.GetComponent<EnemyTurret>().idleTarget.gameObject;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            myTurret.GetComponent<EnemyTurret>().target = other.gameObject;
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && myTurret.GetComponent<EnemyTurret>().target != other.gameObject)
        {
            myTurret.GetComponent<EnemyTurret>().target = other.gameObject;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            myTurret.GetComponent<EnemyTurret>().target = null;
            Invoke("TargetIdleTarget", 1);
            //target = idleTarget.gameObject;
        }
    }
}
