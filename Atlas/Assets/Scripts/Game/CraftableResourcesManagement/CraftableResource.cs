using Game.Item;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Plants.Plant
{
    [Serializable]
    public enum CraftablePeriod
    {
        DeathStageOne = 0,
        DeathStageTwo,
        DeathStageThree,
        DeathStageFour,
        DeathStageFive,
        HarvestPeriod
    }

    [Serializable]
    public struct PeriodToCreate
    {
        [SerializeField]
        public CraftablePeriod  Period;
        public int              Quantity;
        public bool             harvestAllStage;
    }

    [Serializable]
    public struct CraftableResource
    {
        public ItemAbstract             Resource;
        public List<PeriodToCreate>     PeriodsToCreate;
    }
}

