using UnityEngine;
using Events;

namespace InputManagement
{
    public abstract class AInput<T> : ScriptableObject, IEventListener
    {
        public abstract void Set();
        public abstract T Get();
        protected bool _isSet = false;

        public bool isSet { get { return _isSet; } private set { } }

        private static bool init = false;

        public GameEvent _Event;

        public void OnEnable()
        {
            _Event.RegisterListener(this);
        }

        public void OnDisable()
        {
            _Event.UnregisterListener(this);
        }

        public void OnEventRaised()
        {
            if (!isSet)
            {
                Set();
            }
        }
    }
}