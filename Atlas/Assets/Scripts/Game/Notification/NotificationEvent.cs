using System.Collections.Generic;
using UnityEngine;

namespace Game.Notification
{
    [CreateAssetMenu(menuName = "Notification/Notification Event")]
    public class NotificationEvent : ScriptableObject
    {
        private readonly List<INotificationListener> _listeners = new List<INotificationListener>();
        
        public void Raise(Notification notification) {
            for (var i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised(notification);
            }
        }

        public void RegisterListener(INotificationListener listener)
        {
            if (!_listeners.Contains(listener))
                _listeners.Add(listener);
        }

        public void UnregisterListener(INotificationListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}
