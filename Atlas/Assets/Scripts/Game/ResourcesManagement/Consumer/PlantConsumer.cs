using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Inventory;
using Game.Item;
using Game.Questing;
using Plants;
using Plants.GrowerSystem;
using Plants.Plant;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.ResourcesManagement.Consumer
{
    public class PlantConsumer : IConsumer
    {
        #region Protected Variables
        [SerializeField]
        protected int _starvationTimeLimit;

        [SerializeField] private ConditionEvent _conditionEvent = null;
        [SerializeField] private Condition _conditionToTrigger = null;
        [SerializeField] private int TriggerInterval = 5;
        [SerializeField] private int TriggerMinimumQuantity = 5;
        [SerializeField] private Resource[] TriggerOnRessource = new []{Resource.Water};
        protected bool _starved;
        protected bool _starving = false;
        #endregion

        #region Properties Accessors
        public bool Starved
        {
            get { return _starved; }

            set { _starved = value; }
        }

        public bool IsStarving
        {
            get => _starving;

            set => _starving = value;
        }

        public int StarvationTimeLimit
        {
            get { return _starvationTimeLimit; }

            set { _starvationTimeLimit = value; }
        }

        //private PlantStatistics _plantStats;
        //private List<Stage.Need> _needs;
        public List<Stock> ConsumedStocks;

        #endregion

        #region Public Methods
        public void Initialize(PlantStatistics statistics, List<Stage.Need> needs)
        {
            if (IsInvoking("ConsumeResource"))
                CancelInvoke("ConsumeResource");

            //_plantStats = statistics;
            //_needs = needs;
            GrowthRate rates = statistics.GrowthRate;
            
            ResourcesToConsume.RemoveAll(x => true);
            ConsumedStocks.RemoveAll(x => true);
            foreach (var need in needs)
            {
                ResourcesToConsume.Add(need.type);
                LinkedStock[need.type].Limit = need.quantity * 2;
                ConsumedStocks.Add(new Stock() { Resource = need.type, Limit = need.quantity, Quantity = 0});
            }
            ConsumptionRate.TickRate = rates.ConsumationRate;
            ConsumptionRate.ResourcePerTick = rates.QuantityConsumed;
            _starved = false;
        }

        public void StartInvoking()
        {
            InvokeRepeating("ConsumeResource", Random.Range(1f, 3f), ConsumptionRate.TickRate);
        }

        public override void ConsumeResource()
        {
            foreach (var stock in ConsumedStocks)
            {
                stock.Quantity += LinkedStock.RemoveResources(stock.Resource, ConsumptionRate.ResourcePerTick);
                TriggerEvent(stock);
                if (LinkedStock[stock.Resource].Quantity == 0)
                {
                    if (_starveCoroutine == null)
                    {
                        _starveCoroutine = StartCoroutine("Starving");
                        _starving = true;
                    }
                }
                else
                {
                    StopCoroutine("Starving");
                    _starveCoroutine = null;
                    _starving = false;
                }
            }
        }
        #endregion

        #region Private Methods

        private bool ShouldTriggerEvent()
        { 
            return true;
            return Math.Abs(Time.deltaTime % TriggerInterval) <= 0.01f;
        }
        
        private void TriggerEvent(Stock stock)
        {
            if (!ShouldTriggerEvent()) return;
            if (!TriggerOnRessource.Contains(stock.Resource)) return;
            if (stock.Quantity < TriggerMinimumQuantity) return;
            if (_conditionEvent == null || _conditionToTrigger == null) return;
            if (_seed == null) return;
            
            _conditionEvent.Raise(_conditionToTrigger, _seed ,1);
        }

        private ItemAbstract _seed = null;
        
        private void Start()
        {
            var parent = GetComponentInParent<ItemStackBehaviour>();
            if (parent != null)
            {
                _seed = parent.Slot.Content;
            }
            StartInvoking();
        }

        //TODO: Différencié les starved (Manque d'eau et/ou lumière ...)
        private Coroutine _starveCoroutine;
        
        private IEnumerator Starving()
        {
            yield return new WaitForSeconds(_starvationTimeLimit);

            _starved = true;
            _starveCoroutine = null;
        }
        #endregion
        
        ///TODO: Detect Producer
    }
}