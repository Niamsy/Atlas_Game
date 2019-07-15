using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Localization;

namespace Game.Crafting
{
    [CreateAssetMenu(menuName = "Crafting/Recipe Category", order = 2)]
    public class RecipeCategory : ScriptableObject
    {
        [SerializeField]
        private LocalizedText _Name = null;

        public string Name
        {
            get {
                if (_Name)
                {
                    return _Name.Value;
                }
                return null;
            }
        }
    }
}