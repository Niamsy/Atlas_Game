using System.Collections.Generic;
using JetBrains.Annotations;
using Localization;
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
        [Header("Main")]
        [SerializeField] private LocalizedText     _name = null;
        public string                              Name => (_name.Value);

        public string                              ScientificName = "";
        public LocalizedText                       NameAsset => (_name);
        [SerializeField] private int               _ID = 1;
        public int                                 ID => (_ID);
        [SerializeField] private GameObject        _prefab = null;
        public GameObject                          Prefab => (_prefab);

        [Header("Size")]
        [SerializeField] private float             _maxHeight = 1f;
        public float                               MaxHeight => (_maxHeight);

        [Header("Growth")]
        [SerializeField] private GrowthRate        _growthRate = null;
        public GrowthRate                          GrowthRate => (_growthRate);
        [SerializeField] private float             _growthDuration = 1f;
        public float                               GrowthDuration => (_growthDuration);

        [Header("Reproduction")]
        [SerializeField] private Reproduction      _reproduction = Reproduction.Cutting;
        public Reproduction                        Reproductions => (_reproduction);

        [Header("Soil needs")]
        [SerializeField] private SoilType         _soilType = SoilType.Humus;
        public SoilType                           SoilType => (_soilType);
        [SerializeField] private RangeFloat       _soilPh = new RangeFloat();
        public RangeFloat                         SoilPh => (_soilPh);
        [SerializeField] private RangeFloat       _soilHumidity = new RangeFloat();
        public RangeFloat                         SoilHumidity => (_soilHumidity);
        [SerializeField] private PlantContainer   _plantContainer = PlantContainer.Ground;
        public PlantContainer                     PlantContainers => (_plantContainer);

        [Header("Environnement needs")]
        [SerializeField] private RangeFloat      _sunExposure = new RangeFloat();
        public RangeFloat                        SunExposure => (_sunExposure);
        [SerializeField] private RangeFloat      _coldResistance = new RangeFloat();
        public RangeFloat                        ColdResistance => (_coldResistance);

        [Header("Periods")]
        [SerializeField] private bool[]            _plantingPeriods = new bool[12];
        public bool[]                              PlantingPeriods => (_plantingPeriods);
        [SerializeField] private bool[]            _floweringPeriods = new bool[12];
        public bool[]                              FloweringPeriods => (_floweringPeriods);
        [SerializeField] private bool[]            _harvestPeriods = new bool[12];
        public bool[]                              HarvestPeriods => (_harvestPeriods);
        [SerializeField] private bool[]            _cuttingPeriods = new bool[12];
        public bool[]                              CuttingPeriods => (_cuttingPeriods);

        [Header("Stages")]
        [SerializeField] private List<Stage>       _stages = new List<Stage>();
        public List<Stage>                         Stages => (_stages);
    }
}
