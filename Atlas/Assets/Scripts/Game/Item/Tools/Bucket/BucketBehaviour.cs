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
                
                Producer.gameObject.SetActive(State == Status.Watering);
                ProducerParticle.gameObject.SetActive(State == Status.Watering);

                if (State == Status.Watering)
                {
                    Producer.StockedResources = Consumer.LinkedStock;
                    Producer.enabled = true;
                    Producer.Produce();
                }
                else if (State == Status.Filling)
                {
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
