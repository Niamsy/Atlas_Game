using System.Collections.Generic;
using UnityEngine;
using Game.Questing;

public class ConditionListing : MonoBehaviour
{
    public ConditionEvent ConditionEventRef = null;
    public List<Condition> ConditionsRef = new List<Condition>();

    public enum ConditionsName {
        CRAFT,
        PLANT,
        POINTREACH,
        USE,
        PICKUP,
        PLACE,
        WATER_PICKUP,
        WATERING
    };
}
