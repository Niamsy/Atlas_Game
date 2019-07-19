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

        private Recipe _currentRecipe = null;

        public Recipe Recipe
        {
            get { return _currentRecipe; }
            private set { }
        }

        void Start()
        {

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

            _blueprintImage.sprite = _currentRecipe.Sprite;
            _blueprintName.text = _currentRecipe.Name;
            _blueprintDescription.text = _currentRecipe.Description;
        }
    }
}