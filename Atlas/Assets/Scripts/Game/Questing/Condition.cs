using Localization;
using UnityEngine;

namespace Game.Questing
{
    [CreateAssetMenu(fileName = "Condition", menuName = "Questing/Condition")]
    public class Condition : AData
    {
        [SerializeField] private LocalizedFormatText format;

        public LocalizedFormatText Format => format;
    }
}