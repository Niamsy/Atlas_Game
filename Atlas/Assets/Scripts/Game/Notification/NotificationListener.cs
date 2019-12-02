using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Notification
{
    public class NotificationListener : MonoBehaviour, INotificationListener
    {
        [Serializable]
        public class NotificationUnityEvent : UnityEvent<Notification> { }

        [Tooltip("Questing events to register to.")]
        public NotificationEvent Event;
        
        [Tooltip("Response to invoke when Event is raised.")]
        [SerializeField] private NotificationUnityEvent onRaised = null;

        public void OnEventRaised(Notification notification)
        {
            onRaised?.Invoke(notification);
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
