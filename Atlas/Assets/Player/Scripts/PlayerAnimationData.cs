using System;
using UnityEngine;

namespace Player.Scripts
{
    [Serializable]
    public class PlayerAnimationData
    {
        public enum ItemAnim
        {
            none = 0,
            sow = 1,
            useBukect = 2
        }

        public ItemAnim anim;
    }
}
