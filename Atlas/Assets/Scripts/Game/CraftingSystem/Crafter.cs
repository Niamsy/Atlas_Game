using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Crafting
{
    public class Crafter : Menu.MenuWidget
    {
        [SerializeField]
        private RecipeBook _Book = null;

        public RecipeBook RecipeBook => _Book;

        protected override void InitialiseWidget()
        {
            
        }

        private void Start()
        {
            if (_Book == null)
                Debug.LogWarning("No Recipe book setup up on Crafter");
        }
    }
}