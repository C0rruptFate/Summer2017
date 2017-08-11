using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

//public class PlayerDictionary : MonoBehaviour
//{//Used to save to, and pull from the data file.

//    public Dictionary<string, bool> levelBeat = new Dictionary<string, bool>();
//    public Dictionary<string, int> levelKills = new Dictionary<string, int>();
//    public Dictionary<string, bool> bossBeat = new Dictionary<string, bool>();
//    public Dictionary<string, int> zone = new Dictionary<string, int>();

//    //public Dictionary<string, Dictionary<bool, int>> stuff = new Dictionary<string, Dictionary<bool, int>>();

//    void Awake()
//    {
//        //bool zoneALevel1Beat = new bool ("zoneAlevel1Beat", false);

//        //Zone A
//        levelBeat.Add("zoneALevel1", false);
//        levelKills.Add("zoneALevel1", 0);
//        levelBeat.Add("zoneALevel2", false);
//        levelKills.Add("zoneALevel2", 0);
//        levelBeat.Add("zoneALevel3", false);
//        levelKills.Add("zoneALevel3", 0);
//        levelBeat.Add("zoneALevel4", false);
//        levelKills.Add("zoneALevel4", 0);
//        bossBeat.Add("zoneABossBeat", false);
//        zone.Add("A", 0);

//        //Zone B
//        levelBeat.Add("zoneBLevel1", false);
//        levelKills.Add("zoneBLevel1", 0);
//        levelBeat.Add("zoneBLevel2", false);
//        levelKills.Add("zoneBLevel2", 0);
//        levelBeat.Add("zoneBLevel3", false);
//        levelKills.Add("zoneBLevel3", 0);
//        levelBeat.Add("zoneBLevel4", false);
//        levelKills.Add("zoneBLevel4", 0);
//        bossBeat.Add("zoneBBossBeat", false);
//        zone.Add("B", 0);

//        //Zone C
//        levelBeat.Add("zoneCLevel1", false);
//        levelKills.Add("zoneCLevel1", 0);
//        levelBeat.Add("zoneCLevel2", false);
//        levelKills.Add("zoneCLevel2", 0);
//        levelBeat.Add("zoneCLevel3", false);
//        levelKills.Add("zoneCLevel3", 0);
//        levelBeat.Add("zoneCLevel4", false);
//        levelKills.Add("zoneCLevel4", 0);
//        bossBeat.Add("zoneCBossBeat", false);
//        zone.Add("C", 0);

//        //Zone D
//        levelBeat.Add("zoneDLevel1", false);
//        levelKills.Add("zoneDLevel1", 0);
//        levelBeat.Add("zoneDLevel2", false);
//        levelKills.Add("zoneDLevel2", 0);
//        levelBeat.Add("zoneDLevel3", false);
//        levelKills.Add("zoneDLevel3", 0);
//        levelBeat.Add("zoneDLevel4", false);
//        levelKills.Add("zoneDLevel4", 0);
//        bossBeat.Add("zoneDBossBeat", false);
//        zone.Add("D", 0);

//        Debug.Log("Added to dictonary");
//    }

//    public void BeatLevel(string level, string zoneGroup)
//    {
//        levelBeat[level] = true;
//        zone[zoneGroup]++;
//    }

//    //public int enemiesSlain(string enemies)
//    //{
//    //    return levelKills[enemies];
//    //}

//    public void CheckEnemiesSlain(string level, int killCount)
//    {
//        if (levelKills[level] < killCount)
//        {
//            levelKills[level] = killCount;
//        }
//    }

//    //public bool isBossBeat(string bossLevel)
//    //{
//    //    return bossBeat[bossLevel];
//    //}

//    //public void 

//    //public int level_count;
//    //public Dictionary<string, int[]> level_data = new Dictionary<string, int[]>();

//    //private void Start()
//    //{
//    //    for (int i = 0; i < level_count; i++)
//    //        level_data.Add("level_0" + i, new int[] { 0, 0 });
//    //}

//    //public void UpdateLevelData(string level_name, int kills)
//    //{
//    //    int[] data = level_data[level_name];

//    //    if (kills > data[0])
//    //    {
//    //        data[0] = kills;
//    //    }
//    //    level_data[level_name] = data;
//    //}
//}

public class SaveData : MonoBehaviour {

