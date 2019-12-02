using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Plants;
using Plants.GrowerSystem;
using Plants.Plant;
using UnityEngine;

namespace Game.ResourcesManagement.Consumer
{
    public class PlantConsumer : IConsumer
    {
        #region Protected Variables
        [SerializeField]
        protected int _starvationTimeLimit;
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

        private void Start()
        {
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