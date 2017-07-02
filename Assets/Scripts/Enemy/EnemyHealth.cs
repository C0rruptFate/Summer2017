using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//public enum Element { Fire, Ice, Earth, Air };
public class EnemyHealth : MonoBehaviour
{
    [Header("Spawn rate and type")]
    [Tooltip("Average Number of seconds between apperences")]
    public float seenEverySeconds;
    [Tooltip("Select the elemental type the enemy will be.")]
    public Element element;

    [Tooltip("How much health does this enemy have?")]
    public float health = 100f;
    [HideInInspector][Tooltip("Fill with empty drops and pick ups that could be dropped.")]
    public Slider enemyHPUI;
    [HideInInspector][Tooltip("Attach the enemyHpSlider that is a child of the enemy.")]
    public GameObject enemyHPUIObject;

    [Tooltip("How much is the damage I take multiplied by if it counters my element? 'Make this above 1.0f'")]
    public float counterDamageModifier = 1.5f;
    [Tooltip("How much is the damage I take multiplied by if it counters my element? 'Make this below 1.0f'")]
    public float counterResistanceModifier = 0.75f;

    [Tooltip("Fill with empty drops and pick ups that could be dropped.")]
    public GameObject[] drops;


    [HideInInspector]//Currently not being used.
    public GameObject[] enemiesList;
    [HideInInspector]//Set the Enemy controls script this holds the attack and movement for the enemy.
    public Enemy myEnemyScript;
    [HideInInspector]//
    public GameObject canvas;
    [HideInInspector]
    public GameObject whatCantHitMe;

    // Use this for initialization
    public virtual void Start()
    {
        myEnemyScript = gameObject.GetComponent<Enemy>();
        //Debug.Log("myEnemyScript: " + myEnemyScript);
        enemyHPUI.maxValue = health;
        whatCantHitMe = gameObject;

        canvas = GameObject.Find("Canvas");

        if (!canvas)
        {
            canvas = new GameObject("Canvas");
        }
        //Used to set up the enemy HP sliders for the enemy so that the designer doesn't need to. Remove these lines and remove the [HideinInspector] on these varables if this doesn't work.
        if(enemyHPUIObject == null)
        {
            enemyHPUIObject = canvas.transform.Find("Enemy HP Slider").gameObject;
            //Debug.Log("Enemy HP Slider Object: " + enemyHPUIObject);
            enemyHPUI = enemyHPUIObject.GetComponent<Slider>();
        }
    }

    public virtual void TakeDamage(GameObject whatHitMe, float damage, float hitStun)
    {
        //Checks the element of what hit me and causes me to take extra damage or reduced damage.
        if (whatHitMe.CompareTag("Projectile"))
        {
            if (whatHitMe.GetComponent<PlayerProjectile>().element == Constants.whatCountersMe(element))
            {
                damage = damage * counterDamageModifier;
            }
            else if (whatHitMe.GetComponent<PlayerProjectile>().element == Constants.whatICounter(element))
            {
                damage = damage * counterResistanceModifier;
            }
        }
        else
        {
            if (whatHitMe.GetComponent<PlayerMelee>().myElement == Constants.whatCountersMe(element))
            {
                damage = damage * counterDamageModifier;
            }
            else if (whatHitMe.GetComponent<PlayerMelee>().myElement == Constants.whatICounter(element))
            {
                damage = damage * counterResistanceModifier;
            }
        }
        if (whatHitMe != whatCantHitMe)//Makes it so that this enemy can't be hit by the same attack right away (largely for melee attacks that last long).
        {
            health -= damage;//Takes damage.
            if (enemyHPUIObject.activeSelf == false)//Enables the hp bar for this enemy when they get hit.
            {
                enemyHPUIObject.SetActive(true);
            }
            UpDateEnemyUI();//Caues the enemy hp to update when they get hit.
            Debug.Log("I took Damage: " + damage);
            //Hit stun
            myEnemyScript.enabled = false;
            Invoke("HitStun", hitStun);//calls hit stun as long as the attack that hit me says it should last for.
            whatCantHitMe = whatHitMe;//Makes it so that I can't be hit by the same object again.
            if (health <= 0)//If I am out of HP destroy gameobject.
            {
                //Spawns a random drop from drops
                int whatToSpawn = Random.Range(0, drops.Length); //Decides what to drop when the enemy is killed.
                GameObject pickUp = Instantiate(drops[whatToSpawn], transform.position, Quaternion.identity);//drops the pick up that is choosen at random.
                if (drops == null)
                {
                    Debug.LogError("Set up pickups");//If the pick up isn't set up play this message for the designer.
                }

                if (drops[whatToSpawn].GetComponent<PickUpHealth>() != null)//Sets the pick up element to my element.
                {
                    pickUp.GetComponent<PickUpHealth>().pickUpElement = element;
                } 

                //calls destroy enemy
                DestroyObject();
            }
        }
    }

    public virtual void DestroyObject()//Paly death animation, wait then enemy dies.
    {
        //[TODO]trigger death animation
        Destroy(gameObject);
    }

    public virtual void HitStun()//Resets what can hit me and removes stun effect.
    {
        myEnemyScript.enabled = true;
        whatCantHitMe = gameObject;
    }

    public virtual void UpDateEnemyUI()//Updates enemy hp slider when enemy is hit.
    {
        enemyHPUI.value = health;
    }
}