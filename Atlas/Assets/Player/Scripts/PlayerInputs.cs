using UnityEngine;

namespace InputManagement
{
    [CreateAssetMenu(menuName=("Inputs/Player Inputs"))]
    public class PlayerInputs : ScriptableObject
    {
        [Header("Axis")]
        public InputAxis VerticalAxis;
        public InputAxis HorizontalAxis;

        [Header("Keys")]
        public InputKey Sprint;
        public InputKey Jump;
        public InputKey CameraLock;
        public InputKey Pick;
        public InputKey EquippedItemUse;
        public InputKey Sow;
        public InputKey Skip;
    }
}