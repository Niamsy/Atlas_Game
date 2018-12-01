using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Game;
using Game.Inventory;

public class GameControl : MonoBehaviour {

    public static GameControl control;
    public GameData gameData;
    
    public bool LoadData = false;
    public bool SaveData = false;

    void OnEnable()
    {
        if (LoadData)
            Load();
    }

    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        } 
        else if (control != this)
        {
            Destroy(gameObject);
        }
    }

    void OnDisable()
    {
        Debug.Log("Save game data");

        if (SaveData)
            Save();
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gameInfo.dat");

        bf.Serialize(file, gameData);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/gameInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gameInfo.dat", FileMode.Open);

            gameData = (GameData)bf.Deserialize(file);
            file.Close();
        }
    }
}
