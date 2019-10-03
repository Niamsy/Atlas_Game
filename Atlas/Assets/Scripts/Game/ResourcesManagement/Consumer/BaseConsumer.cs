namespace Game.ResourcesManagement.Consumer
{
    public class BaseConsumer : IConsumer
    {
        protected override void Awake() 
        {
            base.Awake();
            InvokeRepeating("ConsumeResource", 0f, ConsumptionRate.TickRate);
        }
    }
}
