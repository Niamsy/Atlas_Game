using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Crafting;
using Variables;

namespace Menu.Crafting
{
    public class GridCraftingHUD : MonoBehaviour
    {
        [SerializeField] private RecipeBook _RecipeBook = null;
        [SerializeField] private FloatVariable _PlayerLevel;
        private List<RecipeHUD> _Slots = new List<RecipeHUD>();
        

        private void OnEnable()
        {
            _Slots = new List<RecipeHUD>(GetComponentsInChildren<RecipeHUD>(true));
            if (_RecipeBook != null)
                LoadThisBook(_RecipeBook);
        }

        public void LoadThisBook(RecipeBook book)
        {
            int listSize = _Slots.Count;
            int currentSlot = 0;

            foreach (RecipeBook.Chapter chapter in book.Chapters)
            {
                
                foreach (Recipe recipe in chapter.Recipes)
                {
                    RecipeHUD slot = _Slots[currentSlot];
                    slot.gameObject.SetActive(currentSlot < listSize);
                    if (currentSlot < listSize)
                    {
                        slot.SetRecipe(recipe, (int)_PlayerLevel.Value >= chapter.RequiredLevel);
                        ++currentSlot;
                    }
                    else
                    {
                        break;
                    }
                }
                if (currentSlot >= listSize) break;
            }
        }
    }
}