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

        private bool isWatering = false;
        
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
            if (isWatering)
                ProducerParticle.gameObject.SetActive(Producer.StockedResources[Resource.Water].Quantity != 0);
        }

        public void SetState(bool newState)
        {
            isWatering = newState;

            Producer.gameObject.SetActive(isWatering);
            Consumer.gameObject.SetActive(!isWatering);
        }
    }
}
