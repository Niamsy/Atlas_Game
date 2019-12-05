using System.Collections.Generic;
using System.Linq;
using Game.Inventory;
using Game.Questing;
using Game.SavingSystem;
using Player;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Crafting
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(CraftingSaver))]
    public class Crafter : AInteractable
    {
        [Header("Crafting")]
        [SerializeField] private RecipeBook _Book = null;

        [Header("UI")]
        [SerializeField] private CraftingMenuHUD _craftingHUD = null;
        private Collider _collider;
        private Canvas _guiCanvas;
        private List<Recipe.Product> _productsOngoing = new List<Recipe.Product>();
        private List<Recipe.Product> _productsFinished = new List<Recipe.Product>();
        private BaseInventory _inventory;
        private UnityAction<Recipe.Product, int> _productFinishedCB;
        
        [Tooltip("Camera to lock when the crafting menu is open")]
        [SerializeField] private CinemachineFreeLookInputConversion _camera = null;
        private bool isShown;

        [Header("Questing")]
        [Tooltip("Event raised when something have been crafted")]
        [SerializeField] private ConditionEvent _conditionEvent = null;
        [SerializeField] private Condition _raisedCondition = null;
        
        public RecipeBook RecipeBook => _Book;

        public BaseInventory Inventory => _inventory;

        public List<Recipe.Product> ProductsOngoing => _productsOngoing;

        public List<Recipe.Product> ProductsFinished => _productsFinished;

        private void Start()
        {
            if (_Book == null)
                Debug.LogWarning("No Recipe book setup up on Crafter");
            _guiCanvas = gameObject.GetComponentInChildren<Canvas>();
            if (_guiCanvas)
                _guiCanvas.gameObject.SetActive(false);
            _toRemove = new List<int>();
        }

        private void triggerCanvas(bool visible)
        {
            Color colName = _hidedCanvasName.color;
            Color colUsage = _hidedCanvasUsage.color;
            colName.a = 0;
            colUsage.a = 0;
            if (visible)
            {
                colName.a = 255;
                colUsage.a = 255;
            }
            _hidedCanvasUsage.color = colUsage;
            _hidedCanvasName.color = colName;
        }

        public override void Interact(PlayerController playerController)
        {
            isShown = !isShown;
            if (isShown)
            {
                _camera.lockCamera = true;
                _inventory = playerController.Inventory;
                _craftingHUD.SetCrafter(this);
            }
            else
            {
                _camera.lockCamera = false;
                _inventory = null;
                _craftingHUD.UnsetCrafter(this);
            }
            _craftingHUD.Show(isShown, true);
        }

        public void SetOnProductFinishedCb(UnityAction<Recipe.Product, int> cb)
        {
            _productFinishedCB = cb;
        }

        private List<int> _toRemove = new List<int>();
        private void Update()
        {
            foreach (var production in _productsOngoing)
            {
                production.Update();
                
                if (!production.IsFinished) continue;
                _toRemove.Add(production.Position);
            }

            foreach (var i in _toRemove)
            {
                OnProductEnd(_productsOngoing[i]);
            }
            
            _toRemove.Clear();
        }

        protected override void OnTriggerEnter(Collider col)
        {
            if (!col.gameObject.CompareTag("Player")) return;
            if (_guiCanvas && _guiCanvas.gameObject)
            {
                _guiCanvas.gameObject.SetActive(true);
            }
            if (_hidedCanvasName != null && _hidedCanvasUsage != null)
                triggerCanvas(true);
            //SaveManager.Instance.InputControls.Player.Interact.performed += _craftingHUD.OpenCloseCraftingMenu;
        }

        protected override void OnTriggerExit(Collider col)
        {
            if (!col.gameObject.CompareTag("Player")) return;
            if (_guiCanvas)
            {
                _craftingHUD.Show(false);
                isShown = false;
                _camera.lockCamera = false;
                _guiCanvas.gameObject.SetActive(false);
            }
            if (_hidedCanvasName != null && _hidedCanvasUsage != null)
                triggerCanvas(false);
            //SaveManager.Instance.InputControls.Player.Interact.performed += _craftingHUD.OpenCloseCraftingMenu;
        }

        
        public bool CanProduce(Recipe recipe, BaseInventory inventory)
        {
            return recipe.Ingredients.All(ingredient => inventory.HasEnoughItems(ingredient.Item, ingredient.RequiredQuantity));
        }

        private void UpdatePositions(List<Recipe.Product> products)
        {
            for (int i = 0; i < products.Count; i++)
            {
                products[i].Position = i;
            }
        }

        private void OnProductEnd(Recipe.Product product)
        {
            _productsFinished.Add(_productsOngoing[product.Position]);
            _productsOngoing.RemoveAt(product.Position);
            UpdatePositions(_productsOngoing);
            UpdatePositions(_productsFinished);
            _productFinishedCB?.Invoke(product, product.Position);
            product.ClearListeners();
        }

        public Recipe.Product GetFinishedProduct(int position)
        {
            if (position < 0 || position >= _productsFinished.Count)
            {
                Debug.LogWarning("Trying to access position " + position + " in Crafter finished product list of size " + _productsFinished.Count);
                return null;
            }

            var product = _productsFinished[position];
            _conditionEvent.Raise(_raisedCondition, product.Item, 1);
            _productsFinished.RemoveAt(position);
            return product;
        }

        public void CancelProduction(int position)
        {
            if (position < 0 || position >= _productsOngoing.Count)
            {
                Debug.LogWarning("Trying to access position " + position + " in Crafter production list of size " + _productsOngoing.Count);
                return;
            }
            
            _productsOngoing.RemoveAt(position);
            UpdatePositions(_productsOngoing);
        }
        
        public bool Produce(Recipe recipe, BaseInventory inventory)
        {   
            if (!recipe.Ingredients.All(ingredient =>
                    inventory.DestroyFirsts(ingredient.Item, ingredient.RequiredQuantity))) return false;

            var product = recipe.Produced.GetClone(recipe.Duration);
            _productsOngoing.Add(product);
            product.Start(recipe.Duration, recipe.Duration,_productsOngoing.Count - 1);
            return true;
        }
    }
}