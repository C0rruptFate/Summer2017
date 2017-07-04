using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject enemyHPUISliderObject;

    [Tooltip("How much is the damage I take multiplied by if it counters my element? 'Make this above 1.0f'")]
    public float counterDamageModifier = 0.5f;
    [Tooltip("How much is the damage I take multiplied by if it counters my element? 'Make this below 1.0f'")]
    public float counterResistanceModifier = 0.25f;

    [Tooltip("Fill with empty drops and pick ups that could be dropped.")]
    public GameObject[] drops;

    [Tooltip("The Y offset that the HP bar should be at.")]
    public Vector3 hpBarOffSet;


    [HideInInspector]//Currently not being used.
    public GameObject[] enemiesList;
    [HideInInspector]//Set the Enemy controls script this holds the attack and movement for the enemy.
    public Enemy myEnemyScript;
    //[HideInInspector]//
    //public GameObject canvas;
    [HideInInspector]
    public GameObject whatCantHitMe;
    [HideInInspector]
    public bool invulnerable = false;

    // Use this for initialization
    public virtual void Start()
    {
        myEnemyScript = gameObject.GetComponent<Enemy>();
        //Debug.Log("myEnemyScript: " + myEnemyScript);
        enemyHPUI.maxValue = health;
        whatCantHitMe = gameObject;

        //Old enemy canvas ui hp bar
        //canvas = GameObject.Find("Canvas");

        //if (!canvas)
        //{
        //    canvas = new GameObject("Canvas");
        //}
        //Used to set up the enemy HP sliders for the enemy so that the designer doesn't need to. Remove these lines and remove the [HideinInspector] on these varables if this doesn't work.
        if(enemyHPUISliderObject == null)
        {
            enemyHPUISliderObject = transform.Find("Enemy HP Slider").gameObject;
            //Debug.Log("Enemy HP Slider Object: " + enemyHPUIObject);
            enemyHPUI = enemyHPUISliderObject.GetComponent<Slider>();
        }
        enemyHPUISliderObject.transform.SetParent(GameObject.Find("Camera Rig").transform.Find("Main Camera Orthagraphic").Find("Canvas-WorldSpace"), false);
        //enemyHPUISliderObject.transform.parent = GameObject.Find("Camera Rig").transform.Find("Main Camera Orthagraphic").Find("Canvas-WorldSpace");
        if (enemyHPUISliderObject.transform.parent == transform)
        {
            Debug.LogError("Couldn't find the Camera canvas");
        }
        enemyHPUISliderObject.GetComponent<EnemyHPUISliderScript>().myEnemy = gameObject;
        enemyHPUISliderObject.GetComponent<EnemyHPUISliderScript>().offSet = hpBarOffSet;
    }

    public virtual void TakeDamage(GameObject whatHitMe, float damage, float hitStun)
    {
        float totalDamageModifier = 0;
        //Checks the element of what hit me and causes me to take extra damage or reduced damage.
        if (whatHitMe.GetComponent<Projectiles>() != null)
        {
            if (whatHitMe.GetComponent<Projectiles>().element == Constants.whatCountersMe(element))
            {
                totalDamageModifier = totalDamageModifier - counterDamageModifier;
            }
            else if (whatHitMe.GetComponent<Projectiles>().element == Constants.whatICounter(element))
            {
                totalDamageModifier = totalDamageModifier + counterResistanceModifier;
            }
        }
        else if (whatHitMe.GetComponent<PlayerMelee>() != null)
        {
            if (whatHitMe.GetComponent<PlayerMelee>().myElement == Constants.whatCountersMe(element))
            {
                totalDamageModifier = totalDamageModifier - counterDamageModifier;
            }
            else if (whatHitMe.GetComponent<PlayerMelee>().myElement == Constants.whatICounter(element))
            {
                totalDamageModifier = totalDamageModifier + counterResistanceModifier;
            }
        }
        else if (whatHitMe.GetComponent<Hazard>() != null)
        {
            if (whatHitMe.GetComponent<Hazard>().element == Constants.whatCountersMe(element))
            {
                totalDamageModifier = totalDamageModifier - counterDamageModifier;
            }
            else if (whatHitMe.GetComponent<Hazard>().element == Constants.whatICounter(element))
            {
                totalDamageModifier = totalDamageModifier + counterResistanceModifier;
            }
        }
        else
        {
            Debug.LogError("Something tried to damage " + gameObject.name + " that I don't have listed!");
        }

        // [TODO] Enemies don't block (yet)
        //if (gameObject.GetComponent<PlayerAttacks>().blocking)//If I am blocking take reduced damage and no hitstun.
        //{
        //    totalDamageModifier = totalDamageModifier + gameObject.GetComponent<PlayerAttacks>().blockingResistanceModifier;
        //    Debug.Log("blocking mod: " + gameObject.GetComponent<PlayerAttacks>().blockingResistanceModifier);
        //    Debug.Log("total Damage Mod round 3 after blocking: " + totalDamageModifier);
        //    Debug.Log("damage round 3 after blocking: " + damage);
        //    hitStun = 0;
        //}

        //if invulnerable don't take damage
        if (invulnerable)
        {
            damage = 0;
        }

        damage = damage * (1 - totalDamageModifier); //calculates total damage taken.


        if (whatHitMe != whatCantHitMe)//Makes it so that this enemy can't be hit by the same attack right away (largely for melee attacks that last long).
        {
            health -= damage;//Takes damage.
            if (enemyHPUISliderObject.activeSelf == false)//Enables the hp bar for this enemy when they get hit.
            {
                enemyHPUISliderObject.SetActive(true);
            }
            UpDateEnemyUI();//Caues the enemy hp to update when they get hit.
            //Debug.Log("I took Damage: " + damage);
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