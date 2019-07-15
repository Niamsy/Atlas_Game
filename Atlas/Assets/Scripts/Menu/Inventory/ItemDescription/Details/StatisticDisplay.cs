using UnityEngine;

namespace Menu.Inventory.ItemDescription.Details
{
    public abstract class StatisticDisplay<TStatistic> : MonoBehaviour
    {
        protected TStatistic StoredValue;

        public void SetValue(TStatistic value)
        {
            StoredValue = value;

            UpdateDisplay();
        }
        
        public abstract void UpdateDisplay();
    }
}
