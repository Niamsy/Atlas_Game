using System;
using Game.Item;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Game.Questing
{
    public class ConditionListener : MonoBehaviour, IConditionEventListener
    {
        [Serializable]
        public class ConditionUnityEvent : UnityEvent<Condition, ItemAbstract, int> { }

        [Tooltip("Questing events to register to.")]
        public ConditionEvent Event;

        [FormerlySerializedAs("OnRaised")]
        [Tooltip("Response to invoke when Event is raised.")]
        [SerializeField] private ConditionUnityEvent onRaised = null;

        public void OnEventRaised(Condition condition, ItemAbstract itemAbstract, int count)
        {
            onRaised?.Invoke(condition, itemAbstract, count);
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