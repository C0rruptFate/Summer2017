using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharger : Enemy {

    private GameObject[] players;
    public float yTargetingOffset = 5;
    public float chargingSpeed = 30;
    public float aggroRange;

    private float defaultSpeed;

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
                if (dist <= aggroRange)
                {
                    closestTargetDist = dist;
                    target = possibleTarget;
                    Charge();
                    
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
        enemyTargetType = EnemyTargetType.Proximity;
        speed = chargingSpeed;
    }
}
