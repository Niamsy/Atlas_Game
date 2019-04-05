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
        playerSleep.Update(Time.deltaTime);
        playerOxygen.Update(Time.deltaTime);
        playerHealth.Update(Time.deltaTime);//Temporary Here
        playerHunger.Update(Time.deltaTime);
    }
}

