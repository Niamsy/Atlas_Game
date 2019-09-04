using System.Collections.Generic;
using Game.Crafting;
using UnityEngine;
using UnityEngine.Events;

public class GridProducingHUD : MonoBehaviour
{
    private List<ProductHUD> _products = new List<ProductHUD>();
    private UnityAction<Recipe.Product, int> _cb;
    
    // Start is called before the first frame update
    private void OnEnable()
    {
        _products = new List<ProductHUD>(GetComponentsInChildren<ProductHUD>());
    }

    public void SetClickCb(UnityAction<Recipe.Product, int> cb)
    {
        _cb = cb;
    }

    public void SetProducts(Recipe recipe, List<Recipe.Product> products)
    {
        int pos = 0;
        int size = _products.Count;

        foreach (var hud in _products)
        {
            hud.Reset();
        }
        
        foreach (var product in products)
        {
            _products[pos].SetProduct(recipe, product, pos, _cb);
            ++pos;
            if (pos >= size) break;
        }
    }
}
