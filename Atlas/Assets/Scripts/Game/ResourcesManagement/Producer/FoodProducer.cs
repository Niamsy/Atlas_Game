using Game.ResourcesManagement;
using Game.ResourcesManagement.Producer;
using UnityEngine;

namespace Game.Item.Food
{
    public class FoodProducer : IProducer
    {
        protected override void Awake()
        {
            ProducedResources.RemoveAll(x => true);
            ProducedResources.Add(Resource.Satiety);
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
