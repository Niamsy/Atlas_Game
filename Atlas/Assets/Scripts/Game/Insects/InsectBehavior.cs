using Game.ResourcesManagement;
using Game.ResourcesManagement.Consumer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        public int          radius;
        protected bool      isActing;

        private Vector3     position;

        private void Awake()
        {
            position = gameObject.transform.position;
        }

        private void Start()
        {
            InvokeRepeating("checkActing", 10.0f, 10.0f);
            InvokeRepeating("checkEvolving", 1.0f, 1.0f);
            if (insect != null)
            {
                consumer.Initialize(insect);
                consumer.StartInvoking();
            }
        }

        private void checkEvolving()
        {
            if (consumer.Full)
            {
                if (insect.currentNumber < insect.maximumNumber)
                    Growth();
            }
        }

        private void Growth()
        {
            insect.currentNumber *= 2;
            if (insect.currentNumber > insect.maximumNumber)
                insect.currentNumber = insect.maximumNumber;
            consumer.updateRates(insect);
        }

        private void checkActing()
        {
            Collider[] colliders = Physics.OverlapSphere(position, radius);
            foreach (Collider col in colliders)
            {
                foreach (InsectAction action in insect.actions)
                {
                    var interactable = col.GetComponent<IInteractableInsect>();
                    if (interactable!= null)
                    {
                        if (!consumer.Full)
                            interactable.insectInteract(action, consumer);
                    }
                }
            }
        }
    }
}