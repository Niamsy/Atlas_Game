using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public class SceneLoader : MonoBehaviour
    {
        #if UNITY_EDITOR
        public static System.Collections.Generic.List<string> ActualLoadedScenes;
        public static int ActiveLoadedScenes;
        #endif
        [SerializeField] private int _startUpSceneIndex = 1;
        
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

        public delegate void SceneLoadingEvent(int sceneIndex);

        public static event SceneLoadingEvent OnSceneLoading;
        public static event SceneLoadingEvent OnSceneUnloading;
        
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

        private IEnumerator Start()
        {
            #if UNITY_EDITOR
            if (ActualLoadedScenes == null || ActualLoadedScenes.Count == 0)
            {
                if (ActualLoadedScenes == null)
                    Debug.LogError("Please open the ATLAS/Master scene windows");
            #endif
                SceneManager.LoadScene(_startUpSceneIndex, LoadSceneMode.Additive);
            #if UNITY_EDITOR
            }
            else
            {
                foreach (var loadedScene in ActualLoadedScenes)
                    SceneManager.LoadScene(loadedScene, LoadSceneMode.Additive);

                _startUpSceneIndex = ActiveLoadedScenes;
            }
            #endif
            yield return null;
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(_startUpSceneIndex));
            if (OnSceneLoading != null)
                OnSceneLoading(_startUpSceneIndex);
        }

        public void LoadScene(int sceneToLoad, int sceneToUnload)
        {
            if (_loading == null)
                _loading = StartCoroutine(FullReloadOfNewScene(sceneToLoad, sceneToUnload));
        }

        public float Progress = 0f;
        private IEnumerator FullReloadOfNewScene(int sceneToLoad, int sceneToUnload)
        {
            Progress = 0f;
            _loadingScreen.SetActive(true);
            if (OnSceneUnloading != null)
                OnSceneUnloading(sceneToUnload);
            yield return DoAsyncOperationUntil(SceneManager.UnloadSceneAsync(sceneToUnload), 0f, 0.5f);
            yield return DoAsyncOperationUntil(SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive), 0.5f, 0.5f);
            yield return null;
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneToLoad));
            if (OnSceneLoading != null)
                OnSceneLoading(sceneToLoad);
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

        public void QuitTheGame()
        {
            for (int x = 0; x < SceneManager.sceneCount; x++)
            {
                if (OnSceneUnloading != null)
                    OnSceneUnloading(SceneManager.GetSceneAt(x).buildIndex);
            }

            Application.Quit();
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }

}
