using Game.Inventory;
using Game.Item;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering.HDPipeline;
using UnityEngine.UIElements;

namespace Game
{
    namespace Crafting
    {
        [Serializable,
        CreateAssetMenu(menuName = "Crafting/Recipe", order = 1)]
        public class Recipe : ItemAbstract
        {
            #region Public Classes

            [Serializable]
            public class Ingredient
            {
                public ItemAbstract Item;
                [UnityEngine.Range(1, 999)]
                public int RequiredQuantity;
            }

            [Serializable]
            public class Product
            {
                public ItemAbstract Item;
                [UnityEngine.Range(1, 999)]
                public int ProducedQuantity;

                [Serializable]
                public class ProductEvent : UnityEvent<Product>
                {
                }
                
                private int _timeRemaining;
                private float _lastTime;
                private readonly UnityEvent<Product> _onEnd = new ProductEvent(); 
                public int TimeRemaining => _timeRemaining;
                public bool IsFinished => _timeRemaining == 0;

                public void AddListenerOnEnd(UnityAction<Product> OnEndAction)
                {
                    _onEnd.AddListener(OnEndAction);
                }
                
                public void Start(Recipe recipe)
                {
                    _timeRemaining = recipe.Duration;
                    _lastTime = Time.time;
                }

                public void Update()
                {
                    if (_timeRemaining <= 0) return;
                    _timeRemaining -= (int) (Time.time - _lastTime);
                    _lastTime = Time.time;
                    if (_timeRemaining <= 0)
                        _onEnd.Invoke(this);
                }
            }
            #endregion

            #region Public Accessors
            public Ingredient[] Ingredients => _Ingredients;
            public Product Produced => _Product;
            public RecipeCategory Category => _Category;
            public bool isUnlocked => _Unlocked;
            public int Duration => CraftingDuration;
            #endregion

            #region Public Properties
            public delegate void RecipeUpdate(Recipe recipe);
            public event RecipeUpdate OnRecipeUpdate;
            #endregion

            #region Private Properties
            [Header("Recipe properties")]
            [SerializeField] private RecipeCategory _Category = null;
            [SerializeField][Tooltip("Duration in seconds for the crafting of the products")] private int CraftingDuration = 0;
            [Space(10)][SerializeField] private Ingredient[] _Ingredients = null;
            [Space(10)][SerializeField] private Product _Product = null;
            private bool _Unlocked = false;

            #endregion

            #region Public Methods
            public override bool CanUse(Transform transform)
            {
                return true;
            }

            public override void Use(ItemStack selfStack)
            {
                Debug.Log("RECIPE : " + this.Name + " LEARNED.");
            }

            public void Unlock(bool shouldUnlock)
            {
                _Unlocked = shouldUnlock;
                FireEvent();
            }
            #endregion

            #region Private Methods
            private void FireEvent()
            {
                OnRecipeUpdate?.Invoke(this);
            }
            #endregion
        }
    }
}