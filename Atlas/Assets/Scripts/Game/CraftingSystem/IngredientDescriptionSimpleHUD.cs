using System.Collections;
using System.Collections.Generic;
using Game.Crafting;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Menu.Crafting
{
    public class IngredientDescriptionSimpleHUD : MonoBehaviour
    {
        [SerializeField] private Image _itemImage = null;
        [SerializeField] private TextMeshProUGUI _itemName = null;
        [SerializeField] private TextMeshProUGUI _itemDescription = null;
        [SerializeField] private TextMeshProUGUI _itemQuantity = null;
        [SerializeField] private Localization.LocalizedText _required = null;
        
        private Recipe.Ingredient _currentItem = null;

        public Recipe.Ingredient Item => _currentItem;

        public void Reset()
        {
            SetItem();
        }

        public void SetItem(Transform itemTransform = null, Recipe.Ingredient ingredient = null)
        {
            _currentItem = ingredient;

            UpdateDescription(itemTransform);
        }

        public void UpdateDescription(Transform itemTransform)
        {
            gameObject.SetActive(_currentItem != null);

            if (_currentItem == null) return;

            _itemImage.sprite = _currentItem.Item.Sprite;
            _itemName.text = _currentItem.Item.Name;
            _itemDescription.text = _currentItem.Item.Description;
            _itemQuantity.text = _required + _currentItem.RequiredQuantity;
            
            if (!itemTransform) return;
            
            var position = itemTransform.position;
            transform.SetPositionAndRotation(new Vector3(position.x + 32, position.y - 10) , itemTransform.rotation);
        }
    }
}