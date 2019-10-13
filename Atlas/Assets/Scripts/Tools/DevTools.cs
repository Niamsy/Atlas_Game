using Game.SavingSystem;
using UnityEngine;

namespace Tools
{
    public class DevTools : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        static void BeforeSplashScreen_RuntimeMethod()
        {
#if ATLAS_DEBUG
            SaveManager.Instance.InputControls.Debug.Enable();
#else
            SaveManager.Instance.InputControls.Debug.Disable();
#endif
        }
    }
}
