using System;
using UnityEngine;

namespace Game.Questing
{
    public class LiveRequirement
    {
        public Requirement Requirement;
        public int CurrentlyAccomplished;
        public delegate void OnValueChanged();

        public OnValueChanged IncrementDelegate;
        
        public LiveRequirement(Requirement requirement, int currentlyAccomplished)
        {
            Requirement = requirement;
            CurrentlyAccomplished = currentlyAccomplished;
        }

        public void IncrementAccomplished(int value)
        {
            if (CurrentlyAccomplished + value > Requirement.Count)
            {
                CurrentlyAccomplished = Requirement.Count;
            }
            else
            {
                CurrentlyAccomplished += value;
            }
            IncrementDelegate?.Invoke();
        }
    }
}