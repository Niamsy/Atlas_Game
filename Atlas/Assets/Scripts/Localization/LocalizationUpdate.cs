using UnityEngine;

namespace Localization
{
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

            LocalizationManager.Instance.CurrentLanguage = newLanguage;
            fs.setConfigFileValue("Default", "Lang", ((int)newLanguage).ToString());
            fs.saveConfig();
            IsFrench = !IsFrench;
        }
    }
}
