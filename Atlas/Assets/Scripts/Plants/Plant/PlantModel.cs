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

        public PlantItem        PlantItem;
        
        public List<Stage>      stages;
        protected int           current_stage = 0;
        public SoilType         ActualSoil;

        public List<Producer>    Producer;
        public List<Consumer>    Consumer;

        #region Methods

        private void Awake()
        {
            MeshRender = GetComponent<MeshRenderer>();
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
            {
                material.SetFloat("_Percentage", stages[current_stage].Needs[0].quantity);
                material.SetFloat("_IsPlant", 1);
            }
        }
        #endregion
    }
}
