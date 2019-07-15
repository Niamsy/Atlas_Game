using System.Collections.Generic;
using Game.ResourcesManagement;
using Game.SavingSystem;
using Game.SavingSystem.Datas;
using UnityEngine;

namespace Game.Player.Stats
{
    [RequireComponent(typeof(PlayerConsumer))]
    public class PlayerStats : MapSavingBehaviour
    {
        public ResourcesStock Resources;
    
        public HealthConstraint PlayerHealth;

        protected override void Awake()
        {
            base.Awake();

            PlayerHealth = new HealthConstraint();
        }

        protected override void SavingMapData(MapData data)
        {
            if (data.PlayerResource == null)
                data.PlayerResource = new List<Stock>();
            else
                data.PlayerResource.Clear();
            data.PlayerResource.AddRange(Resources.ListOfStocks);
        }

        protected override void LoadingMapData(MapData data)
        {
            if (data.PlayerResource != null)
            {
                Resources.ListOfStocks.Clear();
                Resources.ListOfStocks.AddRange(data.PlayerResource);
            }
        }
    }
}

