using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverWorldSmoke : MonoBehaviour {

    public GameObject firstLevelOfMyZone;

    [SerializeField]
    private ParticleSystem.EmissionModule particle;

    public int tempRate;

    // Use this for initialization
    void Start () {
		if (firstLevelOfMyZone.GetComponent<LevelSelector>().isBeaten)
        {
            Destroy(gameObject);
        }
        else if (firstLevelOfMyZone == null)
        {
            Debug.LogError("Attach first level for this zone to: " + gameObject.name);
        }
        particle = GetComponent<ParticleSystem>().emission;
        tempRate = 25;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator DecreaseSmoke()
    {
        
        tempRate = tempRate - 13;
        particle.rateOverTime = tempRate;
        //GetComponent<ParticleSystem>().emission.rateOverTime = tempRate;
        yield return new WaitForSeconds(0.05f);
        if (tempRate <= 0)
        {
            yield return new WaitForSeconds(0.1f);
            StopCoroutine("DecreaseSmoke");
            Destroy(gameObject);
        }
    }
}
