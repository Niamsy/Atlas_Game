using Game.DayNight;
using Game.Insects;
using Game.Map;
using Game.ResourcesManagement;
using Game.ResourcesManagement.Consumer;
using Localization;
using UnityEngine;
using UnityEngine.Events;

namespace Plants.Plant
{
    [RequireComponent(typeof(PlantConsumer))]
    public class PlantModel : MonoBehaviour, IInteractableInsect
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

        protected int last_stage = 0;

        [SerializeField]
        private PlantConsumer _consumer = null;
        [SerializeField]
        private PlantProducer _producer = null;
        private GameObject _currentModel = null;
        [SerializeField]
        private Canvas _GuiCanvasName = null;
        private Canvas levelUp = null;
        private Canvas death = null;
        private bool _reachedFinalStage = false;
        private bool _isSowed = false;
        private bool _isPollinate = false;
        private bool _isCycleLifeComplete = false;

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

        public int LastStageInt { get { return (last_stage); } }
        public Stage LastStage
        {
            get
            {
                return PlantStatistics.Stages[last_stage];
            }
        }

        public bool IsSowed
        {
            get { return _isSowed; }
        }

        public bool IsPollinate
        {
            get { return _isPollinate; }
        }

        #endregion

        #region Public Methods
        public void GoToNextStage()
        {
            OnLevelUp.Invoke();
            levelUp.enabled = false;
            GoToStage(current_stage + 1);
        }

        public void GoToStage(int value, bool playEffect = true)
        {
            current_stage = value;
            Destroy(_currentModel);
            if (CurrentStage.Model)
            {
                _currentModel = Instantiate(CurrentStage.Model, transform);
                var mesh = _currentModel.GetComponent<MeshRenderer>();
                if (mesh)
                {
                    mesh.materials = CurrentStage.Materials;
                }
            }
            if (playEffect)
                PlayEffect(CurrentStage.GrowEffect);
            UpdateProducer();
            UpdateConsumers();
            if (current_stage == last_stage)
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
                last_stage = PlantStatistics.Stages.Count - 1;
                var tree = _currentModel.GetComponent<Tree>();
                if (tree == null)
                {
                    var mesh = _currentModel.GetComponent<MeshRenderer>();
                    if (mesh)
                    {
                        mesh.materials = CurrentStage.Materials;
                    }
                }
            }

            UpdateConsumers();

            InvokeRepeating("UpdatePlantValue", Random.Range(1f, 2f), 3f);

        }

        public void UpdatePlantValue()
        {
            if (IsDead())
            {
                DestroyPlant();
                return;
            }

            if (IsStarving())
            {
                if (death.enabled == false)
                    death.enabled = true;
            }
            else
            {
                if (death.enabled == true)
                    death.enabled = false;
            }

            if (!_reachedFinalStage && CanGoToNextStage())
                GoToNextStage();

            if (current_stage == last_stage && RessourceStock[Resource.Energy] != null && RessourceStock[Resource.Energy].Quantity == 0)
            {
                _isCycleLifeComplete = true;
            }
        }

        public void SetPlantName()
        {
            if (_GuiCanvasName)
            {
                var name = _GuiCanvasName.gameObject.GetComponentInChildren<LocalizedTextBehaviour>();
                if (name)
                    name.LocalizedAsset = PlantStatistics.NameAsset;
            }
        }

        #endregion

        #region Private Methods

        private void Awake()
        {
            MeshRender = GetComponent<MeshRenderer>();
            Canvas hud = gameObject.transform.Find("HUD").gameObject.GetComponent<Canvas>();
            foreach (var child in hud.GetComponentsInChildren<Canvas>())
            {
                if (child.name == "LevelUp")
                    levelUp = child;
                else if (child.name == "Death")
                    death = child;
            }
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
            _producer.UpdateRates(current_stage);
        }

        private void UpdateConsumers()
        {
            _consumer.Initialize(PlantStatistics, CurrentStage.Needs);
            _consumer.StartInvoking();
        }

        private bool IsStarving()
        {
            return _consumer.IsStarving;   
        }

        private bool CanGoToNextStage()
        {
            foreach (var stock in _consumer.ConsumedStocks )
            {
                if (stock.Quantity < stock.Limit)
                {
                    if (((float)stock.Quantity / (float)stock.Limit > 0.85f))
                    {
                        if (levelUp.enabled == false)
                            levelUp.enabled = true;
                    }
                    else
                    {
                        if (levelUp.enabled == true)
                            levelUp.enabled = false;
                    }
                    return (false);
                }
            }
            return (true);
        }

        private bool IsDead()
        {
            return (_consumer.Starved || _isCycleLifeComplete == true);
        }

        public void insectInteract(InsectAction action, InsectConsumer consumer)
        {
            if (current_stage == last_stage && RessourceStock.FindResource(Resource.Energy) == true)
            {
                consumer.LinkedStock.AddResources(Resource.Energy, RessourceStock.RemoveResources(Resource.Energy, _producer.finalStageEnergyGiven));
                Debug.Log("Miam");
                Debug.Log(consumer.LinkedStock.ListOfStocks[0].Quantity);
                if (!consumer.Starved)
                    _isPollinate = true;
                else
                    _isPollinate = false;
            }
        }
        #endregion
    }
}
