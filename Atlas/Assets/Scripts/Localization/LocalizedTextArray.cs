using Localization;
using UnityEngine;

namespace Game.HUD
{
    [CreateAssetMenu(fileName = "LocalizedText", menuName = "Localization/Text Array")]
    public class LocalizedTextArray : ScriptableObject
    {
        public LocalizedText[] Entries;
    }
}
