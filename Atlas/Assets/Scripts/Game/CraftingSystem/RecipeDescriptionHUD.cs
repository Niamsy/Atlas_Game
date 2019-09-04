﻿using UnityEngine;
using UnityEngine.UI;
using Game.Crafting;
using TMPro;

namespace Menu.Crafting
{
    public class RecipeDescriptionHUD : MonoBehaviour
    {
        [SerializeField] private Image _recipeImage;
        [SerializeField] private TextMeshProUGUI _recipeName;
        [SerializeField] private TextMeshProUGUI _recipeDescription;
        [SerializeField] private TextMeshProUGUI _recipeAvailability;

        [Header("Availability Info")]
        [SerializeField] private Localization.LocalizedText _availableLocalizedText;
        [SerializeField] private Localization.LocalizedText _unavailableLocalizedText;
        [SerializeField] private Color _availableLocalizedTextColor;
        [SerializeField] private Color _unavailableLocalizedTextColor;

        private Recipe _currentRecipe = null;

        public Recipe Recipe => _currentRecipe;

        public void Reset()
        {
            SetRecipe();
        }

        public void SetRecipe(Transform recipeTransform = null, Recipe recipe = null)
        {
            _currentRecipe = recipe;

            UpdateDescription(recipeTransform);
        }

        public void UpdateDescription(Transform recipeTransform)
        {
            gameObject.SetActive(_currentRecipe != null);

            if (_currentRecipe == null) return;

            _recipeImage.sprite = _currentRecipe.Sprite;
            _recipeName.text = _currentRecipe.Name;
            _recipeDescription.text = _currentRecipe.Description;
            _recipeAvailability.text = _currentRecipe.isUnlocked ? _availableLocalizedText : _unavailableLocalizedText;
            _recipeAvailability.color = _currentRecipe.isUnlocked ? _availableLocalizedTextColor : _unavailableLocalizedTextColor;

            if (!recipeTransform) return;
            
            var position = recipeTransform.position;
            transform.SetPositionAndRotation(new Vector3(position.x + 32, position.y - 10) , recipeTransform.rotation);
        }
    }
}