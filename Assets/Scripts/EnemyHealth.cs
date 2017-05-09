using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//public enum Element { Fire, Ice, Earth, Air };
public class EnemyHealth : MonoBehaviour
{
    [HideInInspector]
    public Element myElement;
    [Tooltip("How much health does this enemy have?")]
    public float health = 100f;
    [Tooltip("Fill with empty drops and pick ups that could be dropped.")]
    public Slider enemyHPUI;
    public GameObject enemyHPUIObject;

    [Tooltip("How much is the damage I take multiplied by if it counters my element? 'Make this above 1.0f'")]
    public float counterDamageModifier = 1.5f;
    [Tooltip("How much is the damage I take multiplied by if it counters my element? 'Make this below 1.0f'")]
    public float counterResistanceModifier = 0.75f;

    [Tooltip("Fill with empty drops and pick ups that could be dropped.")]
    public GameObject[] drops;


    private GameObject[] enemiesList;
    private Behaviour myEnemyScript;
    private GameObject canvas;
    private GameObject whatCantHitMe;

    // Use this for initialization
    void Start()
    {
        myEnemyScript = gameObject.GetComponent<Enemy>();
        myElement = gameObject.GetComponent<Enemy>().element;
        enemyHPUI.maxValue = health;
        whatCantHitMe = gameObject;

        canvas = GameObject.Find("Canvas");

        if (!canvas)
        {
            canvas = new GameObject("Canvas");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if(beenHit == true)
        //{
        //    enemyHPUI.transform.position = new Vector3(transform.position.x, transform.position.y - offSetUI, transform.position.z);
        //}
    }

    public void TakeDamage(GameObject whatHitMe, float damage, float hitStun)
    {
        //[TODO] Pull the element of what hit me, if it is what counters me it deals bonus damage if I counter it then deal less damage.

        if (whatHitMe.CompareTag("Projectile"))
        {
            if (whatHitMe.GetComponent<PlayerProjectile>().myElement == Constants.whatCountersMe(myElement))
            {
                damage = damage * counterDamageModifier;
            }
            else if (whatHitMe.GetComponent<PlayerProjectile>().myElement == Constants.whatICounter(myElement))
            {
                damage = damage * counterResistanceModifier;
            }
        }
        else
        {
            if (whatHitMe.GetComponent<PlayerMelee>().myElement == Constants.whatCountersMe(myElement))
            {
                damage = damage * counterDamageModifier;
            }
            else if (whatHitMe.GetComponent<PlayerMelee>().myElement == Constants.whatICounter(myElement))
            {
                damage = damage * counterResistanceModifier;
            }
        }
        if (whatHitMe != whatCantHitMe)
        {
            health -= damage;
            if (enemyHPUIObject.activeSelf == false)
            {
                enemyHPUIObject.SetActive(true);
            }
            //UpDateEnemyUI();
            enemyHPUI.value = health;
            //Hit stun
            myEnemyScript.enabled = false;
            Invoke("HitStun", hitStun);
            whatCantHitMe = whatHitMe;
            if (health <= 0)
            {
                //Spawns a random drop from drops
                int whatToSpawn = Random.Range(0, drops.Length);
                Instantiate(drops[whatToSpawn], transform.position, Quaternion.identity);

                if(drops[whatToSpawn].GetComponent<PickUpHealth>() != null)
                {
                    drops[whatToSpawn].GetComponent<PickUpHealth>().myElement = myElement;
                } 
                //trigger death animation

                DestroyObject();
            }
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void HitStun()
    {
        myEnemyScript.enabled = true;
        whatCantHitMe = gameObject;
    }

    //public void UpDateEnemyUI()
    //{
    //    enemyHPUI.value = health;
    //}
}