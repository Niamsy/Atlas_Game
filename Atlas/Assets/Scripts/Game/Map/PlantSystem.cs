using System.Collections.Generic;
using Game;
using Game.SavingSystem;
using Plants.Plant;
using UnityEngine;

namespace Plants
{
    public class PlantSystem : MonoBehaviour
    {
        private List<PlantModel> _models = new List<PlantModel>();

        #region Methods

        private void Awake()
        {
            GameControl.BeforeSavingData += Save;
            GameControl.UponLoadingMapData += Loading;
        }

        private void OnDestroy()
        {
            GameControl.BeforeSavingData -= Save;
            GameControl.UponLoadingMapData -= Loading;
        }

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

        public void Save(GameControl gameControl)
        {
            MapData.PlantSaveData[] plantSaveDatas = new MapData.PlantSaveData[_models.Count];
            for (int x = 0; x < _models.Count; x++)
                plantSaveDatas[x] = new MapData.PlantSaveData(_models[x]);
            gameControl.MapData.Plants = plantSaveDatas;
        }

        public void Loading(GameControl gameControl)
        {
            foreach (var plant in _models)
                Destroy(plant.gameObject);
            foreach (var plant in gameControl.MapData.Plants)
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
        #endregion

        #endregion
    }
}
