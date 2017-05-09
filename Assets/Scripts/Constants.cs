using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element { Fire, Ice, Earth, Air, None };
public enum AttackFromLocation { Empty, Self, Overhead, DownAngled, Horizontal, Behind, Below }; //Set up Self and Infront and behind
public enum EnemyTargetType { Element, Proximity, Random, Roam };

public class Constants : MonoBehaviour {

    [Tooltip("How far below the player do we check to count them as 'Grounded'")]
    public static float whatsBelowMeChecker = -0.7f; //make this private after feel check maybe this should be .6

    public static Element whatICounter(Element myElement)
    {
        switch(myElement)
        {
            case Element.Fire:
                return Element.Earth;
            case Element.Earth:
                return Element.Air;
            case Element.Air:
                return Element.Ice;
            case Element.Ice:
                return Element.Fire;
            default:
                return Element.None;
        }
    }

    public static Element whatCountersMe(Element myElement)
    {
        switch (myElement)
        {
            case Element.Fire:
                return Element.Ice;
            case Element.Earth:
                return Element.Fire;
            case Element.Air:
                return Element.Earth;
            case Element.Ice:
                return Element.Air;
            default:
                return Element.None;
        }
    }

    public static string WhatsBelowMe(GameObject me)
    {
        // Raycast below player and return what we find
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(me.transform.position, new Vector2(0.0f, whatsBelowMeChecker), out hit, 1))
        {
            if (hit.collider.gameObject.tag != null)
            {
                Debug.Log("Right below me " + hit.collider.gameObject.tag);
                return hit.collider.gameObject.tag;
            }
            else
            {
                return "";
            }
        }
        else if (Physics.Raycast(me.transform.position, new Vector3(0.3f, whatsBelowMeChecker - 0.1f, 0.0f), out hit, 1))
        {
            if (hit.collider.gameObject.tag != null)
            {
                Debug.Log("Right Down of me " + hit.collider.gameObject.tag);
                return hit.collider.gameObject.tag;
            }
            else
            {
                return "";
            }
        }
        else if (Physics.Raycast(me.transform.position, new Vector3(-0.3f, whatsBelowMeChecker - 0.1f, 0.0f), out hit, 1))
        {
            if (hit.collider.gameObject.tag != null)
            {
                Debug.Log("Left Down of me " + hit.collider.gameObject.tag);
                return hit.collider.gameObject.tag;
            }
            else
            {
                return "";
            }
        }
        else
        {
            return "";
        }

    }

    public static string WhatsBelowMe2D (GameObject me)
    {
        RaycastHit2D hit = Physics2D.Raycast(me.transform.position, new Vector2(0.0f, whatsBelowMeChecker));

            if (hit.collider != null && hit.collider != me)
            {
                Debug.Log("Right below me " + hit.collider);
                return hit.collider.gameObject.tag;
            }
            else
            {
                return "";
            }
    }

    //Use this OnDrawGizmos to draw our check lines.
    //public static void OnDrawGizmos()
    //{
    //    //used to check below player
    //    Vector3 belowCheckUI = new Vector3(0.0f, Constants.whatsBelowMeChecker, 0.0f);
    //    Gizmos.DrawRay(transform.position, belowCheckUI);
    //    //Used for a sphere below us
    //    //Gizmos.DrawSphere(new Vector3(transform.position.x,transform.position.y -0.2f, transform.position.z), 0.5f);
    //    //Use to cast a thick ray
    //    //Gizmos.DrawCube(new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), new Vector3(0.5f,0.5f,0f));

    //    Vector3 projectileCheckUI = new Vector3(0.5f, -0.5f, 0.0f);
    //    Gizmos.DrawRay(transform.position, projectileCheckUI);
    //}
}
