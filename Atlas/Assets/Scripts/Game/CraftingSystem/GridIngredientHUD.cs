using System;
using System.Collections;
using System.Collections.Generic;
using Game.Crafting;
using UnityEngine;

namespace Menu.Crafting
{
    public class GridIngredientHUD : MonoBehaviour
    {
        private List<IngredientHUD> _slots = new List<IngredientHUD>();
        private Recipe _recipe = null;

        private void OnEnable()
        {
            _slots = new List<IngredientHUD>(GetComponentsInChildren<IngredientHUD>(true));
        }

        public void SetRecipe(Recipe recipe)
        {
            _recipe = recipe;
            LoadThisRecipeIngredients();
        }

        public void LoadThisRecipeIngredients()
        {
            int listSize = _slots.Count;
            int currentSlot = 0;
            
            foreach (IngredientHUD ingredientHud in _slots)
            {
                ingredientHud.gameObject.SetActive(false);
            }
            
            foreach (Recipe.Ingredient ingredient in _recipe.Ingredients)
            {
                IngredientHUD slot = _slots[currentSlot];
                slot.gameObject.SetActive(currentSlot < listSize && _recipe != null);
                if (currentSlot < listSize)
                {
                    slot.SetIngredient(ingredient);
                    ++currentSlot;
                }
                else
                {
                    break;
                }
            }
        }
    }
}