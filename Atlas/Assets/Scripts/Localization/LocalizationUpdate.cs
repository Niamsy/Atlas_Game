using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationUpdate : MonoBehaviour
{
    public bool IsFrench = false;

    public void Switch()
    {
        AtlasFileSystem fs = AtlasFileSystem.Instance;
        SystemLanguage newLanguage;
        if (IsFrench)
            newLanguage = SystemLanguage.English;
        else
            newLanguage = SystemLanguage.French;

        Localization.LocalizationManager.Instance.CurrentLanguage = newLanguage;
        fs.setConfigFileValue("Default", "Lang", ((int)newLanguage).ToString());
        fs.saveConfig();
        IsFrench = !IsFrench;
    }
}
