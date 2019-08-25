using Game.Inventory;
using Game.Item;
using UnityEngine;
using System;

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
            }
            #endregion

            #region Public Accessors
            public Ingredient[] Ingredients
            {
                get { return _Ingredients; }
                private set { }
            }

            public Product Produced
            {
                get { return _Product; }
                private set { }
            }

            public RecipeCategory Category
            {
                get { return _Category; }
                private set { }
            }

            public bool isUnlocked
            {
                get { return _Unlocked; }
                private set { }
            }
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