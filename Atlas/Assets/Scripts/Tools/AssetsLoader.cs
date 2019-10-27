using System.Collections.Generic;
using System.IO;

namespace Tools
{
    using UnityEngine;

    namespace Tools
    {
        public class AssetsLoader<T> where T : ScriptableObject
        {
            public List<T> Load() {
                var assets = new List<T>();

                try
                {
                    assets.AddRange(Resources.FindObjectsOfTypeAll<T>());
                }
                catch (IOException e)
                {
                    Debug.LogError(e.Message);
                    Debug.LogError(e.StackTrace);
                }
                return assets;
            }
        }
    }
}