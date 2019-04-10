using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Leveling {
    public class PlayerExperience : Experience
    {
        protected override int CalculateNextLevelXPNeeded(int NextLevel, int CurrentXPNeed)
        {
            return (NextLevel * NextLevel * 100);
        }
    }
}
