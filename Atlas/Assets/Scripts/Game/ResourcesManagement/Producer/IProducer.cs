using System.Collections.Generic;
using Game.ResourcesManagement.Consumer;
using UnityEngine;

namespace Game.ResourcesManagement.Producer
{
    public abstract class IProducer : MonoBehaviour
    {
        private class ConsumerListener
        {
            public Resource Resource;
            public List<IConsumer> LinkedConsumers;

            public ConsumerListener(Resource resource)
            {
                Resource = resource;
                LinkedConsumers = new List<IConsumer>();
            }
        }

        public Rate                     ProductionRate;
        public int                      ResourceGivenPerTick = 1;
        public ResourcesStock           StockedResources;

        public List<Resource> ProducedResources;
        
        private List<ConsumerListener> _allListeners = new List<ConsumerListener>();

        public abstract void Produce();
        
        private void Awake()
        {
            foreach (var res in ProducedResources)
                _allListeners.Add(new ConsumerListener(res));
        }
        
        public bool AddConsumer(IConsumer consumer, Resource resource)
        {
            if (consumer == null)
                return false;
            
            var listener = _allListeners.Find(oneListener => oneListener.Resource == resource);
            
            if (listener != null && !listener.LinkedConsumers.Contains(consumer))
                listener.LinkedConsumers.Add(consumer);
            return (true);
        }
        
        public void RemoveConsumer(IConsumer consumer, Resource resource)
        {
            if (consumer == null)
                return;
            
            var listener = _allListeners.Find(oneListener => oneListener.Resource == resource);
            
            if (listener != null && listener.LinkedConsumers.Contains(consumer))
                listener.LinkedConsumers.Remove(consumer);
        }

        protected void ShareResources()
        {
            foreach (var resourceListeners in _allListeners)
            {
                foreach (var listener in resourceListeners.LinkedConsumers)
                {
                    if (listener == null)
                        continue;

                    var resourcesRemoved = StockedResources.RemoveResources(resourceListeners.Resource, ResourceGivenPerTick);
                    if (resourcesRemoved > 0)
                        listener.ReceiveResource(resourceListeners.Resource, resourcesRemoved);
                    else
                        break;
                }
            }
        }

    }
}
