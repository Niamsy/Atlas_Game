using JetBrains.Annotations;
using Localization;
using UnityEngine;

namespace Game.Questing
{
    [CreateAssetMenu(fileName = "Objective", menuName = "Questing/Objective")]
    public class Objective : AData
    {
        [Header("Quest Data")] 
        [SerializeField] private LocalizedText _goal;
        
        [Header("Objectives Data")]
        [SerializeField] private Requirement[] requirements = new Requirement[0];
        
        public LocalizedText Goal => _goal;
        public Requirement[] Requirements => requirements;
    }
}