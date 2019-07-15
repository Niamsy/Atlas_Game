using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class AchievementManager : Singleton<AchievementManager>
{
    #region Members
    public enum AchievementId
    {
        PickupBucket,
        PickupShovel,
        PickupFirstSeed,
    }

    [System.Serializable]
    public struct Achievement {
        public AchievementId id;
        public AAchievement achievement;
    }

    public Achievement[] achievementData;
    #endregion
    AchievementManager()
    {
    }

    public void achieve(AchievementId id)
    {
        foreach (Achievement achievement in achievementData)
        {
            if (achievement.id == id && !achievement.achievement.isAchieve())
            {
                achievement.achievement.Achieve();
                Popup.Instance.sendPopup(achievement.achievement.getAchievementSuccessString());
            }
        }
    }
}
