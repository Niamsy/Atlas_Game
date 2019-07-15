using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.ResourcesManagement.Producer
{
    public class OxygenProducer : IProducer
    {
        private void Start()
        {
            InvokeRepeating("Produce", Random.Range(0f, 2f), ProductionRate.TickRate);
        }

        public override void Produce()
        {
            StockedResources.AddResources(Resource.Oxygen, ProductionRate.ResourcePerTick);
            ShareResources();
        }
    }
}