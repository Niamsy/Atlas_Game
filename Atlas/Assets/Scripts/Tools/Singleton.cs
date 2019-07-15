using UnityEngine;

namespace Tools
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        #region Properties
        /// <summary>
        /// Returns the instance of this singleton.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));
                    if (instance == null)
                        Debug.LogError("An instance of " + typeof(T) + " is needed in the scene, but there is none.");
                }
                return instance;
            }
        }
        #endregion

        #region Methods
        protected virtual void Awake()
        {
            if (GetType() != typeof(T))
                DestroySelf();

            if (instance == null)
            {
                instance = this as T;
                if (transform.parent == null)
                    DontDestroyOnLoad(gameObject);
            }
            else if (instance != this)
                DestroySelf();

            OnAwake();
        }

        private void DestroySelf()
        {
            if (Application.isPlaying)
                Destroy(this);
            else
                DestroyImmediate(this);
        }
        #endregion

        protected virtual void OnAwake() { }

        protected static T instance;
    }
}