    public bool zoneALevel1Beat;
    public int zoneALevel1EnemiesSlain;
    public bool zoneALevel2Beat;
    public int zoneALevel2EnemiesSlain;
    public bool zoneALevel3Beat;
    public int zoneALevel3EnemiesSlain;
    public bool zoneALevel4Beat;
    public int zoneALevel4EnemiesSlain;
    public bool zoneABossBeat;

    public bool zoneBLevel1Beat;
    public int zoneBLevel1EnemiesSlain;
    public bool zoneBLevel2Beat;
    public int zoneBLevel2EnemiesSlain;
    public bool zoneBLevel3Beat;
    public int zoneBLevel3EnemiesSlain;
    public bool zoneBLevel4Beat;
    public int zoneBLevel4EnemiesSlain;
    public bool zoneBBossBeat;

    public bool zoneCLevel1Beat;
    public int zoneCLevel1EnemiesSlain;
    public bool zoneCLevel2Beat;
    public int zoneCLevel2EnemiesSlain;
    public bool zoneCLevel3Beat;
    public int zoneCLevel3EnemiesSlain;
    public bool zoneCLevel4Beat;
    public int zoneCLevel4EnemiesSlain;
    public bool zoneCBossBeat;

    public bool zoneDLevel1Beat;
    public int zoneDLevel1EnemiesSlain;
    public bool zoneDLevel2Beat;
    public int zoneDLevel2EnemiesSlain;
    public bool zoneDLevel3Beat;
    public int zoneDLevel3EnemiesSlain;
    public bool zoneDLevel4Beat;
    public int zoneDLevel4EnemiesSlain;
    public bool zoneDBossBeat;

    public static SaveData saveData;

    void Awake()
    {
        if (saveData == null)
        {
            DontDestroyOnLoad(gameObject);
            saveData = this;
        }
        else if (saveData != this)
        {
            Destroy(gameObject);
        }
    }

    //Save code could be on disable to make sure it saves when you quit and load code on enable to load it right away
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerData data = new PlayerData();

        PlayerDictionary dataDictionary = GetComponent<PlayerDictionary>();


        data.zoneALevel1Beat = dataDictionary.CheckLevelBeat("zoneALevel1");
        data.zoneALevel1EnemiesSlain = dataDictionary.CheckLevelKills("zoneALevel1");
        data.zoneALevel2Beat = dataDictionary.CheckLevelBeat("zoneALevel2");
        data.zoneALevel2EnemiesSlain = dataDictionary.CheckLevelKills("zoneALevel2");
        data.zoneALevel3Beat = dataDictionary.CheckLevelBeat("zoneALevel3");
        data.zoneALevel3EnemiesSlain = dataDictionary.CheckLevelKills("zoneALevel3");
        data.zoneALevel4Beat = dataDictionary.CheckLevelBeat("zoneALevel4");
        data.zoneALevel4EnemiesSlain = dataDictionary.CheckLevelKills("zoneALevel4");
        data.zoneABossBeat = dataDictionary.CheckBossBeat("zoneABossBeat");
        data.zoneALevelBeatCount = dataDictionary.CheckZoneClearCount("A");

        data.zoneBLevel1Beat = dataDictionary.CheckLevelBeat("zoneBLevel1");
        data.zoneBLevel1EnemiesSlain = dataDictionary.CheckLevelKills("zoneBLevel1");
        data.zoneBLevel2Beat = dataDictionary.CheckLevelBeat("zoneBLevel2");
        data.zoneBLevel2EnemiesSlain = dataDictionary.CheckLevelKills("zoneBLevel2");
        data.zoneBLevel3Beat = dataDictionary.CheckLevelBeat("zoneBLevel3");
        data.zoneBLevel3EnemiesSlain = dataDictionary.CheckLevelKills("zoneBLevel3");
        data.zoneBLevel4Beat = dataDictionary.CheckLevelBeat("zoneBLevel4");
        data.zoneBLevel4EnemiesSlain = dataDictionary.CheckLevelKills("zoneBLevel4");
        data.zoneBBossBeat = dataDictionary.CheckBossBeat("zoneBBossBeat");
        data.zoneBLevelBeatCount = dataDictionary.CheckZoneClearCount("B");

