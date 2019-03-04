using UnityEngine;
using Variables;

namespace Plants
{
    public class Consumer : MonoBehaviour, IConsumer
    {
        #region Protected Variables
        [SerializeField]
        protected Resources type;
        [SerializeField]
        [Tooltip("Rate at which the plant look for producers")]
        [MinMaxRange(0.5f, 12000f)]
        protected float producerScanRate = 3.0f;
        [SerializeField]
        [MinMaxRange(0.5f, 12000f)]
        [Tooltip("Rate at which the plant consume ressources")]
        protected float consumeRate = 1.0f;
        [SerializeField]
        [Tooltip("Consumption quantity of ressources per consumption tick")]
        protected int rate;
        [SerializeField]
        [Tooltip("Maximum quantity of ressources that can be taken from a producer per tick")]
        protected int quantity;
        protected Stock stocks;
        [SerializeField]
        protected int starvationTimeLimit;
        protected bool starverd;
        [SerializeField]
        protected FloatReference range;
        #endregion

        #region Properties Accessors
        public float ProducerScanRate
        {
            get { return producerScanRate; }
        }

        public float ConsumeRate
        {
            get { return consumeRate; }
        }

        public Stock Stocks
        {
            get { return stocks; }
        }

        public int Rate
        {
            get { return rate; }

            set { rate = value; }
        }

        public bool Starverd
        {
            get { return starverd; }

            set { starverd = value; }
        }

        public int StarvationTimeLimit
        {
            get { return starvationTimeLimit; }

            set { starvationTimeLimit = value; }
        }

        public int Quantity
        {
            get { return stocks.GetCount(); }

            set { quantity = value; }
        }

        public FloatReference Range
        {
            get { return range; }

            set { range = value; }
        }

        public Resources Type
        {
            get
            {
                return type;
            }
        }
        #endregion

        #region Public Methods
        public void Consume()
        {
            stocks.Remove(rate);
        }

        public int Load(IProducer producer, int quantity)
        {
            return stocks.Put(producer.Unload(quantity)).Count;
        }
        #endregion

        #region Private Methods
        private void Awake()
        {
            stocks = gameObject.AddComponent<Stock>();
        }

        private void Start()
        {
            InvokeRepeating("DetectProducers", Random.value, ProducerScanRate);
            InvokeRepeating("Consume", Random.Range(1f, 3f), ConsumeRate);
        }

        private void DetectProducers()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, Range, LayerMask.GetMask("Producer"));
            Debug.Log("Current Consumer Range" + Range.Value + ": \nCurrent colliders count = " + colliders.Length);
            int quantityTaken = 0;
            foreach (Collider collider in colliders)
            {
                Producer producer = collider.GetComponent<Producer>();
                if (producer && producer.Type == Type)
                {
                    quantityTaken += Load(producer, Quantity);
                    Debug.Log("Took " + quantityTaken + " amount");
                }
                if (quantityTaken >= Quantity)
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