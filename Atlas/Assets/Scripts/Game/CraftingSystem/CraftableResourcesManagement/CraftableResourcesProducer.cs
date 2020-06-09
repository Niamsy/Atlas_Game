using Game.DayNight;
using Game.Inventory;
using Game.Item;
using Game.Item.PlantSeed;
using System.Collections.Generic;
using System.Linq;
using Game.Map.DayNight;
using UnityEngine;
using Game.Item.Food;

namespace Plants.Plant
{
    public class CraftableResourcesProducer : MonoBehaviour
    {
        [SerializeField]
        public List<CraftableResource> CraftableResources;
        protected PlantModel _plant;

        protected virtual void Awake()
        {
            _plant = gameObject.GetComponentInParent<PlantModel>();
            _plant.OnDeath.AddListener(ProduceDeathResources);
            if (CalendarManager.Instance)
            {
                CalendarManager.Instance.ActualDate.OnDayChanged += ProduceResources;
                CalendarManager.Instance.ActualDate.OnHourChanged += ProduceResources;
            }
        }

        protected virtual void OnDestroy()
        {
            _plant.OnDeath.RemoveListener(ProduceDeathResources);
            if (CalendarManager.Instance)
            {
                CalendarManager.Instance.ActualDate.OnDayChanged -= ProduceResources;
                CalendarManager.Instance.ActualDate.OnHourChanged += ProduceResources;
            }
        }

        protected virtual void ProduceResources()
        {
            if (IsHarvestPeriod())
            {
                List<PeriodToCreate> resourcesToGenerate = new List<PeriodToCreate>();

                foreach (var resources in CraftableResources)
                {
                    var harvestResources = resources.PeriodsToCreate.Where(resource => resource.Period == CraftablePeriod.HarvestPeriod && (_plant.CurrentStageInt == _plant.LastStageInt) || (resource.harvestAllStage == true));
                    if (harvestResources.Count() > 0)
                        GenerateGameObject(resources.Resource, harvestResources.First());
                }
            }
        }

        protected virtual void ProduceDeathResources()
        {
#if ATLAS_DEBUG
            Debug.Log("DEATH");
#endif
            _plant.OnDeath.RemoveListener(ProduceDeathResources);
            var stage = _plant.CurrentStageInt;
            List<PeriodToCreate> resourcesToGenerate = new List<PeriodToCreate>();

            foreach (var resources in CraftableResources)
            {
                var harvestResources = resources.PeriodsToCreate.Where(resource => (int)resource.Period == stage);
                if (harvestResources.Count() > 0)
                    GenerateGameObject(resources.Resource, harvestResources.First());
            }
        }

        protected virtual void GenerateGameObject(ItemAbstract item, PeriodToCreate resources)
        {
            if (!(item is Food) || (item is Food && _plant.IsPollinate))
            {
                var position = transform.position + Vector3.up + transform.forward.normalized;
                GameObject droppedObject = Instantiate(item.PrefabDroppedGO, position, Quaternion.identity);
                var itemStack = droppedObject.GetComponent<ItemStackBehaviour>();
                if (resources.Quantity == 1)
                    itemStack.Slot.SetItem(item, Random.Range(resources.Quantity, resources.Quantity));
                else
                    itemStack.Slot.SetItem(item, Random.Range(resources.Quantity, resources.Quantity * 2));
                var rb = droppedObject.GetComponent<Rigidbody>();
                rb.AddForce(transform.forward.normalized * 0.1f);
            }
        }

        protected virtual bool IsHarvestPeriod()
        {
            var month = CalendarManager.Instance.ActualDate.Month;
            return _plant.PlantStatistics.HarvestPeriods[month];
        }
    }
}

