using Game.Item;
using Game.Item.PlantSeed;
using Menu.Inventory.ItemDescription.Details;
using UnityEngine;

namespace Menu.Inventory.ItemDescription
{
    public class PlantStatsDescriptionHUD : ASubItemDescriptionHUD
    {
        [SerializeField] private FloatDisplay _maxHeight = null;
        [SerializeField] private FloatDisplay _growthDuration = null;

        [SerializeField] private RangeFloatDisplay _soilPh = null;
        [SerializeField] private RangeFloatDisplay _soilHumidity = null;
        [SerializeField] private RangeFloatDisplay _sunExposure = null;
        [SerializeField] private RangeFloatDisplay _coldResistance = null;
        
        [SerializeField] private EnumDisplay _reproductions = null;
        [SerializeField] private EnumDisplay _soilType = null;
        [SerializeField] private EnumDisplay _plantContainers = null;

        [SerializeField] private MonthRangeDisplay _plantingPeriods = null;
        [SerializeField] private MonthRangeDisplay _floweringPeriods = null;
        [SerializeField] private MonthRangeDisplay _harvestPeriods = null;
        [SerializeField] private MonthRangeDisplay _cuttingPeriods = null;
        /*
        [SerializeField] private GrowthRate GrowthRate;
        */
        
        private Seed _item;

        public override void SetItem(ItemAbstract item)
        {
            Seed seedItem = item as Seed;
            _item = seedItem;
            
            UpdateDisplay();
        }

        public override void UpdateDisplay()
        {
            gameObject.SetActive(_item != null);
        
            if (_item != null)
            {
                _maxHeight.SetValue(_item.PlantStatistics.MaxHeight);
                _growthDuration.SetValue(_item.PlantStatistics.GrowthDuration);
                
                _soilPh.SetValue(_item.PlantStatistics.SoilPh);
                _soilHumidity.SetValue(_item.PlantStatistics.SoilHumidity);
                _soilType.SetValue((short)_item.PlantStatistics.SoilType);

                _sunExposure.SetValue(_item.PlantStatistics.SunExposure);
                _coldResistance.SetValue(_item.PlantStatistics.ColdResistance);
                _reproductions.SetValue((short)_item.PlantStatistics.Reproductions);
                _plantContainers.SetValue((short)_item.PlantStatistics.PlantContainers);

                _plantingPeriods.SetValue(_item.PlantStatistics.PlantingPeriods);
                _floweringPeriods.SetValue(_item.PlantStatistics.FloweringPeriods);
                _harvestPeriods.SetValue(_item.PlantStatistics.HarvestPeriods);
                _cuttingPeriods.SetValue(_item.PlantStatistics.CuttingPeriods);
            }
        }
    }
}
