using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharger : Enemy {

    private GameObject[] players;
    public float yTargetingOffset = 5;
    public float chargingSpeed = 30;
    public float aggroRange;
    public float chargeRampUp = 1;
    public float timeBetweenCharge = 2;

    private float defaultSpeed;
    private float currentTimeBetweenCharge;

    protected override void Start()
    {
        base.Start();
        players = GameObject.FindGameObjectsWithTag("Player");
        defaultSpeed = speed;
    }

    void Update()
    {
        float closestTargetDist = 0.0f;
        foreach (var possibleTarget in players)//Finds the closest target by looping though all players and seeing who is closest.
        {
            if (possibleTarget.gameObject.activeSelf && (possibleTarget.transform.position.y <= yTargetingOffset + transform.position.y && possibleTarget.transform.position.y >= transform.position.y - yTargetingOffset))
            {
                float dist = Vector2.Distance(possibleTarget.transform.position, gameObject.transform.position);
                if (dist <= aggroRange && Time.time > currentTimeBetweenCharge)
                {
                    currentTimeBetweenCharge = Time.time + timeBetweenCharge;
                    closestTargetDist = dist;
                    target = possibleTarget;
                    enemyTargetType = EnemyTargetType.Proximity;
                    speed = 0;
                    //[TODO] Charger ramp up animation
                    Invoke("Charge", chargeRampUp);
                    
                }
                else if (closestTargetDist == 0.0f)
                {
                    closestTargetDist = dist;
                }
            }
        }

        if (target !=null && (target.transform.position.y > yTargetingOffset + transform.position.y || target.transform.position.y < transform.position.y - yTargetingOffset))
        {
            enemyTargetType = EnemyTargetType.Roam;
            speed = defaultSpeed;
            target = null;
            
        }
    }

    void Charge()
    {
        speed = chargingSpeed;
    }
}
