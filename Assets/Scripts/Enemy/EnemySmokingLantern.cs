using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmokingLantern : Enemy {

    public float smokeMaxRange;
    public float smokeTriggerDecrease;

    [HideInInspector]
    public GameObject smokingLanternSmoke;

    private GameObject enemyWeaponParent;

    // Use this for initialization
    protected override void Start()
    {
        //Sets up the components 
        //animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        element = gameObject.GetComponent<EnemyHealth>().element;
        smokingLanternSmoke = transform.Find("Smoking Lantern Smoke").gameObject;

        enemyWeaponParent = GameObject.Find("Enemy Attacks");
        if (!enemyWeaponParent)//If it can't find the weapon parent it will create one (the first player on each level should create this automatically).
        {
            enemyWeaponParent = new GameObject("Enemy Attacks");
        }

        smokingLanternSmoke.transform.parent = enemyWeaponParent.transform;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
