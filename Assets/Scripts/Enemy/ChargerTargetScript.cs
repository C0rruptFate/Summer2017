using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerTargetScript : MonoBehaviour {

    public GameObject myCharger;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == myCharger)
        {
            other.GetComponent<EnemyCharger>().StopCharging();
        }
    }
}
