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
        public InputKey Crouch;
        public InputKey Prone;
        public InputKey CameraLock;
        public InputKey Pick;
        public InputKey NextStage;
        public InputKey GiveWater;
        public InputKey ConsumeWater;
    }
}