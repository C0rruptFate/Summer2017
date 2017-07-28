using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmokingLantern : Enemy {

    public float smokeRadiusMax;
    public float smokeRadiusMin;
    public float smokeIncreaseRate;
    public float aggroRange;
    public bool shrinkSmoke = false;
    public float shrinkDuration;

    [HideInInspector]
    public GameObject smokingLanternSmoke;

    private GameObject enemyWeaponParent;
    private float currentShrinkDuration;

    // Use this for initialization
    protected override void Start()
    {
        //Sets up the components 
        //animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        element = Element.None;
        smokingLanternSmoke = transform.Find("Smoking Lantern Smoke").gameObject;

        enemyWeaponParent = GameObject.Find("Enemy Attacks");
        if (!enemyWeaponParent)//If it can't find the weapon parent it will create one (the first player on each level should create this automatically).
        {
            enemyWeaponParent = new GameObject("Enemy Attacks");
        }

        DamageZoneStats(smokingLanternSmoke.gameObject);
    }

    // Update is called once per frame
    void Update () {
        targets = GameObject.FindGameObjectsWithTag("Player");//Finds all players tagged as "player".
        //float closestTargetDist = 0.0f;
        foreach (var possibleTarget in targets)//Finds the closest target by looping though all players and seeing who is closest.
        {
            if (possibleTarget.gameObject.activeSelf)
            {
                float dist = Vector2.Distance(possibleTarget.transform.position, gameObject.transform.position);
                if (dist <= aggroRange && !shrinkSmoke)
                {
                    smokingLanternSmoke.transform.localScale = new Vector3(smokingLanternSmoke.transform.localScale.x + smokeIncreaseRate * Time.deltaTime, smokingLanternSmoke.transform.localScale.y + smokeIncreaseRate * Time.deltaTime, smokingLanternSmoke.transform.localScale.z + smokeIncreaseRate * Time.deltaTime);
                    if (smokingLanternSmoke.transform.localScale.x >= smokeRadiusMax)
                    {
                        smokingLanternSmoke.transform.localScale = new Vector3 (smokeRadiusMax, smokeRadiusMax, smokeRadiusMax);
                    }
                }
            }
        }

        if (shrinkSmoke)
        {
            if (Time.time >= currentShrinkDuration)
            {
                shrinkSmoke = false;
            }
            smokingLanternSmoke.transform.localScale = new Vector3(smokingLanternSmoke.transform.localScale.x - smokeIncreaseRate * Time.deltaTime, smokingLanternSmoke.transform.localScale.y - smokeIncreaseRate * Time.deltaTime, smokingLanternSmoke.transform.localScale.z - smokeIncreaseRate * Time.deltaTime);
            if (smokingLanternSmoke.transform.localScale.x <= smokeRadiusMin)
            {
                smokingLanternSmoke.transform.localScale = new Vector3(smokeRadiusMin, smokeRadiusMin, smokeRadiusMin);
            }
        }
	}

    public override void FixedUpdate()
    {
    }

    public override void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Wisp"))
        {
            shrinkSmoke = true;
            currentShrinkDuration = Time.time + shrinkDuration;
        }
    }

    void DamageZoneStats(GameObject other)
    {
        other.transform.parent = enemyWeaponParent.transform;
        other.transform.position = transform.position;
        other.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        other.GetComponent<SmokingLanternDamage>().damage = damage;
        other.GetComponent<SmokingLanternDamage>().hitStun = hitStun;
        other.GetComponent<SmokingLanternDamage>().element = element;
        other.GetComponent<SmokingLanternDamage>().swingTimer = swingTimer;
    }
}
