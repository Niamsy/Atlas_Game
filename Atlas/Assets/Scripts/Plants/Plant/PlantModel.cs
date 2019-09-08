using Game.DayNight;
using Game.Map;
using Game.ResourcesManagement;
using Game.ResourcesManagement.Consumer;
using Localization;
using UnityEngine;
using UnityEngine.Events;

namespace Plants.Plant
{
    [RequireComponent(typeof(PlantConsumer))]
    public class PlantModel : MonoBehaviour
    {
        #region Custom Events
        public UnityEvent OnLevelUp;
        public UnityEvent OnDeath;
        #endregion

        #region Public Properties
        public PlantStatistics PlantStatistics;
        public ResourcesStock  RessourceStock;
        #endregion

        #region Private & Protected Properties

        protected int current_stage = 0;
        [SerializeField]
        private PlantConsumer _consumer = null;
        [SerializeField]
        private PlantProducer _producer = null;
        private GameObject _currentModel = null;
        [SerializeField]
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

        public int CurrentStageInt { get { return (current_stage); } }
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
            OnLevelUp.Invoke();
            GoToStage(current_stage + 1);
        }

        public void GoToStage(int value, bool playEffect = true)
        {
            current_stage = value;
            Destroy(_currentModel);
            if (CurrentStage.Model)
            {
                _currentModel = Instantiate(CurrentStage.Model, transform);
                _currentModel.GetComponent<MeshRenderer>().materials = CurrentStage.Materials;
            }
            if (playEffect)
                PlayEffect(CurrentStage.GrowEffect);
            UpdateProducer();
            UpdateConsumers();
            if (current_stage == PlantStatistics.Stages.Count - 1)
                _reachedFinalStage = true;
        }
        
        public void Sow()
        {
            _isSowed = true;
        }

        public void DestroyPlant()
        {
            OnDeath.Invoke();
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
            if (PlantStatistics.Stages != null && PlantStatistics.Stages.Count > 0)
            {
                _currentModel = Instantiate(CurrentStage.Model, transform);
                var tree = _currentModel.GetComponent<Tree>();
                if (tree == null)
                    _currentModel.GetComponent<MeshRenderer>().materials = CurrentStage.Materials;
            }

            UpdateConsumers();

            InvokeRepeating("UpdatePlantValue", Random.Range(1f, 2f), 3f);

        }
        
        public void UpdatePlantValue()
        {
            if (IsDead())
                DestroyPlant();

            if (!_reachedFinalStage && CanGoToNextStage())
                GoToNextStage();
        }

        public void SetPlantName()
        {
            if (_GuiCanvasName)
            {
                var name = _GuiCanvasName.gameObject.GetComponentInChildren<LocalizedTextBehaviour>();
                if (name)
                    name.LocalizedAsset = PlantStatistics.NameAsset;
                _GuiCanvasName.gameObject.SetActive(false);
            }
        }

        #endregion

        #region Private Methods

        private void Awake()
        {
            MeshRender = GetComponent<MeshRenderer>();
            SetPlantName();
            LevelManager.PlantsSystem.AddPlantToTheMap(this);
        }

        private void OnDestroy()
        {
            if (LevelManager.PlantsSystem != null)
                LevelManager.PlantsSystem.RemovePlantFromTheMap(this);
        }
        private void UpdateProducer()
        {
            _producer.StockedResources[Resource.Oxygen].Limit = current_stage * 100;
        }

        private void UpdateConsumers()
        {
            _consumer.Initialize(PlantStatistics, CurrentStage.Needs);
            _consumer.StartInvoking();
        }

        private bool CanGoToNextStage()
        {
            foreach (var stock in _consumer.ConsumedStocks )
            {
                if (stock.Quantity < stock.Limit)
                    return (false);
            }

            return (true);
        }

        private bool IsDead()
        {
            return (_consumer.Starved);
        }

        private void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.CompareTag("Player"))
                _GuiCanvasName.gameObject.SetActive(true);
        }
        private void OnTriggerExit(Collider col)
        {
            if (col.gameObject.CompareTag("Player"))
                _GuiCanvasName.gameObject.SetActive(false);
        }
        #endregion
    }
}
