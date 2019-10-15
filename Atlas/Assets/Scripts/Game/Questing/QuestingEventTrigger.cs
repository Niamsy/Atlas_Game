using System;
using UnityEngine;

namespace Game.Questing
{
    public class QuestingEventTrigger : MonoBehaviour
    {
        public Quest quest = null;
        public QuestingEvent Event = null;
        
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
            
            Event.Raise(quest);
        }
    }
}