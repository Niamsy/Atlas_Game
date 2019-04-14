using System.Collections;
using UnityEngine;

namespace Game.Item.Tools
{
    public class BucketBehaviour : MonoBehaviour
    {
        public enum Status
        {
            Filling,
            Watering
        }
        
        public BucketProducer Producer;
        public BucketConsumer Consumer;

        public GameObject ProducerParticle;

        public int StockSize = 500;
        
        public Status State { get; private set; } 
        
        private void Awake()
        {
            Consumer.Initialize(StockSize);
            Producer.gameObject.SetActive(false);
            Consumer.enabled = false;
            Producer.enabled = false;
        }
        
        public void SetState(Status newState)
        {
            if (newState != State)
            {
                Consumer.enabled = false;
                Producer.enabled = false;

                State = newState;

                if (State == Status.Watering)
                {
                    Producer.StockedResources[ResourcesManagement.Resource.Water].Quantity = Consumer.LinkedStock[ResourcesManagement.Resource.Water].Quantity;
                    if (Producer.StockedResources[ResourcesManagement.Resource.Water].Quantity == 0)
                        return;

                    Producer.gameObject.SetActive(true);
                    ProducerParticle.gameObject.SetActive(true);
                    
                    Producer.enabled = true;
                    Producer.Produce();
                }
                else if (State == Status.Filling)
                {
                    Consumer.LinkedStock[ResourcesManagement.Resource.Water].Quantity = Producer.StockedResources[ResourcesManagement.Resource.Water].Quantity;
                    Producer.gameObject.SetActive(false);
                    ProducerParticle.gameObject.SetActive(false);
                    Consumer.LinkedStock = Producer.StockedResources;
                    Consumer.enabled = true;
                }
            }
        }

        public void Watering()
        {
                SetState(Status.Watering);    
        }

        public void StopWatering()
        {
                SetState(Status.Filling);    
        }
    }
}
