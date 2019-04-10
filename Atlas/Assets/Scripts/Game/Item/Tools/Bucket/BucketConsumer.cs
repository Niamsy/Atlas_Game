using Game.ResourcesManagement;
using Game.ResourcesManagement.Consumer;

public class BucketConsumer : IConsumer
{
    public void Initialize(int stockSize)
    {
        ResourcesToConsume.RemoveAll(x => true);
        ResourcesToConsume.Add(Resource.Water);
        LinkedStock[Resource.Water].Limit = stockSize;
    }

    public override void ConsumeResource()
    {
        throw new System.NotImplementedException();
    }

}
