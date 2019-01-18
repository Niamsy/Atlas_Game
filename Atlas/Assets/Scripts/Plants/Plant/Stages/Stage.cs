using System.Collections.Generic;
using Game.Item.PlantSeed;
using Plants.Plant;
using UnityEngine;

namespace Plants
{
    [CreateAssetMenu(menuName = "Plant System/Stage")]
    public class Stage : ScriptableObject
    {
        public struct Need
        {
            Resources       type;
            int             quantity;
        }

        [SerializeField]
        private SoilType              _soils;
        [SerializeField]
        private List<Need>          _needs;
        [SerializeField]
        private Material            _model;
        [SerializeField]
        private AtlasAudio.Audio    _audio;
        [SerializeField]
        private float               _height;
        [SerializeField]
        private float               _temperature;

        public SoilType Soils
        {
            get { return _soils; }
        }

        public List<Need> Needs
        {
            get { return _needs; }
        }

        public Material Model
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
    }
}
