using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Plants
{
    public abstract class GrowerSystem : ScriptableObject
    {
        protected Plant plant;

        public abstract void Grow();
    }
}
