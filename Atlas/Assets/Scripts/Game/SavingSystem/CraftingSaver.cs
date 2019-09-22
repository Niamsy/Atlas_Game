using System;
using System.Collections.Generic;
using System.Linq;
using Game.Crafting;
using Game.SavingSystem.Datas;
using UnityEngine;

namespace Game.SavingSystem
{
    public class CraftingSaver : MapSavingBehaviour
    {
        private Crafter _crafter;

        protected override void Awake()
        {
            base.Awake();
            _crafter = GetComponent<Crafter>();
        }

        protected override void SavingMapData(MapData data)
        {
            data.Crafting = new MapData.CraftingSaveData(_crafter);
        }

        protected override void LoadingMapData(MapData data)
        {
            var recipes = Resources.FindObjectsOfTypeAll<Recipe>();

            if ( data.Crafting.OnGoingProducts != null)
                FillProductList(recipes, _crafter.ProductsOngoing, data.Crafting.OnGoingProducts, true);
            if ( data.Crafting.FinishedProducts != null)
                FillProductList(recipes, _crafter.ProductsFinished, data.Crafting.FinishedProducts, false);
        }

        private void FillProductList(Recipe[] recipes, List<Recipe.Product> list, MapData.ProductSaveData[] from, bool start)
        {
            foreach (var saveData in from)
            {
                var recipe = FindProduct(recipes, saveData.Id);
                if (recipe == null) continue;
                
                var clone = recipe.Produced.GetClone((int) saveData.TimeRemaining);
                clone.OriginalDuration = saveData.OriginalTiming;
                list.Add(clone);
                if (start)
                    clone.Start(clone.TimeRemaining, clone.OriginalDuration, list.Count - 1);
            }
        }

        private Recipe FindProduct(Recipe[] recipes, int id)
        {
            return (from recipe in recipes where recipe.Produced.Item.Id == id select recipe).FirstOrDefault();
        }
    }
}