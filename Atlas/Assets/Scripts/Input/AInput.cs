using UnityEngine;

namespace InputManagement
{
    public abstract class AInput<T> : ScriptableObject, IEventListener
    {
        public abstract void Set();
        public abstract T Get();

        public AtlasEvents.Event _Event;

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
            Set();
        }
    }
}