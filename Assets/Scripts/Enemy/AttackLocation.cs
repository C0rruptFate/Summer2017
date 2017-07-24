using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLocation : MonoBehaviour {

    //[HideInInspector]
    public GameObject target;
    [HideInInspector]
    public float targetingSpeed;
    [HideInInspector]
    public float stopMovingTime;
    [HideInInspector]
    public GameObject shooter;
    [HideInInspector]
    public float firePostMoveDelay;

    private bool fired = false;
    private float nextStopMovingTime;

	// Use this for initialization
	void Start () {
        nextStopMovingTime = Time.time + stopMovingTime;
	}
	
	// Update is called once per frame
	void Update () {

        if(nextStopMovingTime < Time.time)
        {
            Vector2.MoveTowards(transform.position, new Vector2 (target.transform.position.x, target.transform.position.y - 1), targetingSpeed);
        }
        else if(!fired)
        {
            StartCoroutine(Cast());
        }
	}

    private IEnumerator Cast()
    {
        yield return new WaitForSeconds(firePostMoveDelay);
        GameObject myAttack = Instantiate(shooter.GetComponent<EnemyTargetWizard>().wizardAttack, transform.position, shooter.GetComponent<EnemyTargetWizard>().wizardAttack.transform.rotation);
        AttackStats(myAttack);
        shooter.GetComponent<EnemyTargetWizard>().targeting = false;
        shooter.GetComponent<EnemyTargetWizard>().speed = shooter.GetComponent<EnemyTargetWizard>().startingSpeed;
        Destroy(gameObject);
    }

    void AttackStats(GameObject attack)
    {
        attack.transform.parent = shooter.GetComponent<EnemyTargetWizard>().enemyWeaponParent.transform;
        attack.GetComponent<EnemyProjectile>().element = shooter.GetComponent<EnemyTargetWizard>().element;
        attack.GetComponent<EnemyProjectile>().shooter = shooter.GetComponent<EnemyTargetWizard>().gameObject;
        attack.GetComponent<EnemyProjectile>().projectileDamage = shooter.GetComponent<EnemyTargetWizard>().damage;
        attack.GetComponent<EnemyProjectile>().projectileHitStun = shooter.GetComponent<EnemyTargetWizard>().hitStun;
        attack.GetComponent<EnemyProjectile>().projectileMaxDuration = shooter.GetComponent<EnemyTargetWizard>().projectileMaxDuration;
        attack.GetComponent<EnemyProjectile>().hurtsPlayers = true;
        if (attack.GetComponent<Bomb>() != null)
        {
            attack.GetComponent<Bomb>().explosion_radius = shooter.GetComponent<EnemyTargetWizard>().explosion_radius;
            attack.GetComponent<Bomb>().explosion_force = shooter.GetComponent<EnemyTargetWizard>().explosion_force;
        }
    }
}
