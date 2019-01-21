using System.Collections.Generic;
using Game.Item.PlantSeed;
using UnityEngine;

namespace Plants.Plant
{
    [RequireComponent(typeof(MeshRenderer))]
    public class PlantModel : MonoBehaviour
    {
        public MeshRenderer      MeshRender { get; private set; }
        
        public PlantStatistics   Statistics;
        
        public SoilType          ActualSoil;
        
        public List<Producer>    Producer;
        public List<Consumer>    Consumer;

        #region Methods

        private void Awake()
        {
            MeshRender = GetComponent<MeshRenderer>();
        }

        private void OnEnable()
        {
            PlantSystem.Instance.AddPlant(this);
        }

        private void OnDisable()
        {
            PlantSystem.Instance.RemovePlant(this);
        }

        public enum PlantShader
        {
            Default,
            WaterNeed
        }

        private PlantShader _plantShader = PlantShader.Default;

        private void SetShader(PlantShader plantShader)
        {
            
        }
        #endregion
    }
}
