﻿using System.Collections.Generic;
using Plants.Plant;
using UnityEngine;
using UnityEngine.Experimental.Rendering.HDPipeline;
using UnityEngine.SceneManagement;

namespace Plants
{
    public class PlantSystem : MonoBehaviour
    {
        public static PlantSystem Instance;
        
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

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.N))
                SetDisplayType(_first);
            else if (Input.GetKeyDown(KeyCode.B))
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
                mainCam.SetReplacementShader(displayType.Shader, null);
                foreach (var texturePair in displayType.TexturesToSet)
                    Shader.SetGlobalTexture(texturePair.Name, texturePair.Texture);            
            }
        }
        #endregion
    }
}
