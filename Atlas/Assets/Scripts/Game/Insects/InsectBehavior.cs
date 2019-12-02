using Game.ResourcesManagement;
using Game.ResourcesManagement.Consumer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace Game.Insects
{
    public class InsectBehavior : MonoBehaviour
    {
        [SerializeField]
        public InsectSystem insect;

        [SerializeField]
        public ResourcesStock stocks;

        [SerializeField]
        public InsectConsumer consumer;

        [SerializeField]
        [Range(1, 200)]
        public int radius;

        [SerializeField]
        public LayerMask _layer;

        protected bool isActing;

        private Vector3 position;

        private new BoxCollider collider;

        private int _currentNumber;

        private EmissionModule emissionBeesStatic;

        private EmissionModule emissionBeesMoving;

        public int CurrentNumber
        {
            get { return _currentNumber; }
            set { _currentNumber = value; }
        }

        private void Awake()
        {
            position = gameObject.transform.position;
            collider = gameObject.GetComponent<BoxCollider>();
            collider.size = new Vector3(radius, radius, radius);
            CurrentNumber = 2;
            /*ParticleSystem[] systs = insect.bees.GetComponents<ParticleSystem>();
            if (systs.Length == 2)
            {
                emissionBeesStatic = systs[0].emission;
                emissionBeesStatic.enabled = true;
                emissionBeesMoving = systs[1].emission;
                emissionBeesMoving.enabled = true;
            }*/
        }

        private void Start()
        {
            InvokeRepeating("CheckActing", 5.0f, 5.0f);
            InvokeRepeating("CheckEvolving", 1.0f, 1.0f);
            if (insect != null)
            {
                consumer.Initialize(CurrentNumber);
                consumer.StartInvoking();
            }
        }

        private void CheckEvolving()
        {
            if (consumer.Full)
            {
                if (CurrentNumber < insect.maximumNumber)
                    Growth();
            }
            if (consumer.Starved)
            {
                if (CurrentNumber > 2)
                    DeGrowth();
            }
        }

        private void Growth()
        {
            Debug.Log("GROWTH");
            CurrentNumber *= 2;
            if (CurrentNumber > insect.maximumNumber)
                CurrentNumber = insect.maximumNumber;
            consumer.UpdateRates(CurrentNumber);
            //emissionBeesStatic.rateOverTime = CurrentNumber / 2;
            //emissionBeesMoving.rateOverTime = CurrentNumber / 2;
        }

        private void DeGrowth()
        {
            Debug.Log("DEGROWTH ");
            CurrentNumber /= 2;
            if (CurrentNumber < 2)
                CurrentNumber = 2;
            consumer.UpdateRates(CurrentNumber);
        }

        private void CheckActing()
        {
            Collider[] colliders = Physics.OverlapBox(position, collider.size, Quaternion.identity, _layer);
            foreach (Collider col in colliders)
            {
                foreach (InsectAction action in insect.actions)
                {
                    Debug.Log("Check " + col);
                    var interactable = col.GetComponent<IInteractableInsect>();
                    if (interactable!= null)
                    {
                        Debug.Log(interactable.ToString());
                        if (!consumer.Full && CurrentNumber < insect.maximumNumber)
                        {
                            for (int i = 0; i < CurrentNumber / 2; i++)
                                interactable.insectInteract(action, consumer);
                        }
                    }
                }
            }
        }
    }
}