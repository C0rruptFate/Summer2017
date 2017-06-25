using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSpecialDefend : MonoBehaviour {

    public float shrinkSpeed = 5;
    private bool shrinking;
    private bool growing = true;
    public float speicalMeleeMovementDuration = 2f;
    public Vector3 targetShrinkScale = new Vector3(0.25f, 0.25f, 1f);
    public Vector3 targetFullScale = new Vector3(1f, 8f, 1f);
    public float waitLifeTime;
    public float moveSpeed;

    [HideInInspector]
    public GameObject player;
    private float upSpeed;
    private bool halfSize;
    void Start()
    {
        upSpeed = moveSpeed / 1.5f;

    }

    // Update is called once per frame
    void Update () {
        
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        //transform.Translate(Vector2.up * (moveSpeed/5) * Time.deltaTime);
        if (growing)
        {
            //Debug.Log("Growing");
            //transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            if (transform.localScale.x < targetFullScale.x)
            {
                transform.localScale += Vector3.one * Time.deltaTime * shrinkSpeed;
            }
            
            transform.Translate(Vector2.up * upSpeed * Time.deltaTime);
            if (transform.localScale.x >= targetFullScale.x)
            {
                if (halfSize == false)
                {
                    transform.localScale = new Vector3(targetFullScale.x, targetFullScale.y / 4, targetFullScale.z);
                    halfSize = true;
                }
                
                transform.localScale += Vector3.up * Time.deltaTime * shrinkSpeed;

                if (transform.localScale.y > targetFullScale.y)
                {
                    transform.localScale = targetFullScale;
                    growing = false;
                    Invoke("DestroyBlocker", waitLifeTime);
                }
            }
        }
    }

    void DestroyBlocker()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            Debug.Log("I hit something");
            moveSpeed = 0;
        }
    }
}
