using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmokingLantern : Enemy {

    // Use this for initialization
    protected override void Start()
    {
        //Sets up the components 
        //animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        element = gameObject.GetComponent<EnemyHealth>().element;

        if (useBoundries)
        {
            startPosition = transform.position;
        }

        TargetSelection();//Finds the target.
    }

    // Update is called once per frame
    void Update () {
		
	}
}
