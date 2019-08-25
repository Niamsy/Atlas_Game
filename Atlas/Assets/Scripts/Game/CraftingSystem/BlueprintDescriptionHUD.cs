using UnityEngine;
using UnityEngine.UI;
using Game.Crafting;
using TMPro;

namespace Menu.Crafting
{
    public class BlueprintDescriptionHUD : MonoBehaviour
    {
        [SerializeField] private Image _blueprintImage;
        [SerializeField] private Text _blueprintName;
        [SerializeField] private Text _blueprintDescription;
        [SerializeField] private Text _blueprintDuration;
        private GridIngredientHUD _ingredients;
        
        private Recipe _currentRecipe = null;

        public Recipe Recipe
        {
            get { return _currentRecipe; }
            private set { }
        }

        void OnEnable()
        {
            _ingredients = GetComponentInChildren<GridIngredientHUD>();
        }

        public void Reset()
        {
            SetRecipe();
        }

        public void SetRecipe(Recipe recipe = null)
        {
            _currentRecipe = recipe;

            UpdateDescription();
        }

        public void UpdateDescription()
        {
            if (_currentRecipe == null) return;

            if (_ingredients)
                _ingredients.SetRecipe(_currentRecipe);
            
            _blueprintImage.sprite = _currentRecipe.Sprite;
            _blueprintName.text = _currentRecipe.Name;
            _blueprintDescription.text = _currentRecipe.Description;

            int duration = _currentRecipe.Duration;
            int hour = duration / 3600;
            duration -= 3600 * hour;
            int minute = duration / 60;
            duration -= 60 * minute;

            _blueprintDuration.text = (hour != 0 ? hour + " h " : "") +
                                      (minute != 0 ? minute + " min " : "") +
                                      (duration != 0 ? duration + " sec" : "");
        }
    }
}