using Localization;
using UnityEngine;

namespace Localization
{
    [CreateAssetMenu(fileName = "LocalizedFormatText", menuName = "Localization/FormatText")]
    public class LocalizedFormatText : LocalizedText
    {
        public string Format(params object[] objects)
        {
            return string.Format(Value, objects);
        }
    }
}