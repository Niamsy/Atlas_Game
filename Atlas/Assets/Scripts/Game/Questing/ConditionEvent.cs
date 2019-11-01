using System.Collections.Generic;
using System.Linq;
using Game.Item;
using UnityEngine;

namespace Game.Questing
{
    [CreateAssetMenu(menuName = "Questing/Condition Event")]
    public class ConditionEvent : ScriptableObject
    {
        private readonly List<IConditionEventListener> _listeners = new List<IConditionEventListener>();
        
        public void Raise(Condition condition, ItemAbstract itemAbstract, int count) {
            for (var i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised(condition, itemAbstract, count);
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