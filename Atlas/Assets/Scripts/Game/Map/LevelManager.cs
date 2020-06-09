using System.Collections;
using System.Collections.Generic;
using Game.Item;
using Game.SavingSystem;
using Plants;
using SceneManagement;
using UnityEngine;

namespace Game.Map
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance { get; private set; }
        public static PlantSystem PlantsSystem => Instance != null ? Instance._plantsSystem : null;
        public static DroppedItemManager DroppedItemManager => Instance != null ? Instance._droppedItemManager : null;

        public static Grid.WorldGrid WorldGrid => Instance != null ? Instance._worldGrid : null;

        #region Variables
        public bool LoadAndSave = true;

        [SerializeField] private PlantSystem _plantsSystem = null;
        [SerializeField] private DroppedItemManager _droppedItemManager = null;
        [SerializeField] private Grid.WorldGrid _worldGrid = null;
        private SaveManager _saveManager = null;
        private int _sceneIndex = 0;
        #endregion
        
        #region Methods
        private void Awake()
        {
            Instance = this;
            _sceneIndex = gameObject.scene.buildIndex;
            _saveManager = SaveManager.Instance;
            SceneLoader.OnSceneUnloading += OnSceneUnloading;
            SceneLoader.OnSceneLoading += OnSceneLoading;
        }

        private void OnSceneLoading(int sceneIndex)
        {
            if (sceneIndex == _sceneIndex)
                LoadData();
        }

        #region Load&Save
        public void SaveData()
        {
            if (LoadAndSave)
                _saveManager.SaveMapData(_sceneIndex);
        }

        public void LoadData()
        {
            if (LoadAndSave)
                _saveManager.LoadMapData(_sceneIndex);
        }
        #endregion
        
        private void OnSceneUnloading(int sceneIndex)
        {
            if (sceneIndex == _sceneIndex)
            {
                SceneLoader.OnSceneUnloading -= OnSceneUnloading;
                SceneLoader.OnSceneLoading -= OnSceneLoading;

                
                SaveData();
                
                Instance = null;
            }
        }
        #endregion
    }
}
