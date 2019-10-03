﻿using Game.ResourcesManagement;
using Game.ResourcesManagement.Consumer;

namespace Game.Item.Tools.Bucket
{
    public class BucketConsumer : IConsumer
    {
        protected override void Awake()
        {
            ResourcesToConsume.RemoveAll(x => true);
            ResourcesToConsume.Add(Resource.Water);
        }

        private void OnDisable()
        {
            OnDestroy();
        }
    }
}
