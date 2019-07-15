﻿using Game.HUD.Commons;
using Game.Player.Stats;
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

        private PlayerStats m_Stats;

        private void Start()
        {
            m_Stats = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerStats>();
            CurrentHealthBar.Initialize(m_Stats.PlayerHealth.GetCurrent(), 0, m_Stats.PlayerHealth.GetMax());
            var oxygenStock = m_Stats.Resources[Game.ResourcesManagement.Resource.Oxygen];
            CurrentOxygenBar.Initialize(oxygenStock.Quantity, 0, oxygenStock.Limit);
        }

        private void Update()
        {
            CurrentHealthBar.SetValue(m_Stats.PlayerHealth.GetCurrent());
            var oxygenStock = m_Stats.Resources[Game.ResourcesManagement.Resource.Oxygen];
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
