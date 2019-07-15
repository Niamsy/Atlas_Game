using Game.ResourcesManagement;
using Game.ResourcesManagement.Consumer;
using UnityEngine;

namespace Game.Player.Stats
{
    public class PlayerConsumer : IConsumer
    {
        private void Start()
        {
            StartInvoking();
        }

        protected override void Awake()
        {
            base.Awake();
            ResourcesToConsume.RemoveAll(x => true);
            ResourcesToConsume.Add(Resource.Oxygen);
        }

        public void StartInvoking()
        {
            InvokeRepeating("ConsumeResource", Random.Range(1f, 3f), ConsumptionRate.TickRate);
        }

        private void OnDisable()
        {
            OnDestroy();
        }

        public override void ConsumeResource()
        {
            LinkedStock.RemoveResources(Resource.Oxygen, ConsumptionRate.ResourcePerTick);
        }
    }
}