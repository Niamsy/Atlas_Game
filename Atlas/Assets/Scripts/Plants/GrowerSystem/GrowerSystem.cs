using Plants.Plant;
using UnityEngine;

namespace Plants.GrowerSystem
{
    public abstract class GrowerSystem : ScriptableObject
    {
        protected Plants.Plant.PlantModel Plant;

        public abstract void Grow();
    }
}
