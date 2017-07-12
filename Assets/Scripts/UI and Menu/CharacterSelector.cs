using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour {

    [Tooltip("The character that this selection icon is associated with.")]
    public GameObject characterSelectedPrefab;

    [Tooltip("The element that this selection icon is associated with.")]
    public Element element;

    [HideInInspector]//Makes sure that players can't select the same character as another player.
    public bool alreadySelected = false;

    public GameObject myToolTip;

    // Use this for initialization
    void Start () {
        myToolTip.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
        if (alreadySelected)
        {
            myToolTip.SetActive(false);
        }
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (alreadySelected == false)
        {
            myToolTip.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (alreadySelected == false)
        {
            myToolTip.SetActive(false);
        }
    }
}
