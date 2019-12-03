using System;
using System.Collections.Generic;
using System.Linq;
using Game.Crafting;
using Game.DayNight;
using Game.Questing;
using Game.ResourcesManagement;
using Plants.Plant;
//using UnityEditor;
using UnityEngine;

namespace Game.SavingSystem.Datas
{
    [Serializable]
    public class MapData
    {
        [Serializable]
        public struct ResourceSaveData
        {
            public static Array Values = Enum.GetValues(typeof(Resource));
            
            public Resource Resource;
            public int Quantity;
        }
        
        [Serializable]
        public struct PlantSaveData
        {
            public int                ID;
            public TransformSaveData  PlantPosition;
            public int                CurrentStage;
            public ResourceSaveData[] Resources;
            
            public PlantSaveData(PlantModel model)
            {
                ID = model.PlantStatistics.ID;
                PlantPosition = new TransformSaveData(model.transform);
                CurrentStage = model.CurrentStageInt;

                var resourceStock = model.RessourceStock.ListOfStocks;
                Resources = new ResourceSaveData[resourceStock.Count];
                for (int x = 0; x < resourceStock.Count; x++)
                {
                    Resources[x].Resource = resourceStock[x].Resource;
                    Resources[x].Quantity = resourceStock[x].Quantity;
                }
            }
        }

        [Serializable]
        public struct ProductSaveData
        {
            public int Id;
            public float TimeRemaining;
            public float OriginalTiming;
            
            public void SetFromProduct(Recipe.Product product)
            {
                Id = product.Item.Id;
                TimeRemaining = product.TimeRemaining;
                OriginalTiming = product.OriginalDuration;
            }
        }

        [Serializable]
        public struct CraftingSaveData
        {
            public ProductSaveData[] OnGoingProducts;
            public ProductSaveData[] FinishedProducts;

            public CraftingSaveData(Crafter crafter)
            {
                OnGoingProducts = new ProductSaveData[crafter.ProductsOngoing.Count];
                for (int i = 0; i < crafter.ProductsOngoing.Count; i++)
                {
                    OnGoingProducts[i].SetFromProduct(crafter.ProductsOngoing[i]);
                    
                }
                
                FinishedProducts = new ProductSaveData[crafter.ProductsFinished.Count];
                for (int i = 0; i < crafter.ProductsFinished.Count; i++)
                {
                    FinishedProducts[i].SetFromProduct(crafter.ProductsFinished[i]);
                }
            }
        }

        [Serializable]
        public struct RequirementData
        {
            public Guid ConditionId;
            public int ItemAbstractId;
            public int CurrentlyAccomplished;
            public String ConditionName;

            public RequirementData(Requirement requirement, int currentlyAccomplished)
            {
                ConditionId = requirement.Condition.Id;
                ItemAbstractId = requirement.Argument.Id;
                CurrentlyAccomplished = currentlyAccomplished;
                ConditionName = requirement.Condition.name;
            }
        }
        
        [Serializable]
        public struct QuestData
        {
            public Guid Id;
            public String Name;
            public RequirementData[] Requirements;
            
            public QuestData(LiveQuest data)
            {
                Id = data.Quest.Id;
                Name = data.Quest.Name;
                Requirements = data.Requirements.Select(requirement =>
                    new RequirementData(requirement.Requirement, requirement.CurrentlyAccomplished)).ToArray();
            }
        }

        [Serializable]
        public struct QuestingData
        {
            public QuestData[] Quests;
        }

        public CraftingSaveData     Crafting;
        public PlantSaveData[]      Plants;
        public ItemDroppedsData[]   DroppedItems;
        public List<ItemBaseData>	Inventory;
        public List<ItemBaseData>	EquippedItems;
        public int                  SelectedItems;
        public TransformSaveData	TransformData;
        public DateData				CalendarData;
        public List<Stock>          PlayerResource;
        public QuestingData         Questing;
    }
}
