using Game.Item;
using Menu.Inventory.ItemDescription.Details;
using UnityEngine;

namespace Menu.Inventory.ItemDescription
{
    public class PlantStatsDescriptionHUD : ASubItemDescriptionHUD
    {
        [SerializeField] private FloatDisplay _maxHeight;
        [SerializeField] private FloatDisplay _growthDuration;

        [SerializeField] private RangeFloatDisplay _soilPh;
        [SerializeField] private RangeFloatDisplay _soilHumidity;
        [SerializeField] private RangeFloatDisplay _sunExposure;
        [SerializeField] private RangeFloatDisplay _coldResistance;
        
        [SerializeField] private EnumDisplay _reproductions;
        [SerializeField] private EnumDisplay _soilType;
        [SerializeField] private EnumDisplay _plantContainers;

        [SerializeField] private MonthRangeDisplay _plantingPeriods;
        [SerializeField] private MonthRangeDisplay _floweringPeriods;
        [SerializeField] private MonthRangeDisplay _harvestPeriods;
        [SerializeField] private MonthRangeDisplay _cuttingPeriods;
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
                _soilType.SetValue(_item.PlantStatistics.SoilTypeRaw);

                _sunExposure.SetValue(_item.PlantStatistics.SunExposure);
                _coldResistance.SetValue(_item.PlantStatistics.ColdResistance);
                _reproductions.SetValue(_item.PlantStatistics.ReproductionsRaw);
                _plantContainers.SetValue(_item.PlantStatistics.PlantContainers);

                _plantingPeriods.SetValue(_item.PlantStatistics.PlantingPeriods);
                _floweringPeriods.SetValue(_item.PlantStatistics.FloweringPeriods);
                _harvestPeriods.SetValue(_item.PlantStatistics.HarvestPeriods);
                _cuttingPeriods.SetValue(_item.PlantStatistics.CuttingPeriods);
            }
        }
    }
}
