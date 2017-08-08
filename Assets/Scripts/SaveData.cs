using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveData : MonoBehaviour {

    public bool zoneALevel1Beat;
    public bool zoneALevel2Beat;
    public bool zoneALevel3Beat;
    public bool zoneALevel4Beat;
    public bool zoneABossBeat;

    public bool zoneBLevel1Beat;
    public bool zoneBLevel2Beat;
    public bool zoneBLevel3Beat;
    public bool zoneBLevel4Beat;
    public bool zoneBBossBeat;

    public bool zoneCLevel1Beat;
    public bool zoneCLevel2Beat;
    public bool zoneCLevel3Beat;
    public bool zoneCLevel4Beat;
    public bool zoneCBossBeat;

    public bool zoneDLevel1Beat;
    public bool zoneDLevel2Beat;
    public bool zoneDLevel3Beat;
    public bool zoneDLevel4Beat;
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
        data.zoneALevel1Beat = zoneALevel1Beat;
        data.zoneALevel2Beat = zoneALevel2Beat;
        data.zoneALevel3Beat = zoneALevel3Beat;
        data.zoneALevel4Beat = zoneALevel4Beat;
        data.zoneABossBeat = zoneABossBeat;

        data.zoneBLevel1Beat = zoneBLevel1Beat;
        data.zoneBLevel2Beat = zoneBLevel2Beat;
        data.zoneBLevel3Beat = zoneBLevel3Beat;
        data.zoneBLevel4Beat = zoneBLevel4Beat;
        data.zoneBBossBeat = zoneBBossBeat;

        data.zoneCLevel1Beat = zoneCLevel1Beat;
        data.zoneCLevel2Beat = zoneCLevel2Beat;
        data.zoneCLevel3Beat = zoneCLevel3Beat;
        data.zoneCLevel4Beat = zoneCLevel4Beat;
        data.zoneCBossBeat = zoneCBossBeat;

        data.zoneDLevel1Beat = zoneDLevel1Beat;
        data.zoneDLevel2Beat = zoneDLevel2Beat;
        data.zoneDLevel3Beat = zoneDLevel3Beat;
        data.zoneDLevel4Beat = zoneDLevel4Beat;
        data.zoneDBossBeat = zoneDBossBeat;

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
            file.Close();

            zoneALevel1Beat = data.zoneALevel1Beat;
            zoneALevel2Beat = data.zoneALevel2Beat;
            zoneALevel3Beat = data.zoneALevel3Beat;
            zoneALevel4Beat = data.zoneALevel4Beat;
            zoneABossBeat = data.zoneABossBeat;

            zoneBLevel1Beat = data.zoneBLevel1Beat;
            zoneBLevel2Beat = data.zoneBLevel2Beat;
            zoneBLevel3Beat = data.zoneBLevel3Beat;
            zoneBLevel4Beat = data.zoneBLevel4Beat;
            zoneBBossBeat = data.zoneBBossBeat;

            zoneCLevel1Beat = data.zoneCLevel1Beat;
            zoneCLevel2Beat = data.zoneCLevel2Beat;
            zoneCLevel3Beat = data.zoneCLevel3Beat;
            zoneCLevel4Beat = data.zoneCLevel4Beat;
            zoneCBossBeat = data.zoneCBossBeat;

            zoneDLevel1Beat = data.zoneDLevel1Beat;
            zoneDLevel2Beat = data.zoneDLevel2Beat;
            zoneDLevel3Beat = data.zoneDLevel3Beat;
            zoneDLevel4Beat = data.zoneDLevel4Beat;
            zoneDBossBeat = data.zoneDBossBeat;
        }
    }
}

[Serializable]
class PlayerData
{
    public bool zoneALevel1Beat;
    public bool zoneALevel2Beat;
    public bool zoneALevel3Beat;
    public bool zoneALevel4Beat;
    public bool zoneABossBeat;

    public bool zoneBLevel1Beat;
    public bool zoneBLevel2Beat;
    public bool zoneBLevel3Beat;
    public bool zoneBLevel4Beat;
    public bool zoneBBossBeat;

    public bool zoneCLevel1Beat;
    public bool zoneCLevel2Beat;
    public bool zoneCLevel3Beat;
    public bool zoneCLevel4Beat;
    public bool zoneCBossBeat;

    public bool zoneDLevel1Beat;
    public bool zoneDLevel2Beat;
    public bool zoneDLevel3Beat;
    public bool zoneDLevel4Beat;
    public bool zoneDBossBeat;
}
