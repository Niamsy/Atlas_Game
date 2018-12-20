using UnityEngine;
using UnityEngine.UI;

namespace Menu.Inventory.ItemDescription.Details
{
    public class MonthRangeDisplay : StatisticDisplay<bool[]>
    {
        private Image[] _entries;

        private void Awake()
        {
            _entries = new Image[12];
   
            Transform value = transform.Find("Values");
            int x = 0;
            foreach (Transform child in value)
                _entries[x++] = child.Find("Background").GetComponent<Image>();
        }
        
        public override void UpdateDisplay()
        {
            if (StoredValue != null)
            {
                for (int x = 0; x < _entries.Length; x++)
                    _entries[x].enabled = !(StoredValue[x]);
            }
        }
    }
}
