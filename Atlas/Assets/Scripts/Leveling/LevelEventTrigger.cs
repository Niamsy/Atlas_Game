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

        private void OnDestroy()
        {
            _LevelingEvent.Raise(_XPQuantity, _Factor);
        }
    }
}