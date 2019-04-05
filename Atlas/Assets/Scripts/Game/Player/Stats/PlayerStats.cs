﻿using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public Popup popupSender;
    public StaminaConstraint playerStamina;
    public HydrationConstraint playerHydration;
    public SleepConstraint playerSleep;
    public OxygenConstraint playerOxygen;
    public HealthConstraint playerHealth;
    public HungerConstraint playerHunger;

    private void Start()
    {
        playerStamina = new StaminaConstraint();
        playerHydration = new HydrationConstraint();
        playerSleep = new SleepConstraint();
        playerOxygen = new OxygenConstraint();
        playerHealth = new HealthConstraint();
        playerHunger = new HungerConstraint();
    }

    public void Update()
    {
        playerStamina.Update(Time.deltaTime);
        
        playerHydration.Update(Time.deltaTime);
        if (playerHydration.getCurrent() <= playerHydration.getMax() / 2 && playerHydration.getCurrent() >= playerHydration.getMax() / 2 - 1)
        {
            popupSender.sendPopup("Careful Hydration under 50 %  u should provide ur body regular source of hydration");
        }
        playerSleep.Update(Time.deltaTime);
        if (playerSleep.getCurrent() <= playerSleep.getMax() / 2 && playerSleep.getCurrent() <= playerSleep.getMax() / 2 - 1)
        {
            popupSender.sendPopup("Careful Character sleep under 50 %, take a nap");
        }
        playerOxygen.Update(Time.deltaTime);
        if (playerOxygen.getCurrent() <= playerOxygen.getMax() / 2 && playerOxygen.getCurrent() <= playerOxygen.getMax() / 2 - 1)
        {
            popupSender.sendPopup("Careful Oxygen under 50 % find a air source quickly.");
        }
        playerHealth.Update(Time.deltaTime);
        if (playerHealth.getCurrent() <= playerHealth.getMax() / 2 && playerHealth.getCurrent() <= playerHealth.getMax() / 2 - 1)
        {
            popupSender.sendPopup("Careful health state is half critical, use bandage to heal yourself");
        }
        playerHunger.Update(Time.deltaTime);
        if (playerHunger.getCurrent() <= playerHunger.getMax() / 2 && playerHunger.getCurrent() <= playerHunger.getMax() / 2 - 1)
        {
            popupSender.sendPopup("Careful player Hunger is under 50% you should find something to eat soon");
        }
    }
}

