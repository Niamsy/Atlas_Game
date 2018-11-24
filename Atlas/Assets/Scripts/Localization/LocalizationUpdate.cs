using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationUpdate : MonoBehaviour
{
    public bool IsFrench = false;

    public void Switch()
    {
        if (IsFrench)
            Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.English;
        else
            Localization.LocalizationManager.Instance.CurrentLanguage = SystemLanguage.French;

        IsFrench = !IsFrench;
    }
}
