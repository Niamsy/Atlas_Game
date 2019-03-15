using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Plants
{
    public class Consumer : MonoBehaviour, IConsumer
    {
        #region Protected Variables
        [SerializeField]
        protected Resources _type;
        [SerializeField]
        [Tooltip("Rate at which the plant look for producers")]
        [MinMaxRange(0.5f, 12000f)]
        protected float _producerScanRate = 3.0f;
        [SerializeField]
        [MinMaxRange(0.5f, 12000f)]
        [Tooltip("Rate at which the plant consume ressources")]
        protected float _consumeRate = 1.0f;
        [SerializeField]
        [Tooltip("Consumption quantity of ressources per consumption tick")]
        protected int _rate;
        [SerializeField]
        [Tooltip("Maximum quantity of ressources that can be taken from a producer per tick")]
        protected int _quantity;
        protected Stock _stock;
        [SerializeField]
        protected int _starvationTimeLimit;
        protected bool _starved;
        [SerializeField]
        protected float _range;
        #endregion

        #region Properties Accessors
        public float ProducerScanRate
        {
            get { return _producerScanRate; }
        }

        public float ConsumeRate
        {
            get { return _consumeRate; }
        }

        public Stock Stock
        {
            get { return _stock; }
        }

        public int Rate
        {
            get { return _rate; }

            set { _rate = value; }
        }

        public bool Starved
        {
            get { return _starved; }

            set { _starved = value; }
        }

        public int StarvationTimeLimit
        {
            get { return _starvationTimeLimit; }

            set { _starvationTimeLimit = value; }
        }

        public int Quantity
        {
            get { return _quantity; }

            set { _quantity = value; }
        }

        public float Range
        {
            get { return _range; }

            set { _range = value; }
        }

        public Resources Type
        {
            get
            {
                return _type;
            }
        }
        #endregion

        #region Public Methods
        public void Initialize(Plant.PlantStatistics statitics, Stage.Need need)
        {
            if (IsInvoking("DetectProducers"))
            {
                CancelInvoke("DetectProducers");
            }
            if (IsInvoking("Consume"))
            {
                CancelInvoke("Consume");
            }

            GrowerSystem.GrowthRate rates = statitics.GrowthRate;

            _type = need.type;
            _consumeRate = rates.ConsumationRate;
            _producerScanRate = rates.ProducerScanRate;
            _rate = rates.QuantityConsumed;
            _quantity = rates.QuantityTaken;
            _range = statitics.MaxHeight * 3;
            _starvationTimeLimit = 60;
            _starved = false;
            _stock.SetLimit(need.quantity * 2);
        }

        public void StartInvoking()
        {
            InvokeRepeating("DetectProducers", Random.value, ProducerScanRate);
            InvokeRepeating("Consume", Random.Range(1f, 3f), ConsumeRate);
        }

        public void Consume()
        {
            _stock.Remove(_rate);
            if (_stock.GetCount() == 0)
            {
                StartCoroutine("Starving");
            }
        }

        public int Load(IProducer producer, int quantity)
        {
            List<Resources> resources = producer.Unload(quantity);
            int count = resources.Count;
            _stock.Put(resources);
            resources.Clear();
            if (count > 0)
            {
                StopCoroutine("Starving");
            }
            return count;
        }
        #endregion

        #region Private Methods
        private void Awake()
        {
            _stock = gameObject.AddComponent<Stock>();
        }

        private void Start()
        {
            StartInvoking();
        }

        private IEnumerator Starving()
        {
            yield return new WaitForSeconds(_starvationTimeLimit);
            _starved = true;
        }

        private void DetectProducers()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, Range, LayerMask.GetMask("Producer"));
            Debug.Log("Current Consumer Range" + Range + ": \nCurrent colliders count = " + colliders.Length);
            int quantityRemaining = _quantity;
            foreach (Collider collider in colliders)
            {
                Producer producer = collider.GetComponent<Producer>();
                if (producer && producer.Type == Type)
                {
                    Debug.Log("Trying to find " + quantityRemaining + " amount");
                    quantityRemaining -= Load(producer, quantityRemaining);
                    Debug.Log("Took " + (_quantity - quantityRemaining) + " amount");
                }
                if (quantityRemaining <= 0)
                {
                    Debug.Log("Took everything I could");
                    break;
                }
            }

            Debug.Log("End of detection");
        }
        #endregion
    }
}