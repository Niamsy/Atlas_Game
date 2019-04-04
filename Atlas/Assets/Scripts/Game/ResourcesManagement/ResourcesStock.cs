using System;
using System.Collections.Generic;
using UnityEngine;

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

        public Stock this[Resource resource]
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
                    var rest = quantity - (stock.Limit - stock.Quantity);
                    if (rest < 0)
                        rest = 0;
                    var totalAdded = quantity - rest;

                    stock.Quantity += totalAdded;
                    
                    return (totalAdded);
                }
            }
            return (quantity);
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