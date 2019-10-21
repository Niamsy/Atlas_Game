using System;
using Game.Item;
using UnityEngine;

namespace Game.Questing
{
    [Serializable]
    public struct Requirement
    {
       [SerializeField] private Condition condition;
       [SerializeField] private ItemAbstract argument;
       [SerializeField] private int count;

       public Requirement(Condition condition, ItemAbstract argument, int count)
        {
            this.condition = condition;
            this.argument = argument;
            this.count = count;
        }
       
        public string Description => Condition.Format.Format(Argument.Name);
        public int Count => count;
        public Condition Condition => condition;
        public ItemAbstract Argument => argument;

        public bool Validate(Condition other, ItemAbstract item)
        {
            return other == Condition && item.Id == Argument.Id;
        }
    }
}