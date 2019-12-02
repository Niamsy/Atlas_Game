using FileSystem;
using Localization;
using UnityEngine;

namespace Tools
{
    public class DevTools : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        static void BeforeSplashScreen_RuntimeMethod()
        {
            AtlasFileSystem fs = AtlasFileSystem.Instance;

            LocalizationManager.Instance.CurrentLanguage = (SystemLanguage)(fs.GetConfigIntValue(Key.Lang));;
            int resolutionWidth = fs.GetConfigIntValue(Key.ResolutionWidth, Section.Graphical);
            int resolutionHeight = fs.GetConfigIntValue(Key.ResolutionHeight, Section.Graphical);
            FullScreenMode fullscreenEffect = (FullScreenMode) fs.GetConfigIntValue(Key.Fullscreen, Section.Graphical);

            Screen.SetResolution(resolutionWidth, resolutionHeight, fullscreenEffect);
        }
    }
}
