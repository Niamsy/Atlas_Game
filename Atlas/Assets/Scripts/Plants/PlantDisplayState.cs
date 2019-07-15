using System;
using System.Collections.Generic;
using UnityEngine;

namespace Plants
{
    public enum DisplayType
    {
        Default,
        WaterNeed
    }

    [Serializable]
    public struct ShaderTexture
    {
        public string    Name;
        public Texture   Texture;
    }
    
    [CreateAssetMenu]
    public class PlantDisplayState : ScriptableObject
    {

        public DisplayType    Type;
        public Shader         Shader;
        public List<ShaderTexture> TexturesToSet;
    }
}
