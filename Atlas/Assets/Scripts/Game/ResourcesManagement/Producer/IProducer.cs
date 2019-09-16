using System.Collections.Generic;
using Game.ResourcesManagement.Consumer;
using UnityEngine;

namespace Game.ResourcesManagement.Producer
{
    public abstract class IProducer : MonoBehaviour
    {
        protected class ConsumerListener
        {
            public Resource Resource;
            public List<IConsumer> LinkedConsumers;

            public ConsumerListener(Resource resource)
            {
                Resource = resource;
                LinkedConsumers = new List<IConsumer>();
            }
        }

        public Rate ProductionRate;
        public int ResourceGivenPerTick = 1;
        public ResourcesStock StockedResources;

        public List<Resource> ProducedResources;

        protected readonly List<ConsumerListener> AllListeners = new List<ConsumerListener>();

#if UNITY_EDITOR
        [Header("Debug"), SerializeField] private bool _debugDisplay = false;

        private readonly string LayerName = "Producer";
        private void Reset()
        {
            gameObject.layer = LayerMask.NameToLayer(LayerName);
        }
#endif

        public abstract void Produce();

        protected virtual void Awake()
        {
#if UNITY_EDITOR
            if (LayerMask.LayerToName(gameObject.layer) != LayerName)
                Debug.LogError("The gameObject " + name + "'s composant " + GetType() +
                               " is invalid because it's on the wrong layer. Actual :" +
                               LayerMask.LayerToName(gameObject.layer) + ". Layer needed : " + LayerName);
#endif
            foreach (var res in ProducedResources)
                AllListeners.Add(new ConsumerListener(res));
        }

        public bool AddConsumer(IConsumer consumer, Resource resource)
        {
            if (consumer == null)
                return false;

            var listener = AllListeners.Find(oneListener => oneListener.Resource == resource);

            if (listener != null && !listener.LinkedConsumers.Contains(consumer))
            {
                listener.LinkedConsumers.Add(consumer);
                return (true);
            }
            return (false);
        }

        public void RemoveConsumer(IConsumer consumer, Resource resource)
        {
            if (consumer == null)
                return;
            
            var listener = AllListeners.Find(oneListener => oneListener.Resource == resource);
            
            if (listener != null && listener.LinkedConsumers.Contains(consumer))
                listener.LinkedConsumers.Remove(consumer);
        }

        public void ClearAllListener()
        {
            foreach (var listeners in AllListeners)
            {
                var cpyList = new List<IConsumer>(listeners.LinkedConsumers);
                foreach (var listener in cpyList)
                    listener.UnsubscribeToProducer(this);
            }
        }
        
        protected void ShareResources()
        {
            foreach (var resourceListeners in AllListeners)
            {
                foreach (var listener in resourceListeners.LinkedConsumers)
                {
                    if (listener == null)
                        continue;

                    var resourcesRemoved = StockedResources.RemoveResources(resourceListeners.Resource, ResourceGivenPerTick);
                    if (resourcesRemoved > 0)
                    {
                        #if UNITY_EDITOR
                        if (_debugDisplay) Debug.Log(name + " give to " + listener.name + ": " + resourcesRemoved);
                        #endif
                        listener.ReceiveResource(resourceListeners.Resource, resourcesRemoved);
                    }
                    else
                        break;
                }
            }
        }
    }
}
