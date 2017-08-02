using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugScript : MonoBehaviour
{

    public Text debugText;

    // Use this for initialization
    void Start()
    {
        debugText = GameObject.Find("Debug Text").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        debugText.text = "Got to here";
    }
}
