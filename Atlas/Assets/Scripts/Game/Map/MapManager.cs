using System.Collections.Generic;
using Game.Item;
using Plants;
using SceneManagement;
using UnityEngine;

namespace Game.Map
{
    public class MapManager : MonoBehaviour
    {
        public static MapManager Instance { get; private set; }
        public static PlantSystem PlantsSystem => Instance != null ? Instance._plantsSystem : null;
        public static DroppedItemManager DroppedItemManager => Instance != null ? Instance._droppedItemManager : null;
        
        #region Variables
        [SerializeField] private PlantSystem _plantsSystem;
        [SerializeField] private DroppedItemManager _droppedItemManager;
        
        private int _sceneIndex;
        #endregion
        
        #region Methods
        private void Awake()
        {
            Instance = this;
            _sceneIndex = gameObject.scene.buildIndex;
            SceneLoader.OnSceneUnloading += OnSceneUnloading;
            SceneLoader.OnSceneLoading += OnSceneLoading;
        }

        private void OnSceneLoading(int sceneIndex)
        {
            if (sceneIndex == _sceneIndex)
                GameControl.Control.LoadMapData(_sceneIndex);
        }
        
        private void OnSceneUnloading(int sceneIndex)
        {
            if (sceneIndex == _sceneIndex)
            {
                SceneLoader.OnSceneUnloading -= OnSceneUnloading;
                SceneLoader.OnSceneLoading -= OnSceneLoading;
                GameControl.Control.SaveMapData(_sceneIndex);
                Instance = null;
            }
        }
        #endregion
    }
}
