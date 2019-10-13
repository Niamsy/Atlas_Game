using Game.Map.DayNight;


namespace Plants.Plant
{
    public class CraftableSeedsProducer : CraftableResourcesProducer
    {
        protected override void Awake()
        {
            _plant = gameObject.GetComponentInParent<PlantModel>();
            CalendarManager.Instance.ActualDate.OnDayChanged += ProduceResources;
        }
    }
}