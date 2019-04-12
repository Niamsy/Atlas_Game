using System.Collections;
using System.Collections.Generic;
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

    Dictionary<AchievementId, AAchievement> achievementData = new Dictionary<AchievementId, AAchievement>();
    #endregion

    AchievementManager()
    {
        achievementData[AchievementId.PickupBucket] = new BucketAchievement();
        achievementData[AchievementId.PickupShovel] = new ShovelAchievement();
        achievementData[AchievementId.PickupFirstSeed] = new FirstSeedAchievement();
    }

    public void achieve(AchievementId id)
    {
        if (!achievementData[id].isAchieve())
        {
            achievementData[id].Achieve();
            Popup.Instance.sendPopup(achievementData[id].getAchievementSuccessString());
        }
    }
}
