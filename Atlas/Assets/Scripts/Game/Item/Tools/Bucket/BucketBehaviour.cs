using Game.ResourcesManagement;
using UnityEngine;
using Game.Questing;

namespace Game.Item.Tools.Bucket
{
    [RequireComponent(typeof(ResourcesStock))]
    public class BucketBehaviour : MonoBehaviour
    {
        public BucketProducer Producer;
        public BucketConsumer Consumer;
        public ResourcesStock Stock;

        [SerializeField] public ConditionEvent _conditionEvent;
        [SerializeField] private Condition _raisedCondition;

        public GameObject ProducerParticle;

        private bool _isWatering = false;
        
        private void initQuesting()
        {
            GameObject questingMenu = GameObject.Find("/--- World Menu ---/QuestingMenu");
            if (questingMenu == null)
            {
                Debug.LogError("Unable to notify quest from bucket filling");
                return;
            }
            ConditionListing cmp = questingMenu.GetComponent<ConditionListing>();
            _conditionEvent = cmp.conditionEventRef;
            _raisedCondition = questingMenu.GetComponent<ConditionListing>().conditionsRef[(int)ConditionListing.ConditionsName.WATERING];
        }

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

        private void Start()
        {
            initQuesting();
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

            _isWatering = newState;
            Producer.gameObject.SetActive(_isWatering);
            Consumer.gameObject.SetActive(!_isWatering);
            ProducerParticle.gameObject.SetActive(_isWatering);
            if (_isWatering)
            {
                _conditionEvent.Raise(_raisedCondition, null, 1);
            }
        }
    }
}
