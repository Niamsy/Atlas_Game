using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Game.SavingSystem.Datas;
using UnityEngine;

namespace Game.SavingSystem
{
    public class SaveManager : MonoBehaviour
    {
        public static SaveManager Instance;

        [SerializeField]
        private AccountData _accountData = null;
        public AccountData AccountData => _accountData;
        [SerializeField]
        private MapData _mapData = null;
        public MapData MapData => _mapData;

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
                if (AccountData == null)
                    Debug.LogError("Couldn't Load the Account in the memory.");
                else if (SelectedProfil != null)
                    SelectProfilToUseForSave(SelectedProfil);
                else
                    Debug.Log("Profil: " + SelectedProfil + " loaded.");
            }
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
        }
        
        public bool ReloadAccountData()
        {
            bool ret = LoadFromFile(AccountFile_Path(AccountData.ID), ref _accountData);
            if (ret && UponLoadingAccountData != null)
                UponLoadingAccountData(AccountData);
            return (ret);
        }
        #endregion
      
        #region Map Data
        public bool SaveMapData(int sceneIndex)
        {
            if (BeforeSavingMapData != null)
                BeforeSavingMapData(MapData);
            
            return (SaveInFile(MapFile_Path(sceneIndex, SelectedProfil.ID, AccountData.ID), MapData));
        }

        public bool LoadMapData(int sceneIndex)
        {
            bool ret = LoadFromFile(MapFile_Path(sceneIndex, SelectedProfil.ID, AccountData.ID), ref _mapData);
            if (!ret)
                _mapData = new MapData();
            else if (UponLoadingMapData != null)
                UponLoadingMapData(MapData);
            return (ret);
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
            catch (Exception e)
            {
                Debug.LogError("Exception:" + e);
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
            if (File.Exists(path))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(path, FileMode.Open);
                data = (T)bf.Deserialize(file);
                file.Close();
                return (true);
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
