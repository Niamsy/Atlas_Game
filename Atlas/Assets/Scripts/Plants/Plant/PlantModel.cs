using System.Collections.Generic;
using Game.Item.PlantSeed;
using UnityEngine;
using UnityEngine.UI;

namespace Plants.Plant
{
    public class PlantModel : MonoBehaviour
    {
        #region Public Properties

        public PlantStatistics PlantStatistics;

        #endregion

        #region Private & Protected Properties

        protected int current_stage = 0;
        private List<Producer> _producers = new List<Producer>();
        private Dictionary<Resources, Consumer> _consumers = new Dictionary<Resources, Consumer>();
        private GameObject _currentModel = null;
        private Canvas _GuiCanvasName = null;
        private bool _reachedFinalStage = false;
        private bool _isSowed = false;

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

        public Stage CurrentStage
        {
            get
            {
                return PlantStatistics.Stages[current_stage];
            }
        }

        public bool IsSowed
        {
            get { return _isSowed; }
        }

        #endregion


        #region Public Methods
        public void GoToNextStage()
        {
            ++current_stage;
            Destroy(_currentModel);
            if (CurrentStage.Model)
            {
                _currentModel = Instantiate(CurrentStage.Model, transform);
                _currentModel.GetComponent<MeshRenderer>().materials = CurrentStage.Materials;
            }
            PlayEffect(CurrentStage.GrowEffect);
            UpdateConsumers();
            if (current_stage == PlantStatistics.Stages.Count - 1)
            {
                _reachedFinalStage = true;
            }
        }

        public void Sow()
        {
            _isSowed = true;
        }

        public void DestroyPlant()
        {
            PlayEffect(CurrentStage.DeathEffect);
            if (_currentModel)
                Destroy(_currentModel);
            Destroy(gameObject);
        }

        public void PlayEffect(GameObject effectToPlay)
        {
            if (effectToPlay)
            {
                GameObject effect = Instantiate(effectToPlay, transform);
                ParticleSystem system = effect.GetComponent<ParticleSystem>();
                if (system)
                {
                    Destroy(effect, system.main.startLifetime.constantMax);
                }
            }
        }

        public void Start()
        {
            Animator animator = GetComponent<Animator>();

            if (PlantStatistics.Stages != null && PlantStatistics.Stages.Count > 0)
            {
                _currentModel = Instantiate(CurrentStage.Model, transform);
                Tree tree = _currentModel.GetComponent<Tree>();
                if (tree == null)
                {
                    _currentModel.GetComponent<MeshRenderer>().materials = CurrentStage.Materials;
                }
                else
                {

                }
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
            _GuiCanvasName = gameObject.GetComponentInChildren<Canvas>();
            Text name = _GuiCanvasName.gameObject.GetComponentInChildren<Text>();
            name.text = gameObject.name;
            _GuiCanvasName.gameObject.SetActive(false);
        }

        private void UpdateConsumers()
        {
            foreach (Stage.Need need in CurrentStage.Needs)
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

            foreach (Stage.Need need in CurrentStage.Needs)
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
            foreach (Stage.Need need in CurrentStage.Needs)
            {
                if (_consumers[need.type].Starved)
                {
                    return true;
                }
            }
            return false;
        }
        private void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                _GuiCanvasName.gameObject.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                _GuiCanvasName.gameObject.SetActive(false);
            }
        }

        #endregion
    }
}
