using UnityEngine;
using UnityEngine.UI;

namespace Menu.Inventory.ItemDescription.Details
{
    public class ShortDisplay : StatisticDisplay<short>
    {
        [SerializeField] private Text _text;
        
        public override void UpdateDisplay()
        {
            _text.text = StoredValue.ToString();
        }
    }
}
