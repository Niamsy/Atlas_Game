using UnityEngine;

namespace InputManagement
{
    public abstract class AInputKey<T> : AInput<T>
    {
        public abstract T GetDown();
        public abstract T GetUp();
    }
}