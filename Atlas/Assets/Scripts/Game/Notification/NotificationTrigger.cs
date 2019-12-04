using UnityEngine;

namespace Game.Notification
{
    public class NotificationTrigger : MonoBehaviour
    {
        public Notification notification = null;
        public NotificationEvent Event = null;
        
        public bool triggerOnStart = true;
        public bool triggerOnDestroy = false;
        public bool triggerOnce = false;

        private bool _triggered = false;

        private bool CanTrigger => !triggerOnce || (triggerOnce && !_triggered);
        public void OnDestroy()
        {
            if (!triggerOnDestroy) return;
            if (!CanTrigger) return;
            TriggerEvent();
            _triggered = true;
        }

        private void Start()
        {
            if (!triggerOnStart) return;
            if (!CanTrigger) return;
            TriggerEvent();
            _triggered = true;
        }

        public void TriggerEvent()
        {
            Event.Raise(notification);
        }
    }
}
