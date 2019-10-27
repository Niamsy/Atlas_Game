using System;
using Game.Item;
using Localization;
using UnityEngine;

namespace Game.Questing
{
    [Serializable]
    public struct Requirement
    {
       [SerializeField] private Condition condition;
       [SerializeField] private ItemAbstract argument;
       [SerializeField] private LocalizedText description;
       [SerializeField] private int count;

       public Requirement(Condition condition, ItemAbstract argument, int count, LocalizedText description)
        {
            this.condition = condition;
            this.argument = argument;
            this.count = count;
            this.description = description;
        }

        public string Description => description;
        public int Count => count;
        public Condition Condition => condition;
        public ItemAbstract Argument => argument;

        public bool Validate(Condition other, ItemAbstract item)
        {
            return other.Id == Condition.Id && item.Id == Argument.Id;
        }
    }
}