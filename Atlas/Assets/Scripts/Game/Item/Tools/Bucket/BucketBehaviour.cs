using Game.ResourcesManagement;
using UnityEngine;

namespace Game.Item.Tools.Bucket
{
    [RequireComponent(typeof(ResourcesStock))]
    public class BucketBehaviour : MonoBehaviour
    {
        public BucketProducer Producer;
        public BucketConsumer Consumer;
        public ResourcesStock Stock;
        
        public GameObject ProducerParticle;

        private bool _isWatering = false;
        
        private void Awake()
        {
            Producer.gameObject.SetActive(false);
            Consumer.gameObject.SetActive(true);
            Consumer.LinkedStock = Stock;
            Producer.StockedResources = Stock;

            #if UNITY_EDITOR
            if (Stock[Resource.Water] == null)
                Debug.LogError("Bucket Consumer & Producers doesn't share the same Stock. Repair that");
            #endif
        }

        private void Update()
        {
            if (_isWatering && Producer.StockedResources[Resource.Water].Quantity == 0)
                SetState(false);
        }

        public void SetState(bool newState)
        {
            if (newState == _isWatering)
                return;

            print("I am watering : " + newState);
            _isWatering = newState;

            Producer.gameObject.SetActive(_isWatering);
            Consumer.gameObject.SetActive(!_isWatering);
            ProducerParticle.gameObject.SetActive(_isWatering);
        }
    }
}
