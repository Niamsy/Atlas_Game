using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Crafting;
using UnityEngine.Events;
using Variables;

namespace Menu.Crafting
{
    public class GridCraftingHUD : MonoBehaviour
    {
        [SerializeField] private FloatVariable _PlayerLevel;
        private List<RecipeHUD> _slots = new List<RecipeHUD>();
        

        private void OnEnable()
        {
            _slots = new List<RecipeHUD>(GetComponentsInChildren<RecipeHUD>(true));
        }

        public void LoadThisBook(RecipeBook book, UnityAction<Recipe> recipeSelectedCb)
        {
            int listSize = _slots.Count;
            int currentSlot = 0;

            foreach (RecipeBook.Chapter chapter in book.Chapters)
            {
                foreach (Recipe recipe in chapter.Recipes)
                {
                    RecipeHUD slot = _slots[currentSlot];
                    slot.gameObject.SetActive(currentSlot < listSize);
                    
                    if (currentSlot >= listSize) break;
                    slot.SetRecipe(recipe, (int)_PlayerLevel.Value >= chapter.RequiredLevel, recipeSelectedCb);
                    ++currentSlot;
                }
                if (currentSlot >= listSize) break;
            }
        }

        public void Unload()
        {
            foreach (var recipeHud in _slots)
            {
                recipeHud.SetRecipe(null, false, null);
            }
        }
    }
}