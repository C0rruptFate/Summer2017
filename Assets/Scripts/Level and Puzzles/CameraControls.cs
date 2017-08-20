using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour {

    public float m_DampTime = 0.2f; //How long the camera takes to move to it's target location.
    public float m_ScreenEdgeBuffer = 4f; //Stops the players from reaching the edge of the screen.
    public float m_MinSize = 6.5f; //Min size of the camera used for zooming
    [HideInInspector]
    public List<Transform> players;


    private Camera m_Camera; //The camera that is part of this rig
    private float m_ZoomSpeed; //Deals with the damping the function will use them.
    private Vector3 m_MoveVelocity; //Deals with the damping the function will use them.
    [HideInInspector]
    public Vector3 m_DesiredPosition; //The point we want the camera rig to be at (middle position of all players).
    [HideInInspector]//List of all the players it should be tracking.
    public Transform[] playersArray;


    private void Awake()//On Spawn finds the camera component under this rig.
    {
        m_Camera = GetComponentInChildren<Camera>();
    }

    private void Start()
    {
        playersArray = players.ToArray();//adds all players to its player array so he knows who to track.
        GameObject.Find("Game Manager").GetComponent<GameController>().cameraRig = gameObject;
    }


    private void Update() //Might want to change to update because that is what is used to move our players
    {
        MoveCamera();//Moves the camera to be in the center of all players.
        Zoom();//Zooms to keep all players inside of the camera with a little bit of padding.
    }


    private void MoveCamera()//Moves the camera to track all active (alive) players.
    {
        FindAveragePosition();//Finds the center position of all players.
        //Moves to the center position of all players gradually
        transform.position = Vector3.SmoothDamp(transform.position, m_DesiredPosition, ref m_MoveVelocity, m_DampTime);
    }


    private void FindAveragePosition()//Finds the center position of all players.
    {
        Vector3 averagePos = new Vector3();
        int numTargets = 0;
        //Counts all active (alive) players.
        for (int i = 0; i < playersArray.Length; i++)
        {
            if (!playersArray[i].gameObject.activeSelf)
                continue;

            averagePos += playersArray[i].position;
            numTargets++;
        }

        if (numTargets > 0)
            averagePos /= numTargets;
        //Enable to have camera only move along a certain axis or stop moving all together. Might be nice for a boss fight or climbing portion of a level.
        //Do check if enabling these as the camera could block the player's movement.
        //averagePos.y = transform.position.y;
        //averagePos.x = transform.position.x;

        m_DesiredPosition = averagePos;
    }


    private void Zoom()//Used to keep all players inside of the camera space.
    {
        float requiredSize = FindRequiredSize();//sets how large/small the camera should be.
        m_Camera.orthographicSize = Mathf.SmoothDamp(m_Camera.orthographicSize, requiredSize, ref m_ZoomSpeed, m_DampTime);
    }


    private float FindRequiredSize()//Uses fancy math to find out how big the camera should be, see unity TANKS! tutorial to learn more.
    {
        Vector3 desiredLocalPos = transform.InverseTransformPoint(m_DesiredPosition);

        float size = 0f;

        for (int i = 0; i < playersArray.Length; i++)
        {
            if (!playersArray[i].gameObject.activeSelf)
                continue;

            Vector3 targetLocalPos = transform.InverseTransformPoint(playersArray[i].position);

            Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;

            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));

            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / m_Camera.aspect);
        }

        size += m_ScreenEdgeBuffer;

        size = Mathf.Max(size, m_MinSize);

        return size;
    }


    public void SetStartPositionAndSize()//Sets the position and size based on the functions above.
    {
        FindAveragePosition();

        transform.position = m_DesiredPosition;

        m_Camera.orthographicSize = FindRequiredSize();
    }
}
