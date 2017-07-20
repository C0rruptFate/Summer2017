using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectiles {

    public override void Start()
    {
        element = shooter.GetComponent<EnemyHealth>().element;
        base.Start();
    }
}
