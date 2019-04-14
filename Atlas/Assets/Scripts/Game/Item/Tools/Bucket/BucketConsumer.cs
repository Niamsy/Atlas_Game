using Game.ResourcesManagement;
using Game.ResourcesManagement.Consumer;

namespace Game.Item.Tools.Bucket
{
    public class BucketConsumer : IConsumer
    {
        public void Awake()
        {
            ResourcesToConsume.RemoveAll(x => true);
            ResourcesToConsume.Add(Resource.Water);
        }

        private void OnDisable()
        {
            OnDestroy();
        }
    
        public override void ConsumeResource()
        {
        }

    }
}
