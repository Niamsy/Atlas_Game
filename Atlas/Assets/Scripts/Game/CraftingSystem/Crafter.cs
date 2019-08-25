using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Game.Crafting
{
    [RequireComponent(typeof(Collider))]
    public class Crafter : AInteractable
    {
        [SerializeField] private RecipeBook _Book = null;
        [SerializeField] private CraftingMenuHUD _craftingHUD = null;

        private Collider _collider;
        private Canvas _guiCanvas;
        private List<Recipe.Product> _productsOngoing = new List<Recipe.Product>();
        private List<Recipe.Product> _productsFinished = new List<Recipe.Product>();
        
        private bool isShown = false;
        
        public RecipeBook RecipeBook => _Book;

        private void Start()
        {
            if (_Book == null)
                Debug.LogWarning("No Recipe book setup up on Crafter");
            _guiCanvas = gameObject.GetComponentInChildren<Canvas>();
            if (_guiCanvas)
                _guiCanvas.gameObject.SetActive(false);
        }

        public override void Interact(PlayerController playerController)
        {
            isShown = !isShown;
            _craftingHUD.Show(isShown);
        }
        
        void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                if (_guiCanvas && _guiCanvas.gameObject)
                {
                    _guiCanvas.gameObject.SetActive(true);
                }
            }
        }

        void OnTriggerExit(Collider col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                if (_guiCanvas)
                    _guiCanvas.gameObject.SetActive(false);
            }
        }

        void Produce(Recipe recipe)
        {
            
        }
    }
}