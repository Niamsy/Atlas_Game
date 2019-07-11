using System;
using System.Collections.Generic;
using Game.DayNight;
using Game.ResourcesManagement;
using Plants.Plant;

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
        public struct XPSaveData
        {
            public float PlayerXP;
            public float RoofLevel;
            public float FloorLevel;
            public float PlayerLevel;
            public bool NotFirstTime;
        }

        public PlantSaveData[]      Plants;
        public ItemDroppedsData[]   DroppedItems;
        public List<ItemBaseData>	Inventory;
        public ItemBaseData			EquippedHand;
        public TransformSaveData	TransformData;
        public Date					CalendarData;
        public XPSaveData           XPData;
    }
}
