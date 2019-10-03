using UnityEngine;

namespace Game.ResourcesManagement.Producer
{
    [RequireComponent(typeof(Animator))]
    public class WateringSystem : IProducer
    {
        [SerializeField] private Animator _animator;
        private readonly int _hashEnabled = Animator.StringToHash("Enabled");

        protected override void Awake()
        {
            ProducedResources.Clear();
            ProducedResources.Add(Resource.Water);
            
            base.Awake();
        }
        
        private void Start()
        {
            InvokeRepeating("Produce", Random.Range(0f, 2f), ProductionRate.TickRate);
        }

        public override void Produce()
        {
            StockedResources.AddResources(Resource.Water, ProductionRate.ResourcePerTick);
            ShareResources();
            _animator.SetBool(_hashEnabled, AllListeners[0].LinkedConsumers.Count > 0 && (StockedResources[Resource.Water].Quantity > 0));    
        }
    }
}