using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingHazard : MonoBehaviour {

    public GameObject myjumpingHazard;

    public float fireRate;

    public Vector2 shootForce;

    public float destoryHazardsTime;

    private float nextFire;
	
	// Update is called once per frame
	void Update () {
		
        if(Time.time >= nextFire)
        {
            SpitJumpingHazard();
        }
	}

    void SpitJumpingHazard()
    {
        nextFire = Time.time + fireRate;
        GameObject newJumpingHazard = Instantiate(myjumpingHazard, transform.position, transform.rotation);
        newJumpingHazard.AddComponent<DestroySelfRightAway>();
        newJumpingHazard.AddComponent<Rigidbody2D>();
        newJumpingHazard.GetComponent<DestroySelfRightAway>().waitToDestroyTime = destoryHazardsTime;
        newJumpingHazard.GetComponent<Rigidbody2D>().AddForce(shootForce, ForceMode2D.Impulse);
    }
}
