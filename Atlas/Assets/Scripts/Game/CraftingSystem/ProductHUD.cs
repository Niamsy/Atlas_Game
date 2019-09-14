using System;
using Game.Crafting;
using Menu.Crafting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ProductHUD : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // Start is called before the first frame update

    private Recipe.Product _product;
    private int _position;
    private UnityAction<Recipe.Product, int> _onClick;
    private float _originalDuration;
    [SerializeField] private Image image;
    [SerializeField] private Image cooldown;
    [SerializeField] private ProductDescriptionHUD description;

    public void OnEnable()
    {
    }

    public void SetProduct(Recipe recipe,
        Recipe.Product product, 
        int position, 
        UnityAction<Recipe.Product, int> onClick)
    {
        _product = product;
        _position = position;
        _onClick = onClick;
        cooldown.fillAmount = 1;
        _originalDuration = product.OriginalDuration;
        UpdateContent(_product);
    }

    public void SetProduct(Recipe.Product product, int position, UnityAction<Recipe.Product, int> onClick)
    {
        _product = product;
        _position = position;
        _onClick = onClick;
        _originalDuration = product.OriginalDuration;
    }
    
    public void Reset()
    {
        _product = null;
        _position = 0;
        _onClick = null;
        UpdateContent(null);
    }

    private void Update()
    {
        if (_product == null) return;
        cooldown.fillAmount = _product.TimeRemaining / _originalDuration;
    }
    
    public void UpdateContent(Recipe.Product product)
    {
        image.enabled = product != null;
        cooldown.enabled = product != null && !product.IsFinished;
        image.color = product != null ? Color.white : Color.clear;   
        
        if (!image || product == null) return;
        
        image.preserveAspect = true;
        image.sprite = product.Item.Sprite;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        DisplayDescription();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideDescription();
    }
    
    private void DisplayDescription()
    {
        if (description != null && _product != null)
            description.SetItem(transform, _product);
    }

    private void HideDescription()
    {
        if (description != null && _product != null &&
            description.Item.Position == _position)
            description.Reset();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        HideDescription();
        _onClick?.Invoke(_product, _position);
    }
}
