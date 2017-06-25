using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSDefendTestScript : MonoBehaviour {
    public float shrinkSpeed = 5;
    private bool shrinking;
    private bool growing = true;
    public float speicalMeleeMovementDuration = 2f;
    public Vector3 targetShrinkScale = new Vector3(0.25f, 0.25f, 0.25f);
    public Vector3 targetFullScale = new Vector3(1f, 1f, 1f);
    public float waitLifeTime;
    public float moveSpeed;

    [HideInInspector]
    public GameObject player;
    [SerializeField]
    private bool moving = false;

    void Awake()
    {
        transform.localScale = targetShrinkScale;

    }

    void Start()
    {
        //if (player.GetComponent<AttacksEarth>().earthTotemCurrentCount == 0)
        //{
        //    moving = true;
        //}
    }

    void Update()
    {
        if(moving)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        else if (transform.localScale.x > targetFullScale.x)
        {
            transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y + 1, transform.parent.position.z);
        }
        
        //transform.Translate(Vector2.up * (moveSpeed/5) * Time.deltaTime);
        if (growing)
        {
            //Debug.Log("Growing");
            //transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            transform.localScale += Vector3.one * Time.deltaTime * shrinkSpeed;
            transform.Translate(Vector2.up * (moveSpeed / 10f) * Time.deltaTime);
            if (transform.localScale.x > targetFullScale.x)
            {
                transform.localScale = targetFullScale;
                growing = false;
                //Invoke("DestroyBlocker", waitLifeTime);
                //if (player.GetComponent<AttacksEarth>().earthTotemCurrentCount < player.GetComponent<AttacksEarth>().earthTotemCount)
                //{
                //    GameObject totemPart = Instantiate(gameObject, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), transform.rotation);
                //    totemPart.transform.parent = gameObject.transform;
                //    totemPart.GetComponent<EarthSDefendTestScript>().moving = false;
                //    player.GetComponent<AttacksEarth>().earthTotemCurrentCount++;
                //}
                 
            }
        }
        //if (player.GetComponent<AttacksEarth>().earthTotemCurrentCount >= player.GetComponent<AttacksEarth>().earthTotemCount)
        //{
        //    player.GetComponent<AttacksEarth>().earthTotemCurrentCount = 0;
        //    Invoke("DestroyBlocker", 1);
        //}
    }

    void DestroyBlocker()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

    }
}
