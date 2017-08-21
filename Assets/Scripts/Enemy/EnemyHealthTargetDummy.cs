using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthTargetDummy : EnemyHealth {


    public override void DestroyObject()//Play death animation, wait then enemy dies.
    {
        //[TODO]trigger death animation

        Destroy(enemyHPUISliderObject);
        Destroy(gameObject);
    }
}
