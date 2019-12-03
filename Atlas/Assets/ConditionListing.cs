using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Questing;

public class ConditionListing : MonoBehaviour
{
    [SerializeField] public ConditionEvent conditionEventRef;
    [SerializeField] public List<Condition> conditionsRef;

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
