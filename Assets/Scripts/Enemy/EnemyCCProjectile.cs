using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCCProjectile : EnemyProjectile {

    public GameObject target;

	// Update is called once per frame
	void Update () {
		
	}

    public override void FixedUpdate()
    {
        Vector2.MoveTowards(gameObject.transform.position, target.transform.position, projectileSpeed);
    }
}
