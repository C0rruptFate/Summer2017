using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {

    public bool unlocked;
    public string sceneName;
    public Element primeElement;

    public GameObject levelSelectMap;

    public GameObject switchActiveEffect;//The effect that is played when this switch is flipped
    //public GameObject unlockEffect;
    //public float effectDuration = 5;

    private Transform torchLightSpot;//Where to spawn the effect when this has been activated
    private Transform parentObject;//What door/end level or other object I am a part of.

    [SerializeField]
    private GameObject unlitTorch;

    // Use this for initialization
    void Start () {


        //if (gameObject.name.Contains("1"))
        //{
        //    UnlockTorch();
        //}
    }

    public void UnlockTorch()
    {
        //levelSelectMap = GameObject.Find("Level Select Map");
        //levelSelectMap.GetComponent<LevelSelectMap>().levelName = GameObject.Find("Level Name Text").GetComponent<Text>();
        //levelSelectMap.GetComponent<LevelSelectMap>().levelNameImage = GameObject.Find("Level Name Text").GetComponent<Image>();
        torchLightSpot = transform.Find("Trigger Location");//Sets the position of where the effect should spawn when this is active.
        parentObject = transform.parent;//Sets what door or other object I am a part of, used to solve puzzles.
        unlitTorch = transform.Find("Locked Level Effect").gameObject;

        //if (!unlocked)
        //{
        //    GameObject myUnlockEffect = Instantiate(unlockEffect, torchLightSpot.position, switchActiveEffect.transform.rotation);
        //    myUnlockEffect.GetComponent<DestroySelfRightAway>().waitToDestroyTime = effectDuration;
        //}

        unlocked = true;
        Instantiate(switchActiveEffect, torchLightSpot.position, switchActiveEffect.transform.rotation);
        Destroy(unlitTorch);
    }
}
