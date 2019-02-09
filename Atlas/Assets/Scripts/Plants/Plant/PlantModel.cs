using System.Collections;
using System.Collections.Generic;
using Game.Item.PlantSeed;
using UnityEngine;

namespace Plants.Plant
{
    [RequireComponent(typeof(MeshRenderer))]
    public class PlantModel : MonoBehaviour
    {
        public Stock        stock;
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
            Animator animator = this.GetComponent<Animator>();
            MeshRender = this.GetComponent<MeshRenderer>();
            if (stages.Count > 0)
            {
                Instantiate(stages[current_stage].Model, transform);
            }
            if (stock && stock.GetCount() == 0)
                animator.SetTrigger("FadeIn");
        }

        public void GiveResource()
        {
            Debug.Log("GIVE");
            Animator animator = this.GetComponent<Animator>();
            List<Resources> rcs = new List<Resources>();
            rcs.Add(new Resources());
            stock.Put(rcs);
            if (stock.GetCount() > 0)
                animator.SetTrigger("FadeOut");
        }

        public void ConsumeResource()
        {
            Debug.Log("CONSUME");
            Animator animator = this.GetComponent<Animator>();
            if (stock.GetCount() == 1)
                animator.SetTrigger("FadeIn");
            if (stock.GetCount() > 0)
                stock.Remove(1);
        }

        public void GoToNextStage()
        {
            DestroyImmediate(stages[current_stage].Model);
            ++current_stage;
            if (current_stage > stages.Count - 1)
            {
                DestroyPlant();
            }
            else
            {
                Instantiate(stages[current_stage].Model, transform);
            }
        }

        public void DestroyPlant()
        {
            if (current_stage > 1)
                DestroyImmediate(stages[current_stage - 1].Model);
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
