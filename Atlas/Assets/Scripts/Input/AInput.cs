using UnityEngine;

namespace InputManagement
{
    public abstract class AInput<T> : ScriptableObject
    {
        public abstract void Set();
        public abstract T Get();
        protected bool _isSet = false;

        public bool isSet { get { return _isSet; } private set { } }

        private static bool init = false;

        public void OnEnable()
        {
            if (!isSet)
            {
                Set();
            }
        }
    }
}