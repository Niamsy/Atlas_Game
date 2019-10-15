using Boo.Lang;
using UnityEngine;

namespace Game.Questing
{
    [CreateAssetMenu(menuName = "Questing/ConditionEvent")]
    public class ConditionEvent : ScriptableObject
    {
        private readonly List<IConditionEventListener> _listeners = new List<IConditionEventListener>();
        
        public void Raise(Condition condition) {
            for (var i = 0; i < _listeners.Count - 1; i--)
            {
                _listeners[i].OnEventRaised(condition);
            }
        }

        public void RegisterListener(IConditionEventListener listener)
        {
            if (!_listeners.Contains(listener))
                _listeners.Add(listener);
        }

        public void UnregisterListener(IConditionEventListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}