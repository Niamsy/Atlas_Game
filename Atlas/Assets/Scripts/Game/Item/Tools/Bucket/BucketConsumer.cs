using UnityEngine;
using UnityEngine.Events;
using Game.ResourcesManagement;
using Game.ResourcesManagement.Consumer;
using Game.Questing;


namespace Game.Item.Tools.Bucket
{
    public class BucketConsumer : IConsumer
    {   
        [SerializeField] public ConditionEvent _conditionEvent;
        [SerializeField] private Condition _raisedCondition;

        private void Start()
        {
            GameObject questingMenu = GameObject.Find("/--- World Menu ---/QuestingMenu");
            if (questingMenu == null)
            {
                Debug.LogError("Unable to notify quest from bucket filling");
                return;
            }
            ConditionListing cmp = questingMenu.GetComponent<ConditionListing>();
            _conditionEvent = cmp.conditionEventRef;
            _raisedCondition = questingMenu.GetComponent<ConditionListing>().conditionsRef[(int)ConditionListing.ConditionsName.WATER_PICKUP];
        }

        public BucketConsumer()
        {

        }

        protected override void Awake()
        {
            ResourcesToConsume.RemoveAll(x => true);
            ResourcesToConsume.Add(Resource.Water);
        }

        public override void ReceiveResource(Resource resource, int quantity)
        {
            _conditionEvent.Raise(_raisedCondition, null, 1);
            base.ReceiveResource(resource, quantity);
        }

        private void OnDisable()
        {
            OnDestroy();
        }
    }
}
