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
        [Header("Object")]
        [SerializeField] private ItemAbstract _reward;

        [Header("Text")]
        [SerializeField] private LocalizedFormatText formatText;

        public Reward(ItemAbstract reward, LocalizedFormatText formatText)
        {
            _reward = reward;
            this.formatText = formatText;
        }

        public ItemAbstract reward => _reward;
        public String Name => formatText.Format(_reward.Name);
    }
}