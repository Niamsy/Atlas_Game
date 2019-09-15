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
            CalendarManager.Instance.ActualDate.OnDayChanged += ProduceResources;
        }

        protected virtual void OnDestroy()
        {
            ProduceDeathResources();
            CalendarManager.Instance.ActualDate.OnDayChanged -= ProduceResources;
        }

        protected virtual void ProduceResources()
        {
            if (IsHarvestPeriod() && _plant.CurrentStageInt == 2)
            {
                List<ResourceToCreate> resourcesToGenerate = new List<ResourceToCreate>();

                foreach (var resources in CraftableResources)
                {
                    var harvestResources = resources.ResourcesToCreate.Where(resource => resource.Period == CraftablePeriod.HarvestPeriod);
                    if (harvestResources.Count() > 0)
                    {
                        GenerateGameObject(resources.Resource, harvestResources.First());
                    }
                }
            }
        }

        protected virtual void ProduceDeathResources()
        {
            var stage = _plant.CurrentStageInt;
            List<ResourceToCreate> resourcesToGenerate = new List<ResourceToCreate>();

            foreach (var resources in CraftableResources)
            {
                var harvestResources = resources.ResourcesToCreate.Where(resource => (int)resource.Period == stage);
                if (harvestResources.Count() > 0)
                {
                    GenerateGameObject(resources.Resource, harvestResources.First());
                }
            }
        }

        protected virtual void GenerateGameObject(GameObject obj, ResourceToCreate resources)
        {

            var position = transform.position + Vector3.up + transform.forward.normalized;
            GameObject droppedObject = Instantiate(obj, position, Quaternion.identity);
            var itemStack = droppedObject.GetComponent<ItemStackBehaviour>();
            itemStack.Slot.ModifyQuantity(resources.Quantity);
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

