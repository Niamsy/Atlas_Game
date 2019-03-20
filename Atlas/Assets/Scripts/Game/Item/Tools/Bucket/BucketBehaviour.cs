using System.Collections;
using System.Collections.Generic;
using Plants;
using UnityEngine;

namespace Game.Item.Tools
{
    public class BucketBehaviour : MonoBehaviour
    {
        public enum Status
        {
            Default,
            Filling,
            Watering
        }
        
        public Producer Producer;

        public Status State { get; private set; } 
        
        private void Awake()
        {
            Producer.gameObject.SetActive(false);
        }
        
        public void SetState(Status newState)
        {
            if (newState != State)
            {
                State = newState;
                
                Producer.gameObject.SetActive(State == Status.Watering);
            }
        }

        private readonly float _countdownStep = 0.1f;
        private float _countDown = 0;
        private IEnumerator WateringCoroutine()
        {
            SetState(Status.Watering);    
            while (_countDown > 0)
            {
                yield return new WaitForSeconds(_countdownStep);
                _countDown -= _countdownStep;
            }
            SetState(Status.Default);    
        }

        public void Watering()
        {
            _countDown = _countdownStep * 4f;
            if (State != Status.Watering)
                StartCoroutine(WateringCoroutine());
        }
    }
}
