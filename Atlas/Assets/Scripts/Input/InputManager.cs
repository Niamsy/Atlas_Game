using UnityEngine;

namespace InputManagement
{
    public class InputManager : MonoBehaviour
    {
        public AtlasEvents.Event _AxisSetEvent;
        public AtlasEvents.Event _KeySetEvent;

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