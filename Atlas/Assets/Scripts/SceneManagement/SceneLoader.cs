using System;
using System.Collections;
using Boo.Lang;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class SceneLoader : MonoBehaviour
    {
        #if UNITY_EDITOR
        public static System.Collections.Generic.List<string> ActualLoadedScenes;
        #endif
        [SerializeField] private SceneAsset _startUpScene;
        
        private static SceneLoader _instance = null;
        public static SceneLoader Instance
        {
            private set { _instance = value;}
            get
            {
                #if UNITY_EDITOR
                if (_instance == null)
                    Debug.LogError("Please open the ATLAS/Master scene windows");
                #endif

                return _instance;
            }
            
        }
        
        [SerializeField] private GameObject _loadingScreen;
        private float _loadingProgress;
        private Coroutine _loading;
        
        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
#if UNITY_EDITOR
            if (ActualLoadedScenes == null || ActualLoadedScenes.Count == 0)
            {
                if (ActualLoadedScenes == null)
                    Debug.LogError("Please open the ATLAS/Master scene windows");
#endif
                SceneManager.LoadScene(_startUpScene.name, LoadSceneMode.Additive);
#if UNITY_EDITOR
            }
            else
            {
                foreach (var loadedScene in ActualLoadedScenes)
                    SceneManager.LoadScene(loadedScene, LoadSceneMode.Additive);
            }
#endif
        }

        public void LoadScene(string sceneToLoad, string sceneToUnload)
        {
            if (_loading == null)
                _loading = StartCoroutine(FullReloadOfNewScene(sceneToLoad, sceneToUnload));
        }

        public float Progress = 0f;
        private IEnumerator FullReloadOfNewScene(string sceneToLoad, string sceneToUnload)
        {
            Debug.Log("LOG: " + sceneToLoad + "/" + sceneToUnload);
            Progress = 0f;
            _loadingScreen.SetActive(true);
            yield return DoAsyncOperationUntil(SceneManager.UnloadSceneAsync(sceneToUnload), 0f, 0.5f);
            yield return DoAsyncOperationUntil(SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive), 0.5f, 0.5f);
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
