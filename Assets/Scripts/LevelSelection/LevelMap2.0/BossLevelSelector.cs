using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevelSelector : MonoBehaviour {

    [Tooltip("Level to load in the scene manager 'use flavorful text' ")]
    public string sceneName;
    [Tooltip("my level ID name 'ex. zoneALevel1' ")]
    public string levelIDName;
    [Tooltip("what Zone this is a part of 'A, B, C, D' ")]
    public string bossZone;
    [Tooltip("The number of levels required to beat before chalenging the boss.")]
    public int requiredLevelBeatCount;
    [Tooltip("The prime element for this level")]
    public Element primeElement;
    public string bossName;

    public GameObject previousLevel;

    public GameObject nextLevel;

    public GameObject pathEffect;

    public bool bossUnlocked;

    public bool bossBeaten;

    public GameObject switchActiveEffect;
    public SpriteRenderer bossUnlockProgress;
    public Sprite zeroLevelBeat;
    public Sprite oneLevelBeat;
    public Sprite twoLevelBeat;
    public Sprite threeLevelBeat;
    public Sprite bossBeatSprite;

    private Transform torchLightSpot;
    private GameObject unlitTorch;

    [HideInInspector]
    public PlayerDictionary playerDictionary;

    // Use this for initialization
    void Start () {
		//if (bossBeaten)
  //      {
  //          GameObject myPathEffect = Instantiate(pathEffect, transform.position, pathEffect.transform.rotation);
  //          myPathEffect.GetComponent<LevelMapPathEffect>().point2 = transform;
  //          myPathEffect.GetComponent<LevelMapPathEffect>().point1 = nextLevel.transform;
  //      }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UnlockBossLevel()
    {
        bossUnlocked = true;
        GetComponent<IsUnlocked>().isUnlocked = bossUnlocked;
    }

    public void CheckBossLevelBeaten()
    {
        bossBeaten =  playerDictionary.GetComponent<PlayerDictionary>().CheckBossBeat(levelIDName);
        GetComponent<IsUnlocked>().isBeaten = bossBeaten;
        if (bossBeaten)
        {
            nextLevel.GetComponent<LevelSelector>().CheckUnlock();
            GameObject myPathEffect = Instantiate(pathEffect, transform.position, pathEffect.transform.rotation);
            myPathEffect.GetComponent<LevelMapPathEffect>().point2 = transform;
            myPathEffect.GetComponent<LevelMapPathEffect>().point1 = nextLevel.transform;
            bossUnlockProgress.sprite = bossBeatSprite;
            BeatenTorchEffect();
        }
    }

    public void BeatenTorchEffect()
    {
        torchLightSpot = transform.Find("Trigger Location");
        unlitTorch = transform.Find("Locked Level Effect").gameObject;

        Instantiate(switchActiveEffect, torchLightSpot.position, switchActiveEffect.transform.rotation);
        Destroy(unlitTorch);
    }
}
