using UnityEngine;
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
        // Update is called once per frame

        private Recipe _currentRecipe = null;

        public Recipe Recipe {
            get { return _currentRecipe; }
            private set { }
        }


        void Update()
        {

        }

        public void Reset()
        {
            SetRecipe();
        }

        public void SetRecipe(Transform recipTransform = null, Recipe recipe = null)
        {
            _currentRecipe = recipe;

            UpdateDescription(recipTransform);
        }

        public void UpdateDescription(Transform recipTransform)
        {
            gameObject.SetActive(_currentRecipe != null);

            if (_currentRecipe == null) return;

            _recipeImage.sprite = _currentRecipe.Sprite;
            _recipeName.text = _currentRecipe.Name;
            _recipeDescription.text = _currentRecipe.Description;
            _recipeAvailability.text = _currentRecipe.isUnlocked ? _availableLocalizedText : _unavailableLocalizedText;
            _recipeAvailability.color = _currentRecipe.isUnlocked ? _availableLocalizedTextColor : _unavailableLocalizedTextColor;

            if (recipTransform)
                transform.SetPositionAndRotation(new Vector3(recipTransform.position.x + 32, recipTransform.position.y - 10) , recipTransform.rotation);
        }
    }
}