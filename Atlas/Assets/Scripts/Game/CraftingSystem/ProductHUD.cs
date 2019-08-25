using System;
using System.Collections;
using System.Collections.Generic;
using Game.Crafting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.Rendering.HDPipeline;

public class ProductHUD : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // Start is called before the first frame update

    private Recipe.Product _product;
    private int _position;
    private bool _canBeTaken = false;
    private UnityAction<Recipe.Product, int> _onEnd;
    private UnityAction<Recipe.Product, int> _onClick;
    
    public bool CanBeTaken => _canBeTaken;

    public void OnEnable()
    {
    }

    public void SetProduct(Recipe.Product product, 
        int position, 
        UnityAction<Recipe.Product, int> onEnd,
        UnityAction<Recipe.Product, int> onClick)
    {
        _product = product;
        _position = position;
        _canBeTaken = product.IsFinished;
        _product.AddListenerOnEnd(OnProductEnd);
        _onEnd = onEnd;
        _onClick = onClick;
    }

    public void OnProductEnd(Recipe.Product product)
    {
        _onEnd.Invoke(_product, _position);
    }    
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _onClick.Invoke(_product, _position);
    }
}
