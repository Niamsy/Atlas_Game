using System.Collections.Generic;
using Game.Item.PlantSeed;
using Plants.Plant;
using UnityEngine;

namespace Plants
{
    [CreateAssetMenu(menuName = "Plant System/Stage")]
    public class Stage : ScriptableObject
    {
        [System.Serializable]
        public struct Need
        {
            public Resources       type;
            public int             quantity;
        }
        [Header("Gameplay")]
        [SerializeField]
        private SoilType            _soils;
        [SerializeField]
        private List<Need>          _needs;
        [SerializeField]
        private float               _height;
        [SerializeField]
        private float               _temperature;
        [SerializeField]
        [Header("Miscellaneous")]
        private GameObject          _model;
        [SerializeField]
        private Material            _material;
        [SerializeField]
        private AtlasAudio.Audio    _audio;

        public SoilType Soils
        {
            get { return _soils; }
        }

        public List<Need> Needs
        {
            get { return _needs; }
        }

        public GameObject Model
        {
            get { return _model; }
        }

        public AtlasAudio.Audio Audio
        {
            get { return _audio; }
        }

        public float Height
        {
            get { return _height; }
        }

        public float Temperature
        {
            get { return _temperature; }
        }

        public Material Material
        {
            get { return _material; }
        }
    }
}
