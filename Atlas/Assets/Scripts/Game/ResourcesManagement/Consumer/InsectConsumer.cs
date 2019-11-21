using Game.Insects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.ResourcesManagement.Consumer
{
    public class InsectConsumer : IConsumer
    {
        #region Protected Variables
        [SerializeField]
        protected int _starvationTimeLimit;
        [SerializeField]
        protected int _tickRate;

        protected bool _starved;
        protected bool _starving = false;
        protected bool _full = false;
        protected Stock _consumedStock;
        #endregion


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

        public int TickRate
        {
            get { return _tickRate; }

            set { _tickRate = value; }
        }

        public Stock ConsumedStock
        {
            get { return _consumedStock; }

            set { _consumedStock = value; }
        }

        public bool Full
        {
            get { return _full; }
       
            set { _full = value; }
        }

        private void Start()
        {
            StartInvoking();
        }

        public void Initialize(int nb)
        {
            if (IsInvoking("ConsumeResource"))
                CancelInvoke("ConsumeResource");

            ResourcesToConsume.RemoveAll(x => true);
            ResourcesToConsume.Add(Resource.Energy);
            LinkedStock[Resource.Energy].Limit = nb * 5;
            ConsumedStock = new Stock
            {
                Resource = Resource.Energy,
                Quantity = 0,
                Limit = nb * 5
            };
            ConsumptionRate.TickRate = _tickRate;
            ConsumptionRate.ResourcePerTick = nb;
            _starved = false;
        }

        public void StartInvoking()
        {
            InvokeRepeating("ConsumeResource", Random.Range(1f, 3f), ConsumptionRate.TickRate);
        }

        public override void ConsumeResource()
        {
            ConsumedStock.Quantity += LinkedStock.RemoveResources(ConsumedStock.Resource, ConsumptionRate.ResourcePerTick);
            if (LinkedStock[ConsumedStock.Resource].Quantity >= LinkedStock[ConsumedStock.Resource].Limit)
            {
                Full = true;
            }
            else
                Full = false;
            if (LinkedStock[ConsumedStock.Resource].Quantity == 0)
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

        public void UpdateRates(int nb)
        {
            LinkedStock[Resource.Energy].Limit = nb * 5;
            ConsumedStock.Limit = nb * 5;
            ConsumptionRate.ResourcePerTick = nb;
            Full = false;
        }

        private Coroutine _starveCoroutine;

        private IEnumerator Starving()
        {
            yield return new WaitForSeconds(_starvationTimeLimit);

            _starved = true;
            _starveCoroutine = null;
        }
    }
}
