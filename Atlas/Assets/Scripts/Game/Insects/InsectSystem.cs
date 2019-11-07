using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Game.Insects
{
    [Serializable,
        CreateAssetMenu(fileName = "Insect", menuName ="Insect/System", order = 1)]
    public class InsectSystem : ScriptableObject
    {
        [SerializeField]
        public List<InsectAction> actions;

        [SerializeField]
        public int maximumNumber;

        public int currentNumber = 2;
    }
}
