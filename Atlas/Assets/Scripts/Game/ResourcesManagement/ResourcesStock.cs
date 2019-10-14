using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.ResourcesManagement
{
    [Serializable]
    public class Stock
    {
        public Resource Resource;
        public int      Quantity;
        public int      Limit;
    }
    
    public class ResourcesStock : MonoBehaviour
    {
        
        /// <summary>
        /// There is only one stock per type of resources.
        /// </summary>
        [SerializeField]
        private List<Stock>         _listOfStocks = new List<Stock>();
        public List<Stock>          ListOfStocks { get { return (_listOfStocks); } }

        private Boolean iconDisplayed = false;
        private int waterQuantity = 0;

        public Stock                this[Resource resource]
        {
            get
            {
                foreach (var stock in _listOfStocks)
                {
                    if (stock.Resource == resource)
                        return (stock);
                }

                return (null);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="quantity"></param>
        /// <returns>The quantity of resource successfully added</returns>
        public int AddResources(Resource resource, int quantity)
        {
            foreach (var stock in _listOfStocks)
            {
                if (stock.Resource == resource)
                {
                    showWaterIcon(resource, stock);
                    var rest = quantity - (stock.Limit - stock.Quantity);
                    if (rest < 0)
                        rest = 0;
                    var totalAdded = quantity - rest;

                    stock.Quantity += totalAdded;
                    waterQuantity = stock.Quantity;
                    return (totalAdded);
                }
            }
            return (quantity);
        }

        private void showWaterIcon(Resource resource, Stock stock)
        {
            if (resource == Resource.Water)
            {
                var icon = this.GetComponentInParent<PlantWateringIcon>();
                if (icon != null && !iconDisplayed)
                {
                    icon.enabled = true;
                    Canvas wtr = gameObject.transform.Find("Watering").gameObject.GetComponent<Canvas>();
                    Debug.Log("Canvas Value + " + wtr);
                    icon.WaterIcon = wtr;
                    Image wtrf = wtr.transform.Find("WaterFilled").gameObject.GetComponentInChildren<Image>();
                    icon.WaterDrop = wtrf;
                    icon.PlantStock = ListOfStocks;
                    icon.StartUpdate();
                    StartCoroutine(hideWaterIcon(icon, stock, 3f));
                    iconDisplayed = true;
                }
            }
        }

        private IEnumerator hideWaterIcon(PlantWateringIcon icon, Stock stock, float delay)
        {
            yield return new WaitForSeconds(delay);
            if (waterQuantity == stock.Quantity && iconDisplayed == true)
            {
                iconDisplayed = false;
                icon.StopUpdate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="quantity"></param>
        /// <returns>The quantity successfully removed from the stock</returns>
        public int RemoveResources(Resource resource, int quantity)
        {
            foreach (var stock in _listOfStocks)
            {
                if (stock.Resource == resource)
                {
                    var rest = stock.Quantity - quantity;
                    if (rest < 0)
                        rest = 0;
                    var totalRemoved = stock.Quantity - rest;
                    stock.Quantity = rest;
                    
                    return (totalRemoved);
                }
            }
            return (0);
        }
    }
}