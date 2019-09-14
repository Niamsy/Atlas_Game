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
        HarvestPeriod
    }

    [Serializable]
    public struct ResourceToCreate
    {
        [SerializeField]
        public CraftablePeriod  Period;
        public int              Quantity;
    }

    [Serializable]
    public struct CraftableResource
    {
        public GameObject                   Resource;
        public List<ResourceToCreate>       ResourcesToCreate;
    }
}

