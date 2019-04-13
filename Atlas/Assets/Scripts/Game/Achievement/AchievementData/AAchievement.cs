using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AAchievement
{
    protected string onSuccess;
    protected bool __isAchieve = false;

    public bool isAchieve()
    {
        return __isAchieve;
    }

    public string getAchievementSuccessString()
    {
        return onSuccess;
    }

    public void Achieve()
    {
        __isAchieve = true;
    }
}
