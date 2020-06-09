using Game.ResourcesManagement;
using Game.ResourcesManagement.Consumer;
using UnityEngine;

namespace Game.Player.Stats
{
    public class PlayerConsumer : IConsumer
    {

        public Resource SelectedResource;

        private void Start()
        {
            StartInvoking();
        }

        protected override void Awake()
        {
           
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
            LinkedStock.RemoveResources(this.ResourcesToConsume[0], ConsumptionRate.ResourcePerTick);
        }
    }
}