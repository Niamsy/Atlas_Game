using UnityEngine;

namespace InputManagement
{
    public enum InputKeyStatus
    {
        Nothing = 0,
        Pressed = 1,
        Holded = 2,
        Released = 4
    }
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

        public InputKeyStatus GetStatus()
        {
            if (cInput.GetKeyDown(name))
                return (InputKeyStatus.Pressed);
            if (cInput.GetKey(name))
                return (InputKeyStatus.Holded);
            if (cInput.GetKeyUp(name))
                return (InputKeyStatus.Released);
            return (InputKeyStatus.Nothing);
        }
    }
}