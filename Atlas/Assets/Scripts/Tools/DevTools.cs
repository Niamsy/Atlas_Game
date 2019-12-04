using System;
using FileSystem;
using Localization;
using UnityEngine;

namespace Tools
{
    public static class DevTools
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        public static void BeforeSplashScreen_RuntimeMethod()
        {
            AtlasFileSystem fs = AtlasFileSystem.Instance;

            try
            {
                LocalizationManager.Instance.CurrentLanguage = (SystemLanguage) (fs.GetConfigIntValue(Key.Lang));
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
            }

            try
            {
                int resolutionWidth = fs.GetConfigIntValue(Key.ResolutionWidth, Section.Graphical);
                int resolutionHeight = fs.GetConfigIntValue(Key.ResolutionHeight, Section.Graphical);
                FullScreenMode fullscreenEffect =
                    (FullScreenMode) fs.GetConfigIntValue(Key.Fullscreen, Section.Graphical);

                Screen.SetResolution(resolutionWidth, resolutionHeight, fullscreenEffect);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);

            }
        }
    }
}
