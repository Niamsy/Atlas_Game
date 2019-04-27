using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Game
{
    public class GameControl : MonoBehaviour
    {
        public static GameControl Control;
        public GameData GameData;
        public MapData MapData;

        public delegate void GameControlDelegate(GameControl gameControl);
    
        public static event GameControlDelegate BeforeSavingPlayer;
        public static event GameControlDelegate BeforeSavingData;
        public static event GameControlDelegate UponLoadingPlayerData;
        public static event GameControlDelegate UponLoadingMapData;

        public bool LoadData = false;
        public bool SaveData = false;

        private static readonly string FileExtension = ".dat";
        private static readonly string FileName = "gameInfo" + FileExtension;
        private static string FullPath() { return(Application.persistentDataPath + "/" + FileName); }
        private static string FullMapPath(int levelIndex) { return (Application.persistentDataPath + "/level_" + levelIndex + FileExtension); }

        void Awake()
        {
            if (Control == null)
            {
                DontDestroyOnLoad(gameObject);
                Control = this;
            } 
            else if (Control != this)
                Destroy(gameObject);
        }

        void OnEnable()
        {
            if (LoadData)
                LoadPlayerData();
        }
        
        void OnDestroy()
        {
            if (SaveData)
                SavePlayerData();
        }

        #region Save
        /// <summary>
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <returns>Sucess or failure</returns>
        private static bool SaveInFile(string path, object data)
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(path);
                if (File.Exists(path))
                {
                    bf.Serialize(file, data);
                    file.Close();
                    return (true);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Exception:" + e);
            }

            Debug.LogError("/!\\ Impossible to save game Data /!\\");
            return (false);
        }
        
        public void SavePlayerData()
        {
            if (BeforeSavingPlayer != null)
                BeforeSavingPlayer(this);
            SaveInFile(FullPath(), GameData);
        }

        public void SaveMapData(int sceneIndex)
        {
            if (BeforeSavingData != null)
                BeforeSavingData(this);
            SaveInFile(FullMapPath(sceneIndex), MapData);
            Debug.Log("Saved Map data " + sceneIndex);
        }
        #endregion
        
        #region Load
        /// <summary>
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <returns>Sucess or failure</returns>
        private static bool LoadFromFile<T>(string path, ref T data)
        {
            if (File.Exists(path))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(path, FileMode.Open);
                data = (T)bf.Deserialize(file);
                file.Close();
                return (true);
            }

            return (false);
        }
        
        public void LoadPlayerData()
        {
            LoadFromFile(FullPath(), ref GameData);
            if (UponLoadingPlayerData != null)
                UponLoadingPlayerData(this);
        }

        public void LoadMapData(int sceneIndex)
        {
            LoadFromFile(FullMapPath(sceneIndex), ref MapData);
            if (UponLoadingMapData != null)
                UponLoadingMapData(this);
        }
        #endregion
    }
}
