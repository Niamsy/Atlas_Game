using System.Collections;
using System.Collections.Generic;
using Game.Item.PlantSeed;
using UnityEngine;

namespace Plants.Plant
{
    [RequireComponent(typeof(MeshRenderer))]
    public class PlantModel : MonoBehaviour
    {
        private MeshRenderer meshRenderer;
        public MeshRenderer MeshRender
        {
            get { return meshRenderer; }
            set { meshRenderer = value; }
        }

        public PlantItem PlantItem;

        public List<Stage> stages;
        protected int current_stage = 0;
        public SoilType ActualSoil;

        public List<Producer> Producer;
        public List<Consumer> Consumer;

        #region Methods

        private void Awake()
        {
            MeshRender = this.GetComponent<MeshRenderer>();
            if (stages.Count > 0)
                MeshRender.material = stages[current_stage].Model;
        }

        public void GoToNextStage()
        {
            ++current_stage;
            if (current_stage > stages.Count - 1)
            {
                DestroyPlant();
            }
            else
            {
                Debug.Log(current_stage);
                MeshRender.material = stages[current_stage].Model;
            }
        }

        public void DestroyPlant()
        {
            Destroy(this.gameObject);
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
                // material.SetFloat("_Percentage", stages[current_stage].Needs[0].quantity);
                material.SetFloat("_IsPlant", 1);
            }
        }
        #endregion
    }
}
