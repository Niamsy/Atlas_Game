using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Inventory.ItemDescription.Details
{
    public class RangeFloatDisplay : StatisticDisplay<RangeFloat>
    {
        [SerializeField] private Text _maxValue = null;
        [SerializeField] private Text _minValue = null;
        
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
