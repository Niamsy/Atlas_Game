using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class SceneLoader : MonoBehaviour
    {
        private const string SceneName = "Base Managers";
        
        private static SceneLoader _instance = null;
        public static SceneLoader Instance
        {
            private set { _instance = value;}
            get
            {
                if (_instance == null)
                    return CreateAndReturnSceneLoader();

                return _instance;
            }
            
        }
        
        [SerializeField] private GameObject _loadingScreen;
        private float _loadingProgress;
        private string _activeScene;
        private Coroutine _loading;

        public static SceneLoader CreateAndReturnSceneLoader()
        {
            #if UNITY_EDITOR
            Debug.LogException(new Exception("No SceneLoader found please add the 'Base Managers' Scene, Solve this ASAP"));
            #else
            SceneManager.LoadScene(SceneName);
            var scene = SceneManager.GetSceneByName(SceneName);
            var gameObjects = scene.GetRootGameObjects();
                    
            foreach (var gameO in gameObjects)
            {
                _instance = gameO.GetComponentInChildren<SceneLoader>();
                if (_instance != null)
                    return (_instance);
            }
            Debug.LogException(new Exception("No SceneLoader found in the new scene 'Base Managers', Solve this ASAP"));
            #endif
            return (_instance);
        }
        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            _activeScene = SceneManager.GetActiveScene().name;
        }

        public void LoadScene(string sceneToLoad)
        {
            if (_loading == null)
                _loading = StartCoroutine(FullReloadOfNewScene(sceneToLoad));
        }

        public float Progress = 0f;
        private IEnumerator FullReloadOfNewScene(string sceneToLoad)
        {
            Progress = 0f;
            _loadingScreen.SetActive(true);
            yield return DoAsyncOperationUntil(SceneManager.UnloadSceneAsync(_activeScene), 0f, 0.5f);
            yield return DoAsyncOperationUntil(SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive), 0.5f, 0.5f);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneToLoad));
            _loadingScreen.SetActive(false);
            _loading = null;
        }

        private IEnumerator DoAsyncOperationUntil(AsyncOperation operation, float startProgress, float percentageOfTotalProgress)
        {
            while (!operation.isDone)
            {
                yield return null;
                Progress = startProgress + operation.progress * percentageOfTotalProgress;
            }
        }
    }

}
