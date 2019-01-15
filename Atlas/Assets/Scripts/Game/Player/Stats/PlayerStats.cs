﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public StaminaConstraint playerStamina;
    public HydrationConstraint playerHydration;
    public SleepConstraint playerSleep;
    public OxygenConstraint playerOxygen;
    public HealthConstraint playerHealth;
    public HungerConstraint playerHunger;

    private void Start()
    {
        
    }

    public void Update()
    {
        playerStamina.Update(Time.deltaTime);
        playerHydration.Update(Time.deltaTime);
        playerSleep.Update(Time.deltaTime);
        playerOxygen.Update(Time.deltaTime);
        playerHealth.Update(Time.deltaTime);
        playerHunger.Update(Time.deltaTime);
    }
}
