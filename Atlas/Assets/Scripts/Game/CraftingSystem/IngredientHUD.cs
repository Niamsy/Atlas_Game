﻿using System.Collections;
using System.Collections.Generic;
using Game.Crafting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Menu.Crafting
{
    public class IngredientHUD : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Text quantityText = null;
        [SerializeField] private Image _image = null;
        private Recipe.Ingredient _ingredient = null;
        
        // Start is called before the first frame update
        void Start()
        {
            quantityText = GetComponentInChildren<Text>();
        }

        public void SetIngredient(Recipe.Ingredient ingredient)
        {
            _ingredient = ingredient;
            UpdateContent();
        }
        
        public void UpdateContent()
        {
            if (_image)
            {
                _image.enabled = _ingredient?.Item.Sprite != null;
                _image.sprite = _ingredient?.Item.Sprite;
            }

            if (quantityText)
            {
                quantityText.text = _ingredient?.RequiredQuantity.ToString();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
        }

        public void OnPointerExit(PointerEventData eventData)
        {
        }
    }
}