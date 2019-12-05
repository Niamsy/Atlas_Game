using Game.Item;
using Game.Item.PlantSeed;
using Menu.Inventory.ItemDescription.Details;
using Plants.Plant;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Inventory.ItemDescription
{
    public class PlantStatsDescriptionHUD : MonoBehaviour
    {
        private PlantStatistics _ps;
        
        [SerializeField] private Text		_name = null;
        [SerializeField] private Text		_scientifName = null;
        
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
        
        public void SetPlant(PlantStatistics plantStat)
        {
            _ps = plantStat;
            
            UpdateDisplay();
        }

        public void Start()
        {
            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            gameObject.SetActive(_ps != null);
        
            if (_ps != null)
            {
                _name.text = _ps.Name;
                _scientifName.text = _ps.ScientificName;
                
                _maxHeight.SetValue(_ps.MaxHeight);
                _growthDuration.SetValue(_ps.GrowthDuration);
                
                _soilPh.SetValue(_ps.SoilPh);
                _soilHumidity.SetValue(_ps.SoilHumidity);
                _soilType.SetValue((short)_ps.SoilType);

                _sunExposure.SetValue(_ps.SunExposure);
                _coldResistance.SetValue(_ps.ColdResistance);
                _reproductions.SetValue((short)_ps.Reproductions);
                _plantContainers.SetValue((short)_ps.PlantContainers);

                _plantingPeriods.SetValue(_ps.PlantingPeriods);
                _floweringPeriods.SetValue(_ps.FloweringPeriods);
                _harvestPeriods.SetValue(_ps.HarvestPeriods);
                _cuttingPeriods.SetValue(_ps.CuttingPeriods);
            }
        }
    }
}
