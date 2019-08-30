using System;
using Game.Crafting;
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
    [SerializeField] private Image _image;

    public void OnEnable()
    {
    }

    public void SetProduct(Recipe.Product product, 
        int position, 
        UnityAction<Recipe.Product, int> onClick)
    {
        _product = product;
        _position = position;
        _onClick = onClick;
        UpdateContent(_product);
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
        // TODO update time here
    }
    
    public void UpdateContent(Recipe.Product product)
    {
        _image.enabled = product != null;
        _image.color = product != null ? Color.white : Color.clear;   
        
        if (!_image || product == null) return;
        
        _image.preserveAspect = true;
        _image.sprite = product.Item.Sprite;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _onClick?.Invoke(_product, _position);
    }
}
