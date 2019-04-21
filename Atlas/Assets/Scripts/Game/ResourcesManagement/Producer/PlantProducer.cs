using Game.ResourcesManagement;
using Game.ResourcesManagement.Producer;
using UnityEngine;

public class PlantProducer : IProducer
{
    protected override void Awake()
    {
        ProducedResources.RemoveAll(x => true);
        ProducedResources.Add(Resource.Oxygen);
        base.Awake();
        InvokeRepeating("Produce", Random.Range(0f, 2f), ProductionRate.TickRate);
    }

    public override void Produce()
    {
        StockedResources.AddResources(Resource.Oxygen, ProductionRate.ResourcePerTick);
        ShareResources();
    }

    private void OnDisable()
    {
        ClearAllListener();
    }
}
