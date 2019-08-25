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
            //gameObject.SetActive(_currentRecipe != null);
            
            if (_currentRecipe == null) return;

            if (_ingredients)
                _ingredients.SetRecipe(_currentRecipe);
            
            _blueprintImage.sprite = _currentRecipe.Sprite;
            _blueprintName.text = _currentRecipe.Name;
            _blueprintDescription.text = _currentRecipe.Description;
        }
    }
}