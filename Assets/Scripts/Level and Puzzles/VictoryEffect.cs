using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryEffect : MonoBehaviour {

    [SerializeField]
    float speed = 75;
    private bool goRight;

    void FixedUpdate()
    {
        Vector3 screen_bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
        if (transform.position.x >= screen_bounds.x + 4)
        {
            goRight = false;
        }
        else if (transform.position.x <= -screen_bounds.x - 4)
        {
            goRight = true;
        }
        //Vector2 screenPos = transform.position;
        //screenPos.y = screen_bounds.y / 2;
        //transform.position = screenPos;

        if (goRight)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
        else
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }
    }
}
