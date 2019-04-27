using SceneManagement;
using UnityEngine;

namespace Game
{
    public class MapManager : MonoBehaviour
    {
        public static MapManager Instance { get; private set; }
        
        private int _sceneIndex;
        
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
    }
}
