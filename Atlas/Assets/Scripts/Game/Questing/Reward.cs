using System;
using Game.Item;
using JetBrains.Annotations;
using Localization;
using UnityEngine;

namespace Game.Questing
{
    [Serializable]
    public struct Reward
    {
        [SerializeField] private LocalizedFormatText formatText;
        [SerializeField] private ItemAbstract _reward;
        [SerializeField] private int count;
        
        public Reward(LocalizedFormatText formatText, ItemAbstract reward, int count)
        {
            this.formatText = formatText;
            _reward = reward;
            this.count = count;
        }

        public ItemAbstract reward => _reward;
        public String Name => formatText.Format(_reward.Name);

        public int Count => count;
    }
}