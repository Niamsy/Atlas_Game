using System;
using UnityEngine;

namespace Player.Scripts
{
    [Serializable]
    public class PlayerAnimationData
    {
        public bool Enabled = true;
    
        public enum AnimationType
        {
            Trigger,
            Holded
        }
    
        public string        HashString;
        private int          _hash = -1;

        public int Hash
        {
            get
            {
                if (_hash == -1)
                    _hash = Animator.StringToHash(HashString);
                return (_hash);
                
            }
        }    
        public AnimationType Type = AnimationType.Trigger;
    }
}
