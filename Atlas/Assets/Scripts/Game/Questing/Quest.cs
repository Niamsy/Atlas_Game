using System;
using JetBrains.Annotations;
using Localization;
using UnityEngine;

namespace Game.Questing
{
    [CreateAssetMenu(fileName = "QuestData", menuName = "Questing/Quest")]
    public class Quest : AData
    {
        [Header("Quest Data")] 
        [SerializeField] private LocalizedText _name;
        [SerializeField] private LocalizedText _description;

        [Header("Requirements")]
        [SerializeField] private Requirement[] requirements = new Requirement[0];

        [Header("Rewards")] [SerializeField] private int _xp = 0;
        [SerializeField] private Reward[] _rewards = new Reward[0];
        
        public LocalizedText Name => _name;
        public Requirement[] Requirement => requirements;
        public LocalizedText Description => _description;
        public Reward[] Rewards => _rewards;
        public int Xp => _xp;
    }
}