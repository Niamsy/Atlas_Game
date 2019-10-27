using Boo.Lang;
using UnityEngine;

namespace Game.Questing
{
    [CreateAssetMenu(menuName = "Questing/Questing Event")]
    public class QuestingEvent : ScriptableObject
    {
        private readonly List<IQuestingEventListener> _listeners = new List<IQuestingEventListener>();
        
        public void Raise(Quest quest) {
            for (int i = _listeners.Count - 1; i >= 0; i-- )
            {
                _listeners[i].OnEventRaised(quest);
            }
        }

        public void RegisterListener(IQuestingEventListener listener)
        {
            if (!_listeners.Contains(listener))
                _listeners.Add(listener);
        }

        public void UnregisterListener(IQuestingEventListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}