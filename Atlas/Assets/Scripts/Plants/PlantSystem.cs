using System.Collections.Generic;
using Plants.Plant;
using UnityEngine;

namespace Plants
{
    public class PlantSystem : MonoBehaviour
    {
        public static PlantSystem Instance;
        
        private List<PlantModel> _plants = new List<PlantModel>();

        #region Methods
        private void Awake()
        {
            Instance = this;
        }

        private void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }

        #region Remove/Add
        public void AddPlant(PlantModel newPlant)
        {
            if (!_plants.Contains(newPlant))
                _plants.Add(newPlant);
        }

        public void RemovePlant(PlantModel plant)
        {
            _plants.Remove(plant);
        }
        #endregion

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                SetDisplayType(_first);
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                SetDisplayType(_second);
        }
        
        public PlantDisplayState _first;
        public PlantDisplayState _second;
        
        private PlantDisplayState          _display;

        public void SetDisplayType(PlantDisplayState displayType)
        {
            Camera mainCam = Camera.main;
            mainCam.ResetReplacementShader();
            if (displayType.Shader != null)
            {
                mainCam.SetReplacementShader(displayType.Shader, "RenderType");
                foreach (var texturePair in displayType.TexturesToSet)
                    Shader.SetGlobalTexture(texturePair.Name, texturePair.Texture);            
            }
        }
        #endregion
    }
}
