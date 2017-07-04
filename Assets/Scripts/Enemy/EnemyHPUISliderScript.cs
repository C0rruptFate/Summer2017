using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPUISliderScript : MonoBehaviour {

    [HideInInspector]
    public GameObject myEnemy;

    [HideInInspector]
    public Vector3 offSet;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(myEnemy.transform.position.x + offSet.x, myEnemy.transform.position.y + offSet.y, myEnemy.transform.position.z + offSet.z);

    }
}
