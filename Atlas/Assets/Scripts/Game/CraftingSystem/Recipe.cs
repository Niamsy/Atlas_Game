﻿using Game.Inventory;
using Game.Item;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering.HDPipeline;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

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
                [Range(1, 999)]
                public int ProducedQuantity;

                [Serializable]
                public class ProductEvent : UnityEvent<Product>
                {
                }
                
                private readonly UnityEvent<Product> _onEnd = new ProductEvent(); 
                
                public Product GetClone(int duration)
                {
                    var clone = new Product();
                    clone.Item = Item; // TODO: check if deep cloning here could be required
                    clone.Position = Position;
                    clone.ProducedQuantity = ProducedQuantity;
                    clone.TimeRemaining = duration;
                    return clone;
                }
                
                public float TimeRemaining { get; private set; }

                public bool IsFinished => TimeRemaining <= 0;

                public int Position { get; set; }

                public void Start(Recipe recipe, int position)
                {
                    TimeRemaining = recipe.Duration;
                    Position = position;
                }

                public void Update()
                {
                    if (TimeRemaining <= 0) return;
                    TimeRemaining -= Time.deltaTime;
                }

                public void ClearListeners()
                {
                    _onEnd.RemoveAllListeners();
                }
            }
            #endregion

            #region Public Accessors
            public Ingredient[] Ingredients => ingredients;
            public Product Produced => product;
            public RecipeCategory Category => category;
            public bool isUnlocked => _unlocked;
            public int Duration => craftingDuration;
            #endregion

            #region Public Properties
            public delegate void RecipeUpdate(Recipe recipe);
            public event RecipeUpdate OnRecipeUpdate;
            #endregion

            #region Private Properties
            [Header("Recipe properties")]
            [SerializeField] private RecipeCategory category;
            [SerializeField][Tooltip("Duration in seconds for the crafting of the products")] private int craftingDuration;
            [Space(10)][SerializeField] private Ingredient[] ingredients;
            [Space(10)][SerializeField] private Product product;
            private bool _unlocked = false;

            #endregion

            #region Public Methods
            public override bool CanUse(Transform transform)
            {
                return true;
            }

            public override void Use(ItemStack selfStack)
            {
                Debug.Log("RECIPE : " + Name + " LEARNED.");
            }

            public void Unlock(bool shouldUnlock)
            {
                _unlocked = shouldUnlock;
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