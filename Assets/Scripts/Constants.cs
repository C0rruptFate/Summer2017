using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element { None, Fire, Ice, Earth, Wind, };
public enum AttackFromLocation { Empty, Self, Overhead, DownAngled, Horizontal, Behind, Below }; //Set up Self and Infront and behind
public enum EnemyTargetType { Element, Proximity, Random, Roam };
public enum CameraEdges { Left, Right, Top, Bottom};

public class Constants : MonoBehaviour
{
    //Colors for each element
    public static Color32 fireColor = new Color32(255,69,00,255);
    public static Color32 waterColor = new Color32(165, 242, 243, 255);
    public static Color32 airColor = new Color32(156, 49, 241, 255);
    public static Color32 earthColor = new Color32(109, 93, 73, 255);

    [Tooltip("How far below the player do we check to count them as 'Grounded'")]
    public static float whatsBelowMeChecker = -0.7f; //make this private after feel check maybe this should be .6
    public static int playerCount;

    public static Element player1Element = Element.None;
    public static Element player2Element = Element.None;
    public static Element player3Element = Element.None;
    public static Element player4Element = Element.None;

    public static Element whatICounter(Element myElement)
    {
        switch(myElement)
        {
            case Element.Fire:
                return Element.Earth;
            case Element.Earth:
                return Element.Wind;
            case Element.Wind:
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
            case Element.Wind:
                return Element.Earth;
            case Element.Ice:
                return Element.Wind;
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

}
