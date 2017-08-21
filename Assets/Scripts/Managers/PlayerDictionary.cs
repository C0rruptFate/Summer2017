using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDictionary : MonoBehaviour
{//Used to save to, and pull from the data file.

    public Dictionary<string, bool> levelBeat = new Dictionary<string, bool>();
    public Dictionary<string, int> levelKills = new Dictionary<string, int>();
    public Dictionary<string, bool> bossBeat = new Dictionary<string, bool>();
    public Dictionary<string, int> zone = new Dictionary<string, int>();

    public static PlayerDictionary playerDictionary;

    //public Dictionary<string, Dictionary<bool, int>> stuff = new Dictionary<string, Dictionary<bool, int>>();
    void Awake()
    {
        if (playerDictionary == null)
        {
            DontDestroyOnLoad(gameObject);
            playerDictionary = this;
        }
        else if (playerDictionary != this)
        {
            Destroy(gameObject);
        }
        //bool zoneALevel1Beat = new bool ("zoneAlevel1Beat", false);

        //Zone A
        levelBeat.Add("zoneALevel1", false);
        levelKills.Add("zoneALevel1", 0);
        levelBeat.Add("zoneALevel2", false);
        levelKills.Add("zoneALevel2", 0);
        levelBeat.Add("zoneALevel3", false);
        levelKills.Add("zoneALevel3", 0);
        levelBeat.Add("zoneALevel4", false);
        levelKills.Add("zoneALevel4", 0);
        bossBeat.Add("zoneABossBeat", false);
        zone.Add("A", 0);

        //Zone B
        levelBeat.Add("zoneBLevel1", false);
        levelKills.Add("zoneBLevel1", 0);
        levelBeat.Add("zoneBLevel2", false);
        levelKills.Add("zoneBLevel2", 0);
        levelBeat.Add("zoneBLevel3", false);
        levelKills.Add("zoneBLevel3", 0);
        levelBeat.Add("zoneBLevel4", false);
        levelKills.Add("zoneBLevel4", 0);
        bossBeat.Add("zoneBBossBeat", false);
        zone.Add("B", 0);

        //Zone C
        levelBeat.Add("zoneCLevel1", false);
        levelKills.Add("zoneCLevel1", 0);
        levelBeat.Add("zoneCLevel2", false);
        levelKills.Add("zoneCLevel2", 0);
        levelBeat.Add("zoneCLevel3", false);
        levelKills.Add("zoneCLevel3", 0);
        levelBeat.Add("zoneCLevel4", false);
        levelKills.Add("zoneCLevel4", 0);
        bossBeat.Add("zoneCBossBeat", false);
        zone.Add("C", 0);

        //Zone D
        levelBeat.Add("zoneDLevel1", false);
        levelKills.Add("zoneDLevel1", 0);
        levelBeat.Add("zoneDLevel2", false);
        levelKills.Add("zoneDLevel2", 0);
        levelBeat.Add("zoneDLevel3", false);
        levelKills.Add("zoneDLevel3", 0);
        levelBeat.Add("zoneDLevel4", false);
        levelKills.Add("zoneDLevel4", 0);
        bossBeat.Add("zoneDBossBeat", false);
        zone.Add("D", 0);
        
    }

    //Used when a level is beat for the first time.
    public void BeatLevel(string level, string zoneGroup)
    {
        levelBeat[level] = true;
        zone[zoneGroup]++;
    }

    //Used to check if a level is beat.
    public bool CheckLevelBeat(string level)
    {
        return levelBeat[level];
    }

    //Used to update if a level is beat or not
    public void UpdateLevelBeat(string level, bool status)
    {
        levelBeat[level] = status;
    }

    //Used to check the current highest # of enemies killed on a level
    public int CheckLevelKills(string enemies)
    {
        return levelKills[enemies];
    }

    //Used to update the highest # of enemies killed on a level
    public void UpdateEnemiesSlain(string level, int killCount)
    {
        if (levelKills[level] < killCount)
        {
            levelKills[level] = killCount;
        }
    }

    //Used to check if the player has beaten the correct number of levels before taking on the boss.
    public int CheckZoneClearCount(string zoneGroup)
    {
        return zone[zoneGroup];
    }

    //Used to update the zone group so that it matches the total number of levels beaten in that zone.
    public void UpdateZoneClearCount(string zoneGroup, int count)
    {
        zone[zoneGroup] = count;
    }

    //Used to Check if a boss has been beaten
    public bool CheckBossBeat(string boss)
    {
        return bossBeat[boss];
    }

    //Used to update if a boss has been beaten or not.
    public void UpdateBossBeat(string level, bool status)
    {
        bossBeat[level] = status;
    }

    //void Update()
    //{
    //        //Debug.Log("number of levels in the level beat dic, " + levelBeat.Count);
    //}
    //public bool isBossBeat(string bossLevel)
    //{
    //    return bossBeat[bossLevel];
    //}

    //public void 

    //public int level_count;
    //public Dictionary<string, int[]> level_data = new Dictionary<string, int[]>();

    //private void Start()
    //{
    //    for (int i = 0; i < level_count; i++)
    //        level_data.Add("level_0" + i, new int[] { 0, 0 });
    //}

    //public void UpdateLevelData(string level_name, int kills)
    //{
    //    int[] data = level_data[level_name];

    //    if (kills > data[0])
    //    {
    //        data[0] = kills;
    //    }
    //    level_data[level_name] = data;
    //}
}
