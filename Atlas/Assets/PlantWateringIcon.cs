using Game.ResourcesManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantWateringIcon : MonoBehaviour
{
    [SerializeField]
    public Image    waterDrop;
    public Canvas   waterIcon;
    public ResourcesStock plantStock;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        InvokeRepeating("UpdateWaterFillAmount", 0f, 2f);
        waterIcon.enabled = true;
    }

    private void OnDisable()
    {
        waterIcon.enabled = false;
    }

    void UpdateWaterFillAmount()
    {
        Stock stockw = null;
        foreach (Stock stock in plantStock.ListOfStocks)
        {
            if (stock.Resource == Resource.Water)
            {
                stockw = stock;
                break;
            }
        }
        if (stockw != null)
            setWaterDropResource(stockw.Quantity / stockw.Limit);
    }

    void setWaterDropResource(float amount)
    {
        waterDrop.fillAmount = amount;
    }
}
