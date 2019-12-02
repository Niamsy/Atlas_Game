using System;
using UnityEngine;
using UnityEngine.Events;
using Game.ResourcesManagement;
using Game.ResourcesManagement.Consumer;
using Game.Questing;


namespace Game.Item.Tools.Bucket
{
    public class BucketConsumer : IConsumer
    {   
        [Header("Bucket Consumer Variables")]
        [SerializeField] private ConditionEvent _conditionEvent;
        [SerializeField] private Condition _raisedCondition;
        [SerializeField] private ItemAbstract _item;

        protected override void Awake()
        {
            ResourcesToConsume.RemoveAll(x => true);
            ResourcesToConsume.Add(Resource.Water);
        }
        
        public override void ReceiveResource(Resource resource, int quantity)
        {
            _conditionEvent.Raise(_raisedCondition, _item, 1);
            base.ReceiveResource(resource, quantity);
        }

        private void OnDisable()
        {
            OnDestroy();
        }
    }
}
