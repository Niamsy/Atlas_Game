using Localization;
using UnityEngine;

namespace Game.Item
{
    [CreateAssetMenu(menuName = "Item/Category")]
    public class ItemCategory : ScriptableObject
    {
        [SerializeField] private LocalizedText _name = null;
        [SerializeField] private Color color;
        
        public LocalizedText Name => _name;
        public Color Color => color;
    }
}
