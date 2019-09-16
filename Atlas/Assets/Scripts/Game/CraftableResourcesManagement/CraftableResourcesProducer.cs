using Game.DayNight;
using Game.Inventory;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Plants.Plant
{
    public class CraftableResourcesProducer : MonoBehaviour
    {
        [SerializeField]
        public List<CraftableResource> CraftableResources;
        private PlantModel _plant;

        protected virtual void Awake()
        {
            _plant = gameObject.GetComponentInParent<PlantModel>();
            _plant.OnDeath.AddListener(ProduceDeathResources);
            CalendarManager.Instance.ActualDate.OnDayChanged += ProduceResources;
        }

        protected virtual void OnDestroy()
        {
            if (_plant)
                _plant.OnDeath.RemoveListener(ProduceDeathResources);
            CalendarManager.Instance.ActualDate.OnDayChanged -= ProduceResources;
        }

        protected virtual void ProduceResources()
        {
            if (IsHarvestPeriod() && _plant.CurrentStageInt == 2)
            {
                List<PeriodToCreate> resourcesToGenerate = new List<PeriodToCreate>();

                foreach (var resources in CraftableResources)
                {
                    var harvestResources = resources.PeriodsToCreate.Where(resource => resource.Period == CraftablePeriod.HarvestPeriod);
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

        protected virtual void GenerateGameObject(ProducedResource item, PeriodToCreate resources)
        {

            var position = transform.position + Vector3.up + transform.forward.normalized;
            GameObject droppedObject = Instantiate(item.PrefabDroppedGO, position, Quaternion.identity);
            var itemStack = droppedObject.GetComponent<ItemStackBehaviour>();
            if (resources.Quantity != 1)
                itemStack.Slot.SetItem(item, Random.Range(resources.Quantity, resources.Quantity * 2));
            var rb = droppedObject.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward.normalized * 0.1f);
        }

        protected virtual bool IsHarvestPeriod()
        {
            var month = CalendarManager.Instance.ActualDate.Month;
            return _plant.PlantStatistics.HarvestPeriods[month];
        }
    }
}

