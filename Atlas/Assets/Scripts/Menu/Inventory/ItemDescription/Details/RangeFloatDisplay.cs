using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Inventory.ItemDescription.Details
{
    public class RangeFloatDisplay : StatisticDisplay<RangeFloat>
    {
        [SerializeField] private Text _maxValue;
        [SerializeField] private Text _minValue;
        
        public override void UpdateDisplay()
        {
            if (StoredValue != null)
            {
                _maxValue.text = StoredValue.Max.ToString();
                _minValue.text = StoredValue.Min.ToString();
            }
        }
    }
}
