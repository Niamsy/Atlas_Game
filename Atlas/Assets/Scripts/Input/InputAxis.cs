using UnityEngine;

namespace InputManagement
{
    [CreateAssetMenu(menuName="Inputs/Axis")]
    public class InputAxis : AInput<float>
    {
        public InputKey Negative;
        public InputKey Positive;
        public float Sensitivity = 1f;

        override public void Set() {
            if (!Negative.isSet)
                Negative.Set();
            if (!Positive.isSet)
                Positive.Set();
            cInput.SetAxis(name, Negative.name, Positive.name, Sensitivity);
            _isSet = true;
        }

        override public float Get() {
            if (!isSet)
            {
                return 0f;
            }
            return cInput.GetAxis(name);
        }
    }
}