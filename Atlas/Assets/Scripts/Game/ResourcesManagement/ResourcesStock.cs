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
        private PlantWateringIcon icon = null;

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

        private void Awake()
        {
            icon = this.GetComponentInParent<PlantWateringIcon>();
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
            waterQuantity = this[Resource.Water].Quantity;
            return (quantity);
        }

        public bool FindResource(Resource resource)
        {
            foreach (Stock stock in _listOfStocks)
            {
                if (stock.Resource == resource)
                    return true;
            }
            return false;
        }

        private void showWaterIcon(Resource resource, Stock stock)
        {
            if (resource == Resource.Water && icon != null)
            {
                if (!iconDisplayed)
                {
                    Canvas hud = gameObject.transform.Find("HUD").gameObject.GetComponent<Canvas>();
                    foreach (var child in hud.GetComponentsInChildren<Canvas>())
                    {
                        if (child.name == "Watering")
                        {
                            Canvas wtr = child;
                            icon.WaterIcon = wtr;
                            Image wtrf = wtr.transform.Find("WaterFilled").gameObject.GetComponentInChildren<Image>();
                            icon.WaterDrop = wtrf;
                            icon.PlantStock = stock;
                            icon.StartUpdate();
                            StartCoroutine(hideWaterIcon(icon, stock, 5f));
                            iconDisplayed = true;
                        }
                    }
                }
                else
                {
                    icon.PlantStock = stock;
                }
            }
        }

        private IEnumerator hideWaterIcon(PlantWateringIcon icon, Stock stock, float delay)
        {
            yield return new WaitForSeconds(delay);
            //Debug.Log("Delay Passed Away + water Quantity " + waterQuantity.ToString() + " Stock Quantity " + stock.Quantity);
            if (waterQuantity != stock.Quantity && iconDisplayed == true)
            {
                iconDisplayed = false;
                icon.StopUpdate();
            }
            else
            {
                StartCoroutine(hideWaterIcon(icon, this[Resource.Water], delay));
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