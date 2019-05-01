using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Leveling
{
    public class LevelEventTrigger : MonoBehaviour
    {
        public LevelingEvent _LevelingEvent;
        public int _XPQuantity = 0;
        public int _Factor = 1;
        public bool _TriggerOnStart = false;
        public bool _TriggerOnDestroy = true;

        private void OnDestroy()
        {
            if (_TriggerOnDestroy)
            {
                Trigger(_XPQuantity);
            }
        }

        private void Start()
        {
            if (_TriggerOnStart)
            {
                Trigger(_XPQuantity);
            }
        }

        public void Trigger(int quantity)
        {
            _LevelingEvent.Raise(quantity, _Factor);
        }
    }
}