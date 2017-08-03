using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthCCWizard : EnemyHealth {

    [HideInInspector]
    public GameObject ccTriggerPoint;

    public override void DestroyObject()
    {
        ccTriggerPoint.GetComponent<CCTriggerPoint>().BreakCasting();
        Destroy(gameObject);
    }
}
