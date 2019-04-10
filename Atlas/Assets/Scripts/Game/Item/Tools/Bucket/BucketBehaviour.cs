using System.Collections;
using Game.ResourcesManagement;
using Game.ResourcesManagement.Producer;
using Plants;
using UnityEngine;

namespace Game.Item.Tools
{
    public class BucketBehaviour : MonoBehaviour
    {
        public enum Status
        {
            Default,
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

        private readonly float _countdownStep = 0.1f;
        private float _countDown = 0;
        private IEnumerator WateringCoroutine()
        {
            SetState(Status.Watering);    
            while (_countDown > 0)
            {
                yield return new WaitForSeconds(_countdownStep);
                _countDown -= _countdownStep;
            }
            SetState(Status.Default);    
        }

        public void Watering()
        {
            _countDown = _countdownStep * 4f;
            if (State != Status.Watering)
                StartCoroutine(WateringCoroutine());
        }
    }
}
