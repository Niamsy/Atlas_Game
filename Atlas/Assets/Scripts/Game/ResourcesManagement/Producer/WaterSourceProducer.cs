using UnityEngine;
using UnityEngine.Experimental.Rendering.HDPipeline;

namespace Game.ResourcesManagement.Producer
{
    public class WaterSourceProducer : IProducer
    {
        private void Start()
        {
            InvokeRepeating("Produce", Random.Range(0f, 2f), ProductionRate.TickRate);
        }

        public override void Produce()
        {
            StockedResources.AddResources(Resource.Water, ProductionRate.ResourcePerTick); 
            ShareResources();
        }

    }
}