using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Game.SavingSystem.Datas;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.SavingSystem
{
    public class SaveManager : MonoBehaviour
    {
        public static SaveManager Instance;

        [SerializeField]
        private bool _accountLoaded = false;
        [SerializeField, CanBeNull]
        private AccountData _accountData = null;
        public AccountData AccountData => _accountLoaded ? _accountData : null;
        
        [SerializeField]
        private bool _mapLoaded = false;
        [SerializeField, CanBeNull]
        private MapData _mapData = null;
        public MapData MapData => _mapLoaded ? _mapData : null;

        [SerializeField]
        private int _selectedProfil = -1;
        public ProfilData SelectedProfil => (_selectedProfil == -1 || AccountData == null) ?
                                            (null) : (AccountData.Profils[_selectedProfil]);
        
        public delegate void ProfilSaveDelegate(AccountData accountData);
        public delegate void MapSaveDelegate(MapData mapData);
    
        public static event ProfilSaveDelegate BeforeSavingAccountData;
        public static event ProfilSaveDelegate UponLoadingAccountData;

        public static event MapSaveDelegate BeforeSavingMapData;
        public static event MapSaveDelegate UponLoadingMapData;

        public InputControls InputControls { get; set; }

        private static readonly string FileExtension = ".dat";
        private static readonly string FileName = "profil" + FileExtension;
        
        private static string SaveDirectory_Path() { return(Application.persistentDataPath + "/SAVE"); }
        private static string AccountDirectory_Path(int accountID) { return(SaveDirectory_Path() + "/" + accountID); }
        private static string ProfilDirectory_Path(int profilID, int accountID) { return(AccountDirectory_Path(accountID) + "/PROFIL_" + profilID); }

        private static string AccountFile_Path(int accountID) { return(AccountDirectory_Path(accountID) + "/ACCOUNT" + FileExtension); }
        private static string MapFile_Path(int levelIndex, int profilID, int accountID) { return (ProfilDirectory_Path(profilID, accountID) + "/" + "LEVEL_" + levelIndex + FileExtension); }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                Init();
            } 
            else if (Instance != this)
                Destroy(gameObject);
        }

        private void Init()
        {
            InputControls = new InputControls();
#if UNITY_EDITOR
            if (AccountData != null)
            {
                LoadAccountDataByID(AccountData.ID);
                if (SelectedProfil != null)
                    SelectProfilToUseForSave(SelectedProfil);
            }
#endif
#if !UNITY_EDITOR
            _accountData = null;
#endif
        }
        
        #region Profil gestion


        /// <summary>
        /// </summary>
        /// <param name="profil"></param>
        public void SelectProfilToUseForSave(ProfilData profil)
        {
            if (profil != null)
            {
#if ATLAS_DEBUG
                Debug.Log("Profil: " + profil.ID + " loaded.");
#endif
                _selectedProfil = profil.ID;
            }
            else
            {
#if ATLAS_DEBUG
                Debug.Log("Removed the profil selected.");
#endif
                _selectedProfil = -1;
            }
        }

        public void RemoveAccountData()
        {
            SaveAccountData();
            SelectProfilToUseForSave(null);
            _mapLoaded = false;
            _mapData = null;
            _accountLoaded = false;
            _accountData = null;
        }

        public bool SaveAccountData()
        {
            if (AccountData == null)
                return (false);
            
            if (BeforeSavingAccountData != null)
                BeforeSavingAccountData(AccountData);
            
            return (SaveInFile(AccountFile_Path(AccountData.ID), AccountData));
        }
        
        public void LoadAccountDataByID(int id)
        {
            _accountData = GetAccountData(id);
            _accountLoaded = (_accountData != null);
            if (_accountLoaded && UponLoadingAccountData != null)
                UponLoadingAccountData(_accountData);
        }
        
        public bool ReloadAccountData()
        {
            _accountLoaded = LoadFromFile(AccountFile_Path(AccountData.ID), ref _accountData);
            if (_accountLoaded && UponLoadingAccountData != null)
                UponLoadingAccountData(_accountData);
            return (_accountLoaded);
        }
        #endregion
      
        #region Map Data
        public bool SaveMapData(int sceneIndex)
        {
            if (BeforeSavingMapData != null)
                BeforeSavingMapData(_mapData);
#if ATLAS_DEBUG
            Debug.Log("Saving map data of the scene " + sceneIndex);
#endif  
            CheckSaveDirectory(SaveDirectory_Path());
            CheckSaveDirectory(AccountDirectory_Path(AccountData.ID));
            CheckSaveDirectory(ProfilDirectory_Path(SelectedProfil.ID, AccountData.ID));
            
            return (SaveInFile(MapFile_Path(sceneIndex, SelectedProfil.ID, AccountData.ID), _mapData));
        }

        public bool LoadMapData(int sceneIndex)
        {
#if ATLAS_DEBUG
            Debug.Log("Loading map data of the scene " + sceneIndex);
#endif  
            _mapLoaded = LoadFromFile(MapFile_Path(sceneIndex, SelectedProfil.ID, AccountData.ID), ref _mapData);
            if (_mapLoaded && UponLoadingMapData != null)
                UponLoadingMapData(_mapData);
            if (_mapData == null)
            {
                _mapData = new MapData();
                _mapLoaded = true;
            }
            return (_mapLoaded);
        }
        
        /// <summary>
        /// Use this function to reset the save of a map
        /// </summary>
        /// <param name="sceneIndex"></param>
        /// <returns></returns>
        public bool ResetMapData(int sceneIndex)
        {
            return (DeleteSaveFile(MapFile_Path(sceneIndex, SelectedProfil.ID, AccountData.ID)));
        }
        #endregion
        
        #region Save&Load&Delete
        public static void CheckSaveDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
