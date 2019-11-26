using Game.ResourcesManagement;
using Game.ResourcesManagement.Producer;
using UnityEngine;

public class PlantProducer : IProducer
{
    [SerializeField]
    public int finalStageEnergyGiven = 1;

    [SerializeField]
    public int upgradeOxygenResourceMultiplicator = 100;

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

    public void UpdateRates(int stage)
    {
        StockedResources[Resource.Oxygen].Limit = stage * upgradeOxygenResourceMultiplicator;
    }

    private void OnDisable()
    {
        ClearAllListener();
    }
}
