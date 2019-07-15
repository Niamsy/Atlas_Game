using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Achievement/Achievement")]
public class AAchievement : ScriptableObject
{
    [SerializeField]
    protected Localization.LocalizedText onSuccess;
    [SerializeField]
    protected bool _SaveState = false;

    protected bool __isAchieve = false;

    private void Awake()
    {
        if (!_SaveState)
        {
            __isAchieve = false;
        }
    }

    private void OnDisable()
    {
        if (!_SaveState)
        {
            __isAchieve = false;
        }
    }

    public bool isAchieve()
    {
        return __isAchieve;
    }

    public string getAchievementSuccessString()
    {
        return onSuccess.Value;
    }

    public void Achieve()
    {
        __isAchieve = true;
    }
}