#if ATLAS_DEBUG
                Debug.Log("Directory '" + path + "' doesn't exist, creating it.");
#endif
                Directory.CreateDirectory(path);
            }
        }
        
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
                #if ATLAS_DEBUG
                Debug.Log("Save in file: " + path);
                #endif
            }
            catch (SerializationException e)
            {
#if ATLAS_DEBUG
                Debug.LogError("Serialization error on save:" + path + ".\nType " + data?.GetType() + ".\nException caught: " + e);
#endif
            }

#if ATLAS_DEBUG
            Debug.LogError("/!\\ Impossible to save Data /!\\ Path: " + path);
#endif
            return (false);
        }
        /// <summary>
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <returns>Sucess or failure</returns>
        private static bool LoadFromFile<T>(string path, ref T data)
        {
            try
            {
                if (File.Exists(path))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream file = File.Open(path, FileMode.Open);
                    data = (T)bf.Deserialize(file);
                    file.Close();
                    return (true);
                }
                else
                {
#if ATLAS_DEBUG
                    Debug.Log("File:" + path + ". Doesn't exist\n");
#endif
                }
            }
            catch (SerializationException e)
            {
#if ATLAS_DEBUG
                Debug.LogError("Serialization error on load:" + path + ".\nType " + typeof(T) + ".\nException caught: " + e);
#endif
            }
            data = default(T);
            return (false);
        }
        
        /// <summary>
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <returns>Sucess or failure</returns>
        private static bool DeleteSaveFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
                return (true);
            }
            return (false);
        }
        #endregion
               
        #region AccountData gestion
        /// <summary>
        /// !!! USED Only by self and the MasterAltasWindows !!!
        /// </summary>
        private static AccountData GetAccountData(int id)
        {
            AccountData data = null;
            string accountPath = AccountFile_Path(id);
            if (!LoadFromFile(accountPath, ref data))
            {
                //
                // Create a account save File
                //
                CheckSaveDirectory(SaveDirectory_Path());
                CheckSaveDirectory(AccountDirectory_Path(id));
#if ATLAS_DEBUG
                Debug.Log("Account ID:'" + id + "' created with sucess. Path: " + accountPath);
#endif
                data = new AccountData(id);
                data.Profils = new ProfilData[AccountData.MaxNumberOfProfils];
                for (int x = 0; x < AccountData.MaxNumberOfProfils; x++)
                    data.Profils[x] = new ProfilData(x, "");
                SaveInFile(AccountFile_Path(data.ID), data);
            }

            #if ATLAS_DEBUG
            if (data != null)
                Debug.Log("Account ID:'" + id + "' loaded with sucess. Path: " + accountPath);
            else
                Debug.LogError("Account ID:'" + id + "' couldn't be loaded or created. Path: " + accountPath);
            #endif
            
            return (data);
        }
        
        public static bool InstantiateProfilToUse(AccountData accountData, ProfilData data, string profilName)
        {
            if (data.Used)
                return (false);
            data.Name = profilName;
            data.Used = true;
            CheckSaveDirectory(ProfilDirectory_Path(data.ID, accountData.ID));

#if ATLAS_DEBUG
                Debug.Log("Account ID:'" + accountData.ID + "' instantiating the profil '" + data.ID + "'.");
#endif
            return (SaveAccountData(accountData));
        }
        
        public static bool DestroyProfile(AccountData accountData, ProfilData profilData)
        {
            if (!profilData.Used)
                return (false);
            
#if ATLAS_DEBUG
            Debug.Log("Account ID:'" + accountData.ID + "' starting to delete the profil '" + profilData.ID + "'.");
#endif
            
            var directoryPath = ProfilDirectory_Path(profilData.ID, accountData.ID);
            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, true);
#if ATLAS_DEBUG
                Debug.Log("Account ID:'" + accountData.ID + "' deleting the profil '" + profilData.ID + "' save directory.");
#endif
            }
            profilData.Used = false;
            
#if ATLAS_DEBUG
            Debug.Log("Account ID:'" + accountData.ID + "' finished to delete the profil '" + profilData.ID + "'.");
#endif
            return (SaveAccountData(accountData));
        }
        
        /// <summary>
        /// !!! USED Only by self and the MasterAltasWindows !!!
        /// </summary>
        private static bool SaveAccountData(AccountData data)
        {
            return (SaveInFile(AccountFile_Path(data.ID), data));
        }
        
        #endregion
    }
}
