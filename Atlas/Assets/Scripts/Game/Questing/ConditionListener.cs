using System;
using Game.Item;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Questing
{
    public class ConditionListener : MonoBehaviour, IConditionEventListener
    {
        [Serializable]
        public class ConditionUnityEvent : UnityEvent<Condition, ItemAbstract, int> { }

        [Tooltip("Questing events to register to.")]
        public ConditionEvent Event;

        [Tooltip("Response to invoke when Event is raised.")]
        [SerializeField] private ConditionUnityEvent OnRaised = null;

        public void OnEventRaised(Condition condition, ItemAbstract itemAbstract, int count)
        {
            OnRaised?.Invoke(condition, itemAbstract, count);
        }

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }
    }
}