        data.zoneCLevel1Beat = dataDictionary.CheckLevelBeat("zoneCLevel1");
        data.zoneCLevel1EnemiesSlain = dataDictionary.CheckLevelKills("zoneCLevel1");
        data.zoneCLevel2Beat = dataDictionary.CheckLevelBeat("zoneCLevel2");
        data.zoneCLevel2EnemiesSlain = dataDictionary.CheckLevelKills("zoneCLevel2");
        data.zoneCLevel3Beat = dataDictionary.CheckLevelBeat("zoneCLevel3");
        data.zoneCLevel3EnemiesSlain = dataDictionary.CheckLevelKills("zoneCLevel3");
        data.zoneCLevel4Beat = dataDictionary.CheckLevelBeat("zoneCLevel4");
        data.zoneCLevel4EnemiesSlain = dataDictionary.CheckLevelKills("zoneCLevel4");
        data.zoneCBossBeat = dataDictionary.CheckBossBeat("zoneCBossBeat");
        data.zoneCLevelBeatCount = dataDictionary.CheckZoneClearCount("C");

        data.zoneDLevel1Beat = dataDictionary.CheckLevelBeat("zoneDLevel1");
        data.zoneDLevel1EnemiesSlain = dataDictionary.CheckLevelKills("zoneDLevel1");
        data.zoneDLevel2Beat = dataDictionary.CheckLevelBeat("zoneDLevel2");
        data.zoneDLevel2EnemiesSlain = dataDictionary.CheckLevelKills("zoneDLevel2");
        data.zoneDLevel3Beat = dataDictionary.CheckLevelBeat("zoneDLevel3");
        data.zoneDLevel3EnemiesSlain = dataDictionary.CheckLevelKills("zoneDLevel3");
        data.zoneDLevel4Beat = dataDictionary.CheckLevelBeat("zoneDLevel4");
        data.zoneDLevel4EnemiesSlain = dataDictionary.CheckLevelKills("zoneDLevel4");
        data.zoneDBossBeat = dataDictionary.CheckBossBeat("zoneDBossBeat");
        data.zoneDLevelBeatCount = dataDictionary.CheckZoneClearCount("D");

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            PlayerDictionary dataDictionary = GetComponent<PlayerDictionary>();
            file.Close();


            dataDictionary.UpdateLevelBeat("zoneALevel1", data.zoneALevel1Beat);
            dataDictionary.UpdateEnemiesSlain("zoneALevel2", data.zoneALevel1EnemiesSlain);
            dataDictionary.UpdateLevelBeat("zoneALevel2", data.zoneALevel2Beat);
            dataDictionary.UpdateEnemiesSlain("zoneALevel2", data.zoneALevel2EnemiesSlain);
            dataDictionary.UpdateLevelBeat("zoneALevel3", data.zoneALevel3Beat);
            dataDictionary.UpdateEnemiesSlain("zoneALevel3", data.zoneALevel3EnemiesSlain);
            dataDictionary.UpdateLevelBeat("zoneALevel4", data.zoneALevel4Beat);
            dataDictionary.UpdateEnemiesSlain("zoneALevel4", data.zoneALevel4EnemiesSlain);
            dataDictionary.UpdateBossBeat("zoneABossBeat", data.zoneABossBeat);
            dataDictionary.UpdateZoneClearCount("A", data.zoneALevelBeatCount);

            dataDictionary.UpdateLevelBeat("zoneBLevel1", data.zoneBLevel1Beat);
            dataDictionary.UpdateEnemiesSlain("zoneBLevel2", data.zoneBLevel1EnemiesSlain);
            dataDictionary.UpdateLevelBeat("zoneBLevel2", data.zoneBLevel2Beat);
            dataDictionary.UpdateEnemiesSlain("zoneBLevel2", data.zoneBLevel2EnemiesSlain);
            dataDictionary.UpdateLevelBeat("zoneBLevel3", data.zoneBLevel3Beat);
            dataDictionary.UpdateEnemiesSlain("zoneBLevel3", data.zoneBLevel3EnemiesSlain);
            dataDictionary.UpdateLevelBeat("zoneBLevel4", data.zoneBLevel4Beat);
            dataDictionary.UpdateEnemiesSlain("zoneBLevel4", data.zoneBLevel4EnemiesSlain);
            dataDictionary.UpdateBossBeat("zoneBBossBeat", data.zoneBBossBeat);
            dataDictionary.UpdateZoneClearCount("B", data.zoneBLevelBeatCount);

