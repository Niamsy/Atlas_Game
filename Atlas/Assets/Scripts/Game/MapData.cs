using System;
using System.Collections.Generic;
using Game.ResourcesManagement;
using Plants.Plant;

namespace Game
{
    [Serializable]
    public class MapData : SaveData
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

        public PlantSaveData[] Plants;
    }
}
