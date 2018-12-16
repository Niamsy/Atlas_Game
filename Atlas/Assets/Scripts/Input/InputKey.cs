using UnityEngine;

namespace InputManagement
{
    [CreateAssetMenu(menuName="Inputs/Key")]
    public class InputKey : AInputKey<bool>
    {
        [Header("Keyboard")]
        public InputString Default;
        public InputString User;

        [Header("Joystick")]
        public InputString Button;

        override public void Set() {
            InputString key = User.Value != "" ? User : Default;

            cInput.SetKey(name, key.Value, Button.Value);
        }

        override public bool Get() {
            return cInput.GetKey(name);
        }

        override public bool GetDown() {
            return cInput.GetKeyDown(name);
        }

        override public bool GetUp() {
            return cInput.GetKeyUp(name);
        }
    }
}