using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Game.Crafting;
using UnityEngine;

public class GridProducingHUD : MonoBehaviour
{
    private List<ProductHUD> _products = new List<ProductHUD>();
    
    // Start is called before the first frame update
    private void OnEnable()
    {
        _products = new List<ProductHUD>(GetComponentsInChildren<ProductHUD>());
    }

    public void Set(List<Recipe.Product> products)
    {
        
    }

    public void push(Recipe.Product product, int slot)
    {
        
    }

    public void remove(Recipe.Product product, int slot)
    {
        
    }
}
