using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Game.Crafting;
using Localization;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Menu.Crafting
{
    public class ProductDescriptionHUD : MonoBehaviour
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private TextMeshProUGUI itemDescription;
        [SerializeField] private TextMeshProUGUI itemQuantity;
        [SerializeField] private LocalizedText over;
        
        private Recipe.Product _currentItem = null;

        public Recipe.Product Item => _currentItem;

        public void Reset()
        {
            SetItem();
        }

        public void SetItem(Transform itemTransform = null, Recipe.Product product = null)
        {
            _currentItem = product;

            UpdateDescription(itemTransform);
        }

        public void UpdateDescription(Transform itemTransform)
        {
            gameObject.SetActive(_currentItem != null);

            if (_currentItem == null)
            {
                return;
            }

            itemImage.sprite = _currentItem.Item.Sprite;
            itemName.text = _currentItem.Item.Name;
            itemDescription.text = _currentItem.Item.Description;
            itemQuantity.text = _currentItem.TimeRemaining.ToString(CultureInfo.CurrentCulture);
            
            if (!itemTransform) return;
            
            var position = itemTransform.position;
            transform.SetPositionAndRotation(new Vector3(position.x + 32, position.y - 10) , itemTransform.rotation);
        }

        private void Update()
        {
            if (_currentItem == null) return;

            if (_currentItem.IsFinished || _currentItem.TimeRemaining < 1)
            {
                itemQuantity.text = over;
                return;
            }
            
            int duration = (int)_currentItem.TimeRemaining;
            int hour = duration / 3600;
            duration -= 3600 * hour;
            int minute = duration / 60;
            duration -= 60 * minute;

            itemQuantity.text = (hour != 0 ? hour + " h " : "") +
                                (minute != 0 ? minute + " min " : "") +
                                (duration != 0 ? duration + " sec" : "");
        }
    }
}