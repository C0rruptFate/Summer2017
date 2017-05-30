using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour
{

    private Transform myTransform;

    public CameraEdges cameraEdges;

    // Use this for initialization
    void Start()
    {
        myTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (cameraEdges)
        {
            case CameraEdges.Bottom:
                //groundGun = transform.Find("Overhead Gun");
                break;

        }
    }

}
