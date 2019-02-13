using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Game;

public class GameControl : MonoBehaviour {

    public static GameControl control;
    public GameData gameData;

    public delegate void GameControlDelegate(GameControl gameControl);
    
    public static event GameControlDelegate BeforeSaving;
    public static event GameControlDelegate UponLoading;
    
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
        if (SaveData)
            Save();
    }

    public void Save()
    {
        if (BeforeSaving != null)
            BeforeSaving(this);
        
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
        
        if (UponLoading != null)
            UponLoading(this);
    }
}
