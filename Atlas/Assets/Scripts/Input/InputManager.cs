using UnityEngine;
using Events;

namespace InputManagement
{
    public class InputManager : MonoBehaviour
    {
        public GameEvent _AxisSetEvent;
        public GameEvent _KeySetEvent;

        public bool _ShouldResetSettings = false;

        private void Awake()
        {
            cInput.Init();
            if (_ShouldResetSettings)
                cInput.Clear();
        }

        private void Start()
        {
            _KeySetEvent.Raise();
            _AxisSetEvent.Raise();   
        }
    }
}