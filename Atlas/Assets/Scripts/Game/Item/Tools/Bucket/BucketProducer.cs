using Game.ResourcesManagement;
using Game.ResourcesManagement.Producer;
using UnityEngine;

namespace Game.Item.Tools.Bucket
{
    public class BucketProducer : IProducer
    {
        protected override void Awake()
        {
            ProducedResources.RemoveAll(x => true);
            ProducedResources.Add(Resource.Water);
            base.Awake();
            InvokeRepeating("Produce", Random.Range(0f, 2f), ProductionRate.TickRate);
        }

        private void OnDisable()
        {
            ClearAllListener();
        }

        public override void Produce()
        {
            ShareResources();
        }
    }
}
