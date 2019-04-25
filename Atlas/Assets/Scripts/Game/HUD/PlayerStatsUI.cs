using Game.HUD.Commons;
using UnityEngine;

namespace Game.HUD
{
    public class PlayerStatsUI : MonoBehaviour
    {
        public HUDJauge CurrentHealthBar;
        public HUDJauge CurrentOxygenBar;

        public HUDJauge CurrentHungerBar;
        public HUDJauge CurrentSleepBar;
        public HUDJauge CurrentStaminaBar;
        public HUDJauge CurrentWaterBar;

        public PlayerStats Stats;

        private void Start()
        {
            CurrentHealthBar.Initialize(Stats.PlayerHealth.GetCurrent(), Stats.PlayerHealth.GetMax());
            var oxygenStock = Stats._consumer.LinkedStock[Game.ResourcesManagement.Resource.Oxygen];
            CurrentOxygenBar.Initialize(oxygenStock.Quantity, oxygenStock.Limit);
        }

        private void Update()
        {
            CurrentHealthBar.SetValue(Stats.PlayerHealth.GetCurrent());
            var oxygenStock = Stats._consumer.LinkedStock[Game.ResourcesManagement.Resource.Oxygen];
            CurrentOxygenBar.SetValue(oxygenStock.Quantity);

            /*
        ratio = stats.playerHunger.getCurrent() / stats.playerHunger.getMax();
        currentHungerBar.rectTransform.localScale = new Vector3(ratio, 1, 1);

        ratio = stats.playerSleep.getCurrent() / stats.playerSleep.getMax();
        currentSleepBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        ratio = stats.playerStamina.getCurrent() / stats.playerStamina.getMax();
        currentStaminaBar.rectTransform.localScale = new Vector3(ratio, 1, 1);

        ratio = stats.playerHydration.getCurrent() / stats.playerHydration.getMax();
        currentWaterBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        */
        }
    
    }
}
