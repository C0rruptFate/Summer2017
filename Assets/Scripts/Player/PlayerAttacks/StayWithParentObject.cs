using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayWithParentObject : MonoBehaviour {

    public Vector3 parentOffset;
	
    void Start()
    {
        parentOffset = transform.position;
    }

	// Update is called once per frame
	void Update () {
        //transform.position = new Vector3(transform.parent.localPosition.x + parentOffset.x, transform.parent.localPosition.y + parentOffset.y, transform.parent.localPosition.z + parentOffset.z);
        transform.position = new Vector3 (transform.parent.position.x + parentOffset.x, transform.parent.position.y + parentOffset.y, transform.parent.position.z + parentOffset.z);
	}
}
