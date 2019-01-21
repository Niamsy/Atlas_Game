using System.Collections;
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
        
        public float             WaterNeed = 0.5f;
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

        public IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                UpdatePlantValue();
            }
        }
        
        public void UpdatePlantValue()
        {
            foreach (var material in MeshRender.materials)
                material.SetFloat("_Percentage", WaterNeed);
        }
        #endregion
    }
}
