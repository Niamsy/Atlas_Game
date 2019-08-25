using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Game.Crafting
{
    public class Crafter : AInteractable
    {
        [SerializeField] private RecipeBook _Book = null;
        [SerializeField] private CraftingMenuHUD _craftingHUD = null;

        private bool isShown = false;
        
        public RecipeBook RecipeBook => _Book;

        private void Start()
        {
            if (_Book == null)
                Debug.LogWarning("No Recipe book setup up on Crafter");
        }

        public override void Interact(PlayerController playerController)
        {
            isShown = !isShown;
            _craftingHUD.Show(isShown);
        }
    }
}