            dataDictionary.UpdateLevelBeat("zoneCLevel1", data.zoneCLevel1Beat);
            dataDictionary.UpdateEnemiesSlain("zoneALevel2", data.zoneCLevel1EnemiesSlain);
            dataDictionary.UpdateLevelBeat("zoneCLevel2", data.zoneCLevel2Beat);
            dataDictionary.UpdateEnemiesSlain("zoneALevel2", data.zoneCLevel2EnemiesSlain);
            dataDictionary.UpdateLevelBeat("zoneCLevel3", data.zoneCLevel3Beat);
            dataDictionary.UpdateEnemiesSlain("zoneALevel3", data.zoneCLevel3EnemiesSlain);
            dataDictionary.UpdateLevelBeat("zoneCLevel4", data.zoneCLevel4Beat);
            dataDictionary.UpdateEnemiesSlain("zoneALevel4", data.zoneCLevel4EnemiesSlain);
            dataDictionary.UpdateBossBeat("zoneCBossBeat", data.zoneCBossBeat);
            dataDictionary.UpdateZoneClearCount("C", data.zoneCLevelBeatCount);

            dataDictionary.UpdateLevelBeat("zoneDLevel1", data.zoneDLevel1Beat);
            dataDictionary.UpdateEnemiesSlain("zoneALevel2", data.zoneDLevel1EnemiesSlain);
            dataDictionary.UpdateLevelBeat("zoneDLevel2", data.zoneDLevel2Beat);
            dataDictionary.UpdateEnemiesSlain("zoneALevel2", data.zoneDLevel2EnemiesSlain);
            dataDictionary.UpdateLevelBeat("zoneDLevel3", data.zoneDLevel3Beat);
            dataDictionary.UpdateEnemiesSlain("zoneALevel3", data.zoneDLevel3EnemiesSlain);
            dataDictionary.UpdateLevelBeat("zoneDLevel4", data.zoneDLevel4Beat);
            dataDictionary.UpdateEnemiesSlain("zoneALevel4", data.zoneDLevel4EnemiesSlain);
            dataDictionary.UpdateBossBeat("zoneDBossBeat", data.zoneDBossBeat);
            dataDictionary.UpdateZoneClearCount("D", data.zoneDLevelBeatCount);
        }
    }
}

[Serializable]
class PlayerData
{
    public bool zoneALevel1Beat;
    public int zoneALevel1EnemiesSlain;
    public bool zoneALevel2Beat;
    public int zoneALevel2EnemiesSlain;
    public bool zoneALevel3Beat;
    public int zoneALevel3EnemiesSlain;
    public bool zoneALevel4Beat;
    public int zoneALevel4EnemiesSlain;
    public bool zoneABossBeat;
    public int zoneALevelBeatCount;

    public bool zoneBLevel1Beat;
    public int zoneBLevel1EnemiesSlain;
    public bool zoneBLevel2Beat;
    public int zoneBLevel2EnemiesSlain;
    public bool zoneBLevel3Beat;
    public int zoneBLevel3EnemiesSlain;
    public bool zoneBLevel4Beat;
    public int zoneBLevel4EnemiesSlain;
    public bool zoneBBossBeat;
    public int zoneBLevelBeatCount;

    public bool zoneCLevel1Beat;
    public int zoneCLevel1EnemiesSlain;
    public bool zoneCLevel2Beat;
    public int zoneCLevel2EnemiesSlain;
    public bool zoneCLevel3Beat;
    public int zoneCLevel3EnemiesSlain;
    public bool zoneCLevel4Beat;
    public int zoneCLevel4EnemiesSlain;
    public bool zoneCBossBeat;
    public int zoneCLevelBeatCount;

    public bool zoneDLevel1Beat;
    public int zoneDLevel1EnemiesSlain;
    public bool zoneDLevel2Beat;
    public int zoneDLevel2EnemiesSlain;
    public bool zoneDLevel3Beat;
    public int zoneDLevel3EnemiesSlain;
    public bool zoneDLevel4Beat;
    public int zoneDLevel4EnemiesSlain;
    public bool zoneDBossBeat;
    public int zoneDLevelBeatCount;
}