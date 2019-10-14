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

        public string Description => condition.Format.Format(argument);
        public int Count => count;

        public bool Validate(Condition other, ItemAbstract item)
        {
            return other == condition && item.Id == argument.Id;
        }
    }
}