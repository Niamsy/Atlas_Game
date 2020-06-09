using System;
using System.Security.Permissions;
using JetBrains.Annotations;
using Localization;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Questing
{
    [CreateAssetMenu(fileName = "QuestData", menuName = "Questing/Quest")]
    public class Quest : AData
    {
        [Header("Quest Data")] 
        [SerializeField] private LocalizedText _name = null;
        [SerializeField] private LocalizedText _description = null;
        
        [FormerlySerializedAs("requirements")]
        [Header("Requirements")]
        [SerializeField] private Requirement[] requirementses = new Requirement[0];

        [Header("Rewards")]
        [SerializeField] private int _xp = 0;
        [SerializeField] private Reward[] _rewards = new Reward[0];
        [SerializeField] public GameObject toSpawn;
        [SerializeField] public Transform spawnPoint;

        [Header("Objective")] 
        [SerializeField] private bool isObjective = false;
        
        public LocalizedText Name => _name;
        public Requirement[] Requirements => requirementses;
        public LocalizedText Description => _description;
        public Reward[] Rewards => _rewards;
        public int Xp => _xp;
        public bool IsObjective => isObjective;
    }
}