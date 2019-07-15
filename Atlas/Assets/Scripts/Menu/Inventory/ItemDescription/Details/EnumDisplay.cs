using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Inventory.ItemDescription.Details
{
    public class EnumDisplay : StatisticDisplay<short>
    {
        private List<Image> _entries;

        private void Awake()
        {
            _entries = new List<Image>();
            Transform value = transform.Find("Values");
            foreach (Transform child in value)
                _entries.Add(child.Find("Background").GetComponent<Image>());
        }

        public override void UpdateDisplay()
        {
            if (_entries == null)
                Awake();
            for (int x = 0; x < _entries.Count; x++)
                _entries[x].enabled = (StoredValue & (1 << x)) == 0;
        }
    }
}
