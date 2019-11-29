using UnityEngine;
using UnityEngine.Events;
using Game.ResourcesManagement;
using Game.ResourcesManagement.Consumer;
using Game.Questing;


namespace Game.Item.Tools.Bucket
{
    public class BucketConsumer : IConsumer
    {
        [SerializeField] private ConditionEvent _conditionEvent;
        [SerializeField] private Condition _raisedCondition;
        protected override void Awake()
        {
            ResourcesToConsume.RemoveAll(x => true);
            ResourcesToConsume.Add(Resource.Water);
        }

        public override void ReceiveResource(Resource resource, int quantity)
        {
            print("Je suis le bucket je recoit de l'eau");
            base.ReceiveResource(resource, quantity);
        }

        private void OnDisable()
        {
            OnDestroy();
        }
    }
}
