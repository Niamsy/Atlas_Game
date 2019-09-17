using Game.DayNight;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Plants.Plant
{
    public class CraftableSeedsProducer : CraftableResourcesProducer
    {
        protected override void Awake()
        {
            _plant = gameObject.GetComponentInParent<PlantModel>();
            CalendarManager.Instance.ActualDate.OnDayChanged += ProduceResources;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}