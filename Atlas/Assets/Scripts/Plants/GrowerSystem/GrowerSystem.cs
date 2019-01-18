using Plants.Plant;
using UnityEngine;

namespace Plants.GrowerSystem
{
    public abstract class GrowerSystem : ScriptableObject
    {
        protected PlantModel Plant;

        public abstract void Grow();
    }
}
