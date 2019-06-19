using System.Collections;
using Game.Map;
using SceneManagement;
using UnityEngine;

namespace Game.SavingSystem
{
    public class AutoSaveManager : MonoBehaviour
    {
        private bool _finishedLoading = false;
        /// <summary>
        /// Delay between auto save in seconds
        /// </summary>
        public int DelayBetweenSave = 60;
        private Coroutine _autoSaveCoroutine = null;
        private bool _saving = false;
        public bool Saving => _saving;

        private void Awake()
        {
            SceneLoader.OnSceneLoading += Enable;
        }

        private void OnDestroy()
        {
            SceneLoader.OnSceneLoading -= Enable;
        }

        private void Enable(int index)
        {
            SceneLoader.OnSceneLoading -= Enable;
            _finishedLoading = true;
            OnEnable();    
        }
        
        private void OnEnable()
        {
            if (!_finishedLoading)
                return;
            if (_autoSaveCoroutine != null)
                StopCoroutine(_autoSaveCoroutine);
            _autoSaveCoroutine = StartCoroutine(AutoSave());
        }

        private void OnDisable()
        {
            StopCoroutine(_autoSaveCoroutine);
        }

        private IEnumerator AutoSave()
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(DelayBetweenSave);
                _saving = true;
                Debug.Log("Auto Save Start");
                yield return new WaitForSecondsRealtime(0.5f);
                LevelManager.Instance.SaveData();
                yield return new WaitForSecondsRealtime(0.5f);
                Debug.Log("Auto Save End");
                _saving = false;
            }
        }
    }
}
