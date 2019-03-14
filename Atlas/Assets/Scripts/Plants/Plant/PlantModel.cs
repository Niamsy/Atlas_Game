using System.Collections.Generic;
using Game.Item.PlantSeed;
using UnityEngine;

namespace Plants.Plant
{
    public class PlantModel : MonoBehaviour
    {
        #region Public Properties

        public PlantStatistics PlantStatistics;
        public PlantItem PlantItem;

        #endregion

        #region Private & Protected Properties

        protected int current_stage = 0;
        private List<Producer> _producers = new List<Producer>();
        private Dictionary<Resources, Consumer> _consumers = new Dictionary<Resources, Consumer>();
        private GameObject _currentModel = null;
        private bool _reachedFinalStage = false;

        #endregion

        #region Public Accessors

        public MeshRenderer MeshRender
        {
            get;
            set;
        }

        public List<Producer> Producers
        {
            get;
            set;
        }

        public List<Consumer> Consumers
        {
            get;
            set;
        }

        #endregion


        #region Public Methods
        public void GoToNextStage()
        {
            ++current_stage;
            Destroy(_currentModel);
            _currentModel = Instantiate(PlantStatistics.Stages[current_stage].Model, transform);
            _currentModel.GetComponent<MeshRenderer>().materials = PlantStatistics.Stages[current_stage].Materials;
            UpdateConsumers();
            if (current_stage == PlantStatistics.Stages.Count - 1)
            {
                _reachedFinalStage = true;
            }
        }

        public void DestroyPlant()
        {
            if (_currentModel)
                Destroy(_currentModel);
            Destroy(gameObject);
        }

        public void Start()
        {
            Animator animator = GetComponent<Animator>();

            if (PlantStatistics.Stages != null && PlantStatistics.Stages.Count > 0)
            {
                _currentModel = Instantiate(PlantStatistics.Stages[current_stage].Model, transform);
                _currentModel.GetComponent<MeshRenderer>().materials = PlantStatistics.Stages[current_stage].Materials;
            }

            UpdateConsumers();

            InvokeRepeating("UpdatePlantValue", Random.Range(1f, 2f), 3f);
        }

        public void UpdatePlantValue()
        {
            if (isDead())
            {
                DestroyPlant();
            }

            if (!_reachedFinalStage && CanGoToNextStage())
            {
                GoToNextStage();
            }
        }

        #endregion

        #region Private Methods

        private void Awake()
        {
            MeshRender = GetComponent<MeshRenderer>();
        }

        private void UpdateConsumers()
        {
            foreach (Stage.Need need in PlantStatistics.Stages[current_stage].Needs)
            {
                if (!_consumers.ContainsKey(need.type))
                {
                    Consumer c = gameObject.AddComponent<Consumer>();
                    c.Initialize(PlantStatistics, need);
                    _consumers.Add(need.type, c);
                }
                else
                {
                    _consumers[need.type].Initialize(PlantStatistics, need);
                    _consumers[need.type].StartInvoking();
                }
            }
        }

        private bool CanGoToNextStage()
        {
            bool canEvolve = true;

            foreach (Stage.Need need in PlantStatistics.Stages[current_stage].Needs)
            {
                if (_consumers[need.type].Stock.GetCount() < need.quantity)
                {
                    canEvolve = false;
                    break;
                }
            }
            return canEvolve;
        }

        private bool isDead()
        {
            foreach (Stage.Need need in PlantStatistics.Stages[current_stage].Needs)
            {
                if (_consumers[need.type].Starved)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}
