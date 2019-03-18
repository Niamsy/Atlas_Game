using System.Collections.Generic;
using Tools;
using UnityEngine;
using Plants.GrowerSystem;

namespace Plants.Plant
{
    //public enum GrowthRate : short
    //{
    //    Slow    = 1, // 001
    //    Normal  = 2, // 010
    //    Quick   = 4  // 100
    //}

    public enum Reproduction : short
    {
        Seedling = 1,    // 00001
        Division = 2,    // 00010
        Cutting  = 4,    // 00100
        Layering = 8,    // 01000
        Grafting = 16    // 10000
    }

    public enum SoilType : short
    {
        Calcareous = 1,    // 00001
        Clay       = 2,    // 00010
        Humus      = 4,    // 00100
        Sandy      = 8,    // 01000
        Stony      = 16    // 10000
    }
    
    public enum PlantContainer : short
    {
        Ground       = 1,    // 01
        Potted       = 2,    // 10
    }
    
    [CreateAssetMenu(fileName = "Plant statistics", menuName = "Plant System/PlantStatistic", order = 1)]
    public class PlantStatistics : ScriptableObject
    {
        [Header("Size")]
        [SerializeField] private float             _maxHeight = 1f;
        public float                               MaxHeight { get { return (_maxHeight); } }

        [Header("Growth")]
        [SerializeField] private GrowthRate        _growthRate;
        public GrowthRate                          GrowthRate { get { return (_growthRate); } }
        [SerializeField] private float             _growthDuration = 1f;
        public float                               GrowthDuration { get { return (_growthDuration); } }

        [Header("Reproduction")]
        [SerializeField] private Reproduction      _reproduction;
        public Reproduction                        Reproductions { get { return (_reproduction); } }

        [Header("Soil needs")]
        [SerializeField] private SoilType         _soilType;
        public SoilType                           SoilType { get { return (_soilType); } }
        [SerializeField] private RangeFloat       _soilPh;
        public RangeFloat                         SoilPh { get { return (_soilPh); } }
        [SerializeField] private RangeFloat       _soilHumidity;
        public RangeFloat                         SoilHumidity { get { return (_soilHumidity); } }
        [SerializeField] private PlantContainer   _plantContainer;
        public PlantContainer                     PlantContainers { get { return (_plantContainer); } }
        
        [Header("Environnement needs")]
        [SerializeField] private RangeFloat      _sunExposure;
        public RangeFloat                        SunExposure { get { return (_sunExposure); } }
        [SerializeField] private RangeFloat      _coldResistance;
        public RangeFloat                        ColdResistance { get { return (_coldResistance); } }

        [Header("Periods")]
        [SerializeField] private bool[]            _plantingPeriods = new bool[12];
        public bool[]                              PlantingPeriods { get { return (_plantingPeriods); } }
        [SerializeField] private bool[]            _floweringPeriods = new bool[12];
        public bool[]                              FloweringPeriods { get { return (_floweringPeriods); } }
        [SerializeField] private bool[]            _harvestPeriods = new bool[12];
        public bool[]                              HarvestPeriods { get { return (_harvestPeriods); } }
        [SerializeField] private bool[]            _cuttingPeriods = new bool[12];
        public bool[]                              CuttingPeriods { get { return (_cuttingPeriods); } }
        
        [Header("Stages")]
        [SerializeField] private List<Stage>       _stages = new List<Stage>();
        public List<Stage>                         Stages { get { return (_stages); } }
    }
}
