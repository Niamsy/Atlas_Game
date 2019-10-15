using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Questing
{
    public class QuestingListener : MonoBehaviour, IQuestingEventListener
    {
        [Serializable]
        public class QuestingUnityEvent : UnityEvent<Quest> { }

        [Tooltip("Questing events to register to.")]
        public QuestingEvent Event;

        [Tooltip("Response to invoke when Event is raised.")]
        [SerializeField] private QuestingUnityEvent OnRaised = null;

        public void OnEventRaised(Quest quest)
        {
            OnRaised?.Invoke(quest);
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