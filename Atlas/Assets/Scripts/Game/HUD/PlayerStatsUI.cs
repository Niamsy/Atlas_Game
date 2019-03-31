﻿using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    public Image currentHealthBar;
    public Image currentHungerBar;
    public Image currentSleepBar;
    public Image currentOxygenBar;
    public Image currentStaminaBar;
    public Image currentWaterBar;

    public PlayerStats stats;
    
    private void Update()
    {
        updateBar();
    }
    private void updateBar()
    {
        float ratio = stats.playerHealth.getCurrent() / stats.playerHealth.getMax();

        currentHealthBar.rectTransform.localScale = new Vector3(ratio, 1, 1);

        ratio = stats.playerHunger.getCurrent() / stats.playerHunger.getMax();
        currentHungerBar.rectTransform.localScale = new Vector3(ratio, 1, 1);

        ratio = stats.playerSleep.getCurrent() / stats.playerSleep.getMax();
        currentSleepBar.rectTransform.localScale = new Vector3(ratio, 1, 1);

        ratio = stats.playerOxygen.getCurrent() / stats.playerOxygen.getMax();
        currentOxygenBar.rectTransform.localScale = new Vector3(ratio, 1, 1);

        ratio = stats.playerStamina.getCurrent() / stats.playerStamina.getMax();
        currentStaminaBar.rectTransform.localScale = new Vector3(ratio, 1, 1);

        ratio = stats.playerHydration.getCurrent() / stats.playerHydration.getMax();
        currentWaterBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
    }
    
}
