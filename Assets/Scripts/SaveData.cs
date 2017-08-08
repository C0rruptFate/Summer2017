using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

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
        data.zoneALevel1Beat = zoneALevel1Beat;
        data.zoneALevel1EnemiesSlain = zoneALevel1EnemiesSlain;
        data.zoneALevel2Beat = zoneALevel2Beat;
        data.zoneALevel2EnemiesSlain = zoneALevel2EnemiesSlain;
        data.zoneALevel3Beat = zoneALevel3Beat;
        data.zoneALevel3EnemiesSlain = zoneALevel3EnemiesSlain;
        data.zoneALevel4Beat = zoneALevel4Beat;
        data.zoneALevel4EnemiesSlain = zoneALevel4EnemiesSlain;
        data.zoneABossBeat = zoneABossBeat;

        data.zoneBLevel1Beat = zoneBLevel1Beat;
        data.zoneBLevel1EnemiesSlain = zoneBLevel1EnemiesSlain;
        data.zoneBLevel2Beat = zoneBLevel2Beat;
        data.zoneBLevel2EnemiesSlain = zoneBLevel2EnemiesSlain;
        data.zoneBLevel3Beat = zoneBLevel3Beat;
        data.zoneBLevel3EnemiesSlain = zoneBLevel3EnemiesSlain;
        data.zoneBLevel4Beat = zoneBLevel4Beat;
        data.zoneBLevel4EnemiesSlain = zoneBLevel4EnemiesSlain;
        data.zoneBBossBeat = zoneBBossBeat;

        data.zoneCLevel1Beat = zoneCLevel1Beat;
        data.zoneCLevel1EnemiesSlain = zoneCLevel1EnemiesSlain;
        data.zoneCLevel2Beat = zoneCLevel2Beat;
        data.zoneCLevel2EnemiesSlain = zoneCLevel2EnemiesSlain;
        data.zoneCLevel3Beat = zoneCLevel3Beat;
        data.zoneCLevel3EnemiesSlain = zoneCLevel3EnemiesSlain;
        data.zoneCLevel4Beat = zoneCLevel4Beat;
        data.zoneCLevel4EnemiesSlain = zoneCLevel4EnemiesSlain;
        data.zoneCBossBeat = zoneCBossBeat;

        data.zoneDLevel1Beat = zoneDLevel1Beat;
        data.zoneDLevel1EnemiesSlain = zoneDLevel1EnemiesSlain;
        data.zoneDLevel2Beat = zoneDLevel2Beat;
        data.zoneDLevel2EnemiesSlain = zoneDLevel2EnemiesSlain;
        data.zoneDLevel3Beat = zoneDLevel3Beat;
        data.zoneDLevel3EnemiesSlain = zoneDLevel3EnemiesSlain;
        data.zoneDLevel4Beat = zoneDLevel4Beat;
        data.zoneDLevel4EnemiesSlain = zoneDLevel4EnemiesSlain;
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
            zoneALevel1EnemiesSlain = data.zoneALevel1EnemiesSlain;
            zoneALevel2Beat = data.zoneALevel2Beat;
            zoneALevel2EnemiesSlain = data.zoneALevel2EnemiesSlain;
            zoneALevel3Beat = data.zoneALevel3Beat;
            zoneALevel3EnemiesSlain = data.zoneALevel3EnemiesSlain;
            zoneALevel4Beat = data.zoneALevel4Beat;
            zoneALevel4EnemiesSlain = data.zoneALevel4EnemiesSlain;
            zoneABossBeat = data.zoneABossBeat;

            zoneBLevel1Beat = data.zoneBLevel1Beat;
            zoneBLevel1EnemiesSlain = data.zoneBLevel1EnemiesSlain;
            zoneBLevel2Beat = data.zoneBLevel2Beat;
            zoneBLevel2EnemiesSlain = data.zoneBLevel2EnemiesSlain;
            zoneBLevel3Beat = data.zoneBLevel3Beat;
            zoneBLevel3EnemiesSlain = data.zoneBLevel3EnemiesSlain;
            zoneBLevel4Beat = data.zoneBLevel4Beat;
            zoneBLevel4EnemiesSlain = data.zoneBLevel4EnemiesSlain;
            zoneBBossBeat = data.zoneBBossBeat;

            zoneCLevel1Beat = data.zoneCLevel1Beat;
            zoneCLevel1EnemiesSlain = data.zoneCLevel1EnemiesSlain;
            zoneCLevel2Beat = data.zoneCLevel2Beat;
            zoneCLevel2EnemiesSlain = data.zoneCLevel2EnemiesSlain;
            zoneCLevel3Beat = data.zoneCLevel3Beat;
            zoneCLevel3EnemiesSlain = data.zoneCLevel3EnemiesSlain;
            zoneCLevel4Beat = data.zoneCLevel4Beat;
            zoneCLevel4EnemiesSlain = data.zoneCLevel4EnemiesSlain;
            zoneCBossBeat = data.zoneCBossBeat;

            zoneDLevel1Beat = data.zoneDLevel1Beat;
            zoneDLevel1EnemiesSlain = data.zoneDLevel1EnemiesSlain;
            zoneDLevel2Beat = data.zoneDLevel2Beat;
            zoneDLevel2EnemiesSlain = data.zoneDLevel2EnemiesSlain;
            zoneDLevel3Beat = data.zoneDLevel3Beat;
            zoneDLevel3EnemiesSlain = data.zoneDLevel3EnemiesSlain;
            zoneDLevel4Beat = data.zoneDLevel4Beat;
            zoneDLevel4EnemiesSlain = data.zoneDLevel4EnemiesSlain;
            zoneDBossBeat = data.zoneDBossBeat;
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
}
