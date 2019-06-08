using System.Collections;
using System.Collections.Generic;
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
                [Range(1, 1000)]
                public int RequiredQuantity;
            }

            [Serializable]
            public class Product
            {
                public ItemAbstract Item;
                [Range(1, 1000)]
                public int ProducedQuantity;
            }
            #endregion

            #region Public Accessors
            public Ingredient[] Ingredients
            {
                get { return _Ingredients; }
                private set { }
            }

            public Product[] Products
            {
                get { return _Products; }
                private set { }
            }

            public RecipeCategory Category
            {
                get { return _Category; }
                private set { }
            }
            #endregion

            #region Private Properties
            [Header("Recipe properties")]
            [SerializeField]
            private Ingredient[] _Ingredients;
            [SerializeField]
            private Product[] _Products;
            [SerializeField]
            private RecipeCategory _Category;
            #endregion

            #region Public Methods
            public override bool CanUse(Transform transform)
            {
                return true;
            }

            public override void Use(ItemStack selfStack)
            {
                Debug.Log("RECIP : " + this.Name + " LEARNED.");
            }
            #endregion
        }
    }
}