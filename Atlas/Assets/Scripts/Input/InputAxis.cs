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
                Negative.Set();
                Positive.Set();
            cInput.SetAxis(name, Negative.name, Positive.name, Sensitivity);
        }

        override public float Get() {
            return cInput.GetAxis(name);
        }
    }
}