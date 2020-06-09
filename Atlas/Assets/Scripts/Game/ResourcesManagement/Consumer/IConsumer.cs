using System.Collections.Generic;
using Game.ResourcesManagement.Producer;
using UnityEngine;

namespace Game.ResourcesManagement.Consumer
{
    public abstract class IConsumer : MonoBehaviour
    {
        [Header("Base Consumer Variables")]
        public List<Resource>           ResourcesToConsume;
        public Rate                     ConsumptionRate;
        public ResourcesStock           LinkedStock;

        private List<IProducer>         _linkedProducers = new List<IProducer>();
        
        #if UNITY_EDITOR
        [Header("Debug"), SerializeField]
        private bool _debugDisplay = false;
        private readonly string LayerName = "Consumer";

        private void Reset()
        {
            gameObject.layer = LayerMask.NameToLayer(LayerName);
        }
        #endif

        protected virtual void Awake()
        {
            
#if UNITY_EDITOR
            if (LayerMask.LayerToName(gameObject.layer) != LayerName)
                Debug.LogError("The gameObject " + name + "'s composant " + GetType() + " is invalid because it's on the wrong layer. Actual :" + LayerMask.LayerToName(gameObject.layer) + ". Layer needed : " + LayerName);
#endif
        }
        protected virtual void OnDestroy()
        {
            var linkedProducers = new List<IProducer>(_linkedProducers);
            foreach (var producer in linkedProducers)
                UnsubscribeToProducer(producer);
        }

        public virtual void ConsumeResource()
        {
            foreach (var resource in ResourcesToConsume)
                LinkedStock.RemoveResources(resource, ConsumptionRate.ResourcePerTick);
        }

        public void SubscribeToProducer(IProducer producer)
        {
            if (producer == null || _linkedProducers.Contains(producer))
                return;
#if UNITY_EDITOR
            if (_debugDisplay) Debug.Log(name + " subscribe " + producer.name);
#endif
            
            var didAdd = false;
            foreach (var resources in ResourcesToConsume)
                didAdd |= producer.AddConsumer(this, resources);

            if (didAdd)
                _linkedProducers.Add(producer);
        }
        
        public void UnsubscribeToProducer(IProducer producer)
        {
            if (producer == null || !_linkedProducers.Contains(producer))
                return;
            
#if UNITY_EDITOR
            if (_debugDisplay) Debug.Log(name + " unsubscribe " + producer.name);
#endif
            
            _linkedProducers.Remove(producer);
            foreach (var resources in ResourcesToConsume)
                producer.RemoveConsumer(this, resources);
        }

        public virtual void ReceiveResource(Resource resource, int quantity)
        {
            LinkedStock.AddResources(resource, quantity);
        }

        private void OnTriggerEnter(Collider other)
        {
            var prod = other.GetComponent<IProducer>();
            if (prod)
                SubscribeToProducer(prod);
        }

        private void OnTriggerExit(Collider other)
        {
            var prod = other.GetComponent<IProducer>();
            if (prod)
                UnsubscribeToProducer(prod);
        }
    }
}
