using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element { None, Fire, Ice, Earth, Wind, }; //Possible player elements
public enum AttackFromLocation { Empty, Self, Overhead, DownAngled, Horizontal, Behind, Below }; //where each player's attack can come from
public enum EnemyTargetType { Element, Proximity, Random, Roam }; //How enemies find their targets
public enum PuzzleType {None, Door, EndLevel}//What does each switch controller do when all switches are hit (open a door, end the level spawn enemies, other)

public class Constants : MonoBehaviour
{
    //Colors for each element
    public static Color fireColor = new Color(255,69,00,128); //hex FF450080
    public static Color waterColor = new Color(35, 137, 218, 128);
    public static Color airColor = new Color(253, 208, 35, 128); //hex FDD023
    public static Color earthColor = new Color(109, 93, 73, 128);

    [Tooltip("How far below the player do we check to count them as 'Grounded'")]
    public static float whatsBelowMeChecker = -0.7f; //make this private after feel check maybe this should be .6
    //the Number of players that joined the level
    public static int playerCount;

    //The element each player selected
    public static Element player1Element = Element.None;
    public static Element player2Element = Element.None;
    public static Element player3Element = Element.None;
    public static Element player4Element = Element.None;

    //what difficulty was choosen, might not use this
    public static int difficulty = 1;

    //determins what element I counter depending on what element I am. Used to deal extra damage to what I counter, and take less from what I counter.
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

    //Determins what element counters me. Used to deal reduced damage to what counters me and take more from what I counter.
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

    //Used to check what is below me, for things like jumping or attach jumps
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

    //Used to check what is below me, for things like jumping or attach jumps in 3D, currently not being used.
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

}
