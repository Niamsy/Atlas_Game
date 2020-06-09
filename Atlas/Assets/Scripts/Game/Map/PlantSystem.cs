using System.Collections.Generic;
using Game;
using Game.SavingSystem;
using Game.SavingSystem.Datas;
using Plants.Plant;
using UnityEngine;

namespace Plants
{
    public class PlantSystem : MapSavingBehaviour
    {
        private List<PlantModel> _models = new List<PlantModel>();

        #region Methods
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.N))
                SetDisplayType(_first);
            else if (Input.GetKeyDown(KeyCode.B))
                SetDisplayType(_second);
        }

        public void AddPlantToTheMap(PlantModel plant)
        {
            if (!_models.Contains(plant))
                _models.Add(plant);
        }

        public void RemovePlantFromTheMap(PlantModel plant)
        {
            if (_models.Contains(plant))
                _models.Remove(plant);
        }

        #region SetDisplayType

        public PlantDisplayState _first;
        public PlantDisplayState _second;

        private PlantDisplayState _display;

        public void SetDisplayType(PlantDisplayState displayType)
        {
            Camera mainCam = Camera.main;
            mainCam.ResetReplacementShader();
            if (displayType.Shader != null)
            {
                mainCam.SetReplacementShader(displayType.Shader, null);
                foreach (var texturePair in displayType.TexturesToSet)
                    Shader.SetGlobalTexture(texturePair.Name, texturePair.Texture);
            }
        }

        #endregion

        #region Load/Save
        protected override void SavingMapData(MapData data)
        {
            MapData.PlantSaveData[] plantSaveDatas = new MapData.PlantSaveData[_models.Count];
            for (int x = 0; x < _models.Count; x++)
                plantSaveDatas[x] = new MapData.PlantSaveData(_models[x]);
            data.Plants = plantSaveDatas;
        }

        protected override void LoadingMapData(MapData data)
        {
            if (data.Plants == null)
                return;
            foreach (var plant in _models)
                Destroy(plant.gameObject);
            foreach (var plant in data.Plants)
            {
                var plantStats = GetPlantForID(plant.ID);
                if (plantStats)
                {
                    var trans = plant.PlantPosition;
                    GameObject plantModel = Instantiate(plantStats.Prefab, trans.Position.Value, trans.Rotation.Value);
                    PlantModel model = plantModel.GetComponent<PlantModel>();
                    model.Sow();
                    model.GoToStage(plant.CurrentStage);
                    foreach (var resourceSaveData in plant.Resources)
                        model.RessourceStock[resourceSaveData.Resource].Quantity = resourceSaveData.Quantity;
                }
            }
        }

        private static PlantStatistics[] _allPlantStats;

        private static void Create()
        {
            _allPlantStats = Resources.LoadAll<PlantStatistics>("Plants/");
        }

        public static PlantStatistics GetPlantForID(int id)
        {
            if (_allPlantStats == null)
                Create();

            foreach (var item in _allPlantStats)
                if (item.ID == id)
                    return (item);

            Debug.LogError("Plant of id:" + id + " not found");
            return (null);
        }
        
        public static PlantStatistics GetPlantForName(string name)
        {
            if (_allPlantStats == null)
                Create();

            foreach (var item in _allPlantStats)
                if (item.ScientificName == name)
                    return (item);

            Debug.LogWarning("Plant of id:" + name + " not found");
            return (null);
        }

        public  static PlantStatistics[] GetAllPlantStats()
        {
            if (_allPlantStats == null)
                Create();
            return _allPlantStats;
        }

        #endregion

        #endregion
    }
